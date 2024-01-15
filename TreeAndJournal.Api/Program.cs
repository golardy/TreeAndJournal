using TreeAndJournal.Infrastructure;
using TreeAndJournal.Application;
using TreeAndJournal.Api.Extensions;
using Serilog;
using Serilog.Events;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Microsoft.Extensions.Configuration;

namespace TreeAndJournal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCustomExceptionHandler();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}