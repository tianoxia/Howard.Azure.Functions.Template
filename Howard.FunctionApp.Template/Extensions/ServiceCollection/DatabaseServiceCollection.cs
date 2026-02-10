using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Howard.FunctionApp.Model.Constants;
using Howard.FunctionApp.Repository.Context;
using Howard.FunctionApp.Repository.Pattern.Implementation;
using Howard.FunctionApp.Repository.Pattern.Interface;
using System;

namespace Howard.FunctionApp.Template.Extensions.ServiceCollection
{
    public static class DatabaseServiceCollection
    {
        public static void AddDatabaseServiceCollection(this IServiceCollection services)
        {
            var db = Environment.GetEnvironmentVariable(EnvironmentConstants.AZURE_DB);
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(db));
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(db);

            using (var context = new DatabaseContext(optionsBuilder.Options))
                context.Database.Migrate();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
