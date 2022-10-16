using Microsoft.AspNetCore.Mvc.Razor;

namespace WSApp.Src.Application.Configurations
{
    public static class AppConfigurations
    {
        public static void RazorConfiguration(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/Admin/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Admin/Shared/{0}" + RazorViewEngine.ViewExtension);

                o.ViewLocationFormats.Add("/Views/Client/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Client/Shared/{0}" + RazorViewEngine.ViewExtension);
            });
        }
    }
}