using Microsoft.Extensions.DependencyInjection;
using Howard.FunctionApp.Services.Implementation;
using Howard.FunctionApp.Services.Interface;

namespace Howard.FunctionApp.Template.Extensions.ServiceCollection
{
    public static class ServiceServiceCollection
    {
        public static void AddServicesServiceCollection(this IServiceCollection services)
        {
            services.AddTransient<IItemService, ItemService>();
        }
    }
}
