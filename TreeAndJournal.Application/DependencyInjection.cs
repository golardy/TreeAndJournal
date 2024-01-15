using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TreeAndJournal.Application.Abstractions.Date;
using TreeAndJournal.Application.Utils;
using TreeAndJournal.Application.Abstractions.Behaviors;

namespace TreeAndJournal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
            {
                configuration.AddOpenBehavior(typeof(LoggingExceptionsBehavior<,>));
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
