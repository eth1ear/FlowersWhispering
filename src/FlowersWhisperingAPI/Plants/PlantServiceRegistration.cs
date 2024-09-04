using FlowersWhisperingAPI.Plants.Mappers;
using FlowersWhisperingAPI.Plants.Services;
using FlowersWhisperingAPI.Plants.Services.Interface;

namespace FlowersWhisperingAPI.Plants
{
    public static class PlantServiceRegistration
    {
        public static IServiceCollection AddPlantService(this IServiceCollection services, string connectionString)
        {
            
            // 注册自定义服务
            services.AddSingleton(new PlantFindMapper(connectionString));
            services.AddSingleton(new PlantLatestMapper(connectionString));
            services.AddScoped<IPlantFindService, PlantFindService>();
            services.AddScoped<IPlantLatestService, PlantLatestService>();
            // 可以在这里继续注册其他服务
            

            return services;
        }
    }
}