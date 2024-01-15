using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TreeAndJournal.Application.Abstractions.Date;
using TreeAndJournal.Application.Behaviours;
using TreeAndJournal.Application.Utils;

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
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
