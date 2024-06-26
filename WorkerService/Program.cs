using Serilog;
using Serilog.Events;
using WorkerService.Options;
using WorkerService.FormatStrategy;
using WorkerService.Services;
using WorkerService.Services.Interfaces;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"C:\temp\workerservice\LogFile.txt")
    .CreateLogger();

try
{
    Log.Information("Starting up the service");
    IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IFormatStrategyFactory, FormatStrategyFactory>();
        services.AddScoped<CsvFormatStrategy>();
        services.AddScoped<JsonFormatStrategy>();
        services.AddScoped<XmlFormatStrategy>();

        services.Configure<WorkerOptions>(context.Configuration.GetSection("WorkerOptions"));
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();
    return;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Threr was a problem starting the service");
    return;
}
