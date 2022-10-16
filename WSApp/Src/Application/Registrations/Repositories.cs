using WSApp.Src.Domain.Repositories;
using WSApp.Src.Domain.Repositories.Base;
using WSApp.Src.Persistence.Repositories;
using WSApp.Src.Persistence.Repositories.Base;

namespace WSApp.Src.Application.Registrations
{
    public static class Repositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepositories<>), typeof(BaseRepositories<>));
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}