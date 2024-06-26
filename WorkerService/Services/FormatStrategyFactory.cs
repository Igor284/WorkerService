using WorkerService.FormatStrategy;
using WorkerService.Services.Interfaces;
using WorkerService.Strategies;

namespace WorkerService.Services
{
    public class FormatStrategyFactory : IFormatStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<FormatStrategyFactory> _logger;
        private readonly IReadOnlyDictionary<string, Func<IFormatStrategy>> _strategyMap;

        public FormatStrategyFactory(IServiceProvider serviceProvider, ILogger<FormatStrategyFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _strategyMap = new Dictionary<string, Func<IFormatStrategy>>
            {
                ["CSV"] = GetService<CsvFormatStrategy>,
                ["JSON"] = GetService<JsonFormatStrategy>,
                ["XML"] = GetService<XmlFormatStrategy>
            };
        }

        public bool TryGet(string format, out IFormatStrategy strategy)
        {
            strategy = default;
            if (!_strategyMap.TryGetValue(format, out var strategyFunc))
            {
                _logger.LogWarning("wrong format");

                return false;
            }

            strategy = strategyFunc();
            return true;
        }

        private IFormatStrategy GetService<TStrategy>() where TStrategy : IFormatStrategy
        {
            using var serviceScope = _serviceProvider.CreateScope();
            return serviceScope.ServiceProvider.GetService<TStrategy>();
        }
    }
}
