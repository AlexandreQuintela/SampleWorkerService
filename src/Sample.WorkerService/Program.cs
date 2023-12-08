using Sample.WorkerService;
using Sample.WorkerService.Core.Extensions;
using Serilog;

SerilogExtension.AddSerilog();

try
{
    Log.Information("Starting host");
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}