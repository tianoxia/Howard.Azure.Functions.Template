using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Howard.FunctionApp.Model.Constants;
using Howard.FunctionApp.Services.Helpers;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Howard.FunctionApp.Template.Extensions.ServiceCollection
{
    public static class LoggingServiceCollection
    {
        /// <summary>
        ///     Adds Logging service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddLoggingServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            var instrumentationKey = Environment.GetEnvironmentVariable(EnvironmentConstants.APPINSIGHTS_INSTRUMENTATIONKEY);
            var logger = new LoggerConfiguration()
                            .WriteTo.Console(LogEventLevel.Information)
                            .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, LogEventLevel.Information)
                            .ReadFrom.Configuration(configuration)
                            .CreateLogger();
            services.AddSingleton(logger);
            services.AddLogging(l => l.AddSerilog(logger));

            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console(LogEventLevel.Information)
                        .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, LogEventLevel.Information)
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            services.AddSingleton(Log.Logger);
        }
    }
}
