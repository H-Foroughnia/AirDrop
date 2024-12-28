using AirDrop.Application.Interface;
using AirDrop.Application.MVC.IService;
using AirDrop.Application.MVC.Service;
using AirDrop.Application.Service;
using AirDrop.Domain.Interface;
using AirDrop.Domain.MVC.IRepository;
using AirDrop.Infra.Data.MVC.Repository;
using AirDrop.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AirDrop.Infra.IoC;

public class DependencyContainer
{
    public static void RegisterService(IServiceCollection services)
    {
        #region MVC

        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITaskRepository, TaskRepository>();

        #endregion

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskImageRepository, TaskImageRepository>();
        services.AddScoped<ITaskImageService, TaskImageService>();

    }
}