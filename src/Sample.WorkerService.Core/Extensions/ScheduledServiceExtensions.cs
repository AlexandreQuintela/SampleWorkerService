using Microsoft.Extensions.DependencyInjection;

namespace Sample.WorkerService.Core.Extensions;

public static class ScheduledServiceExtensions
{
    public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobExtensions
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options), @"Forneça a configurações do agendamento.");
        }

        var config = new ScheduleConfig<T>();
        options.Invoke(config);

        if (string.IsNullOrWhiteSpace(config.CronExpression))
        {
            throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"A expressão do Cron vazia não é permitida!");
        }

        services.AddSingleton<IScheduleConfig<T>>(config);
        services.AddHostedService<T>();

        return services;
    }
}