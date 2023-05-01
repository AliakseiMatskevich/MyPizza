using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Data;
using MyPizza.Infrastructure.Data.QueryServices;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Services;
using MyPizza.Web.Services.Stripe;
using MyPizza.Web.Services.TimeService;

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
            services.AddScoped(typeof(IWeightTypeViewModelService), typeof(WeightTypeViewModelService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(ICartQueryService), typeof(CartQueryService));             
            services.AddScoped(typeof(ICartService), typeof(CartService));
            services.AddScoped(typeof(ICartViewModelService), typeof(CartViewModelService));
            services.AddScoped(typeof(IOrderViewModelService), typeof(OrderViewModelService));
            services.AddScoped(typeof(IStripeService), typeof(StripeService));
            services.AddScoped(typeof(ITimeService), typeof(TimeService)); 
            return services;
        }
    }
}
