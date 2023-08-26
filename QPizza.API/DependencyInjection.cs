using Microsoft.AspNetCore.Mvc.Infrastructure;
using QPizza.API.Common.Errors;
using QPizza.API.Common.Mapping;

namespace QPizza.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, QPizzaProblemDetailsFactory>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();

            return services;
        }
    }
}
