using ExchangeHttpWorker.DependencyInjection;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder
    .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;
        config.AddEnvironmentVariables();
        config.AddJsonFile("Secrets.json", optional: true, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices(ConfigureAppServices.ConfigureServices);


IHost host = builder.Build();

host.Run();