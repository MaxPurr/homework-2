using Microsoft.Extensions.DependencyInjection;

namespace Domain.Services
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IProductService, ProductService>();
        }
    }
}
