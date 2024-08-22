using FlowersWhisperingAPI.src.User.Services;
using FlowersWhisperingAPI.User.Mappers;
using FlowersWhisperingAPI.User.Services.Interface;

namespace FlowersWhisperingAPI.User
{
    public static class UserServiceRegistration
    {
        public static IServiceCollection AddUserService(this IServiceCollection services, string connectionString)
        {
            //services.AddSingleton(new AccountMapper(connectionString));
            
            // 注册自定义服务
            services.AddSingleton(new UserAccountMapper(connectionString));
            services.AddScoped<IUserAccountService, UserAccountService>();
            // 可以在这里继续注册其他服务
            // services.AddScoped<IOtherService, OtherService>();

            return services;
        }
    }
}