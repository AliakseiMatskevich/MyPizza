using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Data;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Services;

namespace MyPizza.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEFRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IUoWRepository), typeof(UoWRepository));
            services.AddScoped(typeof(ICategoryViewModelService), typeof(CategoryViewModelService));
            services.AddScoped(typeof(IProductTypeViewModelService), typeof(ProductTypeViewModelService));

            services.AddScoped(typeof(IUoWServices), typeof(UoWServices));
            return services;
        }
    }
}
