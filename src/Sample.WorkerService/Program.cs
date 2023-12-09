using Sample.WorkerService.Core.Extensions;
using Serilog;

SerilogExtension.AddSerilog();

try
{
    Log.Information("Iniciando Host.");
    Log.Information("----------------");
    Log.Error("Testando... Mensagem de erro.");
    Log.Information("Testando... Mensagem de informação.");
    Log.Fatal("Testando... Erro fatal.");
    Log.Debug("Testando... Mensagem de Debug.");
    Log.Warning("Testando... Mensagem de Warning.");

    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

    })
    .UseSerilog()
    .Build();

    await host.RunAsync();
    return 0;
}
catch (Exception ex)
{
   Log.Fatal(ex, "Host finalizado inesperadamente.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}