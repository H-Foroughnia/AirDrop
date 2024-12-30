using Application.IService;
using Application.Service;
using Domain.IRepository;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public class DependancyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEarnRepository, EarnRepository>();
            services.AddScoped<IEarnService, EarnService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<IFriendsRepository, FriendsRepository>();

        }
    }
}
