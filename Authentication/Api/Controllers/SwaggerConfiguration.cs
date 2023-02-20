using Microsoft.OpenApi.Models;

namespace Api.Controllers
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication Service",
                    Version = "v1",
                });

            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
