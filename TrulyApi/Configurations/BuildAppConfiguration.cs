using TrulyApi.Repositorys.Interfaces;
using TrulyApi.Repositorys;
using TrulyApi.Services.Interfaces;
using TrulyApi.Services;

namespace TrulyApi.Configurations
{
    public static class BuildAppConfiguration
    {
        public static void DepandancyInjectionConfig(this IServiceCollection services)
        {
            //Services Depandancy Injection
            //services.AddScoped(typeof(ICsvFileBuilder<>), typeof(CsvFileBuilder<>));
            services.AddScoped<ICsvFileBuilder, CsvFileBuilder>();


            //Repository Depandancy Injection
            services.AddScoped<IQuotesRepository, QuotesRepository>();
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            //services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddTransient<IDateTime, DateTimeService>();
        }
    }
}
