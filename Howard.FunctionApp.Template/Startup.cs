using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Howard.FunctionApp.Template.Extensions.ServiceCollection;
using Howard.FunctionApp.Model.AutoMapper;
using Howard.FunctionApp.Model.Constants;
using Howard.FunctionApp.Services.Helpers;
using Serilog;
using System;

[assembly: FunctionsStartup(typeof(Howard.FunctionApp.Template.Startup))]
namespace Howard.FunctionApp.Template
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var currentEnvironment = Environment.GetEnvironmentVariable(EnvironmentConstants.ASPNETCORE_ENVIRONMENT);
            var jsonFile = currentEnvironment.Equals("local", StringComparison.OrdinalIgnoreCase) 
                                    ? "appsettings.local.json" : "appsettings.json";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(DirectoryHelper.GetCurrentDirectory())
                .AddJsonFile(jsonFile, false, true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLoggingServiceCollection(configuration);
            Log.Information("Adding Database Service");
            builder.Services.AddDatabaseServiceCollection();
            Log.Information("Adding CustomServices Service");
            builder.Services.AddServicesServiceCollection();
            Log.Information("Adding AutoMapper Service");
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
