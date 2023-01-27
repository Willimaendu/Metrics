using winservice;
using Prometheus;
using winservice.Configurations;

int metricsPort = 0;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "WinService";
    })
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        var metricsConfigurationSection = hostContext.Configuration.GetRequiredSection(MetricsConfiguration.Name);
        metricsPort = metricsConfigurationSection.GetValue<int>("Port");
        services.Configure<MetricsConfiguration>(metricsConfigurationSection);
    })
    .Build();


using var server = new MetricServer(metricsPort);
server.Start();

host.Run();
