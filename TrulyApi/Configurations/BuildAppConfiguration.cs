using TrulyApi.Repositorys.Interfaces;
using TrulyApi.Repositorys;

namespace TrulyApi.Configurations
{
    public static class BuildAppConfiguration
    {
        public static void DepandancyInjectionConfig(this IServiceCollection services)
        {
            //Services Depandancy Injection
            //services.AddScoped<IIdentityService, IdentityService>();


            //Repository Depandancy Injection
            services.AddScoped<IQuotesRepository, QuotesRepository>();
        }
    }
}
