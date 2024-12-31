using Application.IService;
using Application.Service;
using Domain.IRepository;
using Infra.Data.Repository;
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
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ILabelTaskRepository, LabelTaskRepository>();
            services.AddScoped<ILabelTaskService, LabelTaskService>();
        }
    }
}
