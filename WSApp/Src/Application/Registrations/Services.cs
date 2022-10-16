using WSApp.Src.Application.Services;
using WSApp.Src.Application.Services.Base;
using WSApp.Src.Domain.Services.Base;

namespace WSApp.Src.Application.Registrations
{
    public static class Services
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
