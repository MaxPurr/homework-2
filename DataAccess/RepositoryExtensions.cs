using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}
