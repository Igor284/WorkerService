using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Serialization;
using WorkerService.Models;
using WorkerService.Options;
using WorkerService.Services.Interfaces;

namespace WorkerService.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptionsMonitor<WorkerOptions> _options; //I use Options Monitor to track configuration changes while the application is running.
        private readonly IFormatStrategyFactory _formatStrategyFactory;

        public Worker(ILogger<Worker> logger, IOptionsMonitor<WorkerOptions> options, IFormatStrategyFactory formatStrategyFactory)
        {
            _logger = logger;
            _options = options;
            _formatStrategyFactory = formatStrategyFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //Interval is set in the configurations of appsettings.json
            var interval = TimeSpan.FromSeconds(_options.CurrentValue.Interval);
            using var timer = new PeriodicTimer(interval);

            while (!cancellationToken.IsCancellationRequested && await timer.WaitForNextTickAsync(cancellationToken))
            {
                try
                {
                    var client = new WebClient();
                    var clientData = client.DownloadString("https://bank.gov.ua/NBU_Exchange/exchange?json");
                    var currencyRates = JsonConvert.DeserializeObject<List<CurrencyRate>>(clientData);

                    //I implemented a strategy so that in the future we could expand the functionality, instead of changing the logic.
                    // Strategy Factory checks the format for validity and returns the desired strategy to us.
                    _formatStrategyFactory.TryGet(_options.CurrentValue.Format.ToString().ToUpper(), out var strategy);
                    strategy.Convertor(currencyRates);

                    _logger.LogInformation("Data downloaded successfully");

                    if (string.IsNullOrEmpty(clientData))
                    {
                        _logger.LogInformation("Received empty data");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception: " + ex.Message);
                }
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker service starting up...");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker service has been stopped...");
            return base.StopAsync(cancellationToken);
        }
    }
}