using Api.AutoMapper.Profiles;

namespace Api.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperServices(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserProfile));

            return services;
        }
    }
}
