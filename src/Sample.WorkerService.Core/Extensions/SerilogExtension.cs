using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace Sample.WorkerService.Core.Extensions;

public static class SerilogExtension
{
    public static void AddSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            //Configura o registro mínimo de log do namespace informado.
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            // Contexto principal de trabalho do Serilog.
            // É preciso registrar esse trecho em praticamente todos os casos, para obter os benefícios do provedor de log.
            .Enrich.FromLogContext()
            // Em caso de erro, coleta e registra todos os detalhes da exception.
            .Enrich.WithExceptionDetails()
            //Faz a correlação entre todos os registros de log, para facilitar o trace.
            .Enrich.WithCorrelationId()
            //Inclui uma nova propriedade customizada chamada ApplicationName no log.
            .Enrich.WithProperty("ApplicationName", $"WorkerService - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}") 
             //Exclui o log coletado de acordo com o Matching configurado.
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
            //Realiza todas as operações de forma assíncrona(background). Essa é uma das configurações mais importante do Serilog.
            .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
            .CreateLogger();
    }
}
