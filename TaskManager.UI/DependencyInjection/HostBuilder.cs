using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.AppLayer.Interfaces;
using TaskManager.Application.Services;

namespace TaskManager.UI.DependencyInjection;

public static class HostBuilder
{
    public static IHost CreateHost()
    {
        return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                RegisterUI(services);
                RegisterApplication(services);
            })
            .Build();
    }

    private static void RegisterUI(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        //View  models
        services.AddSingleton<TaskManager.UI.ViewModels.MainViewModel>();
    }

    private static void RegisterApplication(IServiceCollection services)
    {
        //Application services
        services.AddScoped<ITaskService, TaskService>();
    }
}
