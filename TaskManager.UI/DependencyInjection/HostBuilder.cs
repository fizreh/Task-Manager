using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.AppLayer.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Persistence;
using TaskManager.UI.ViewModels;

namespace TaskManager.UI.DependencyInjection;

public static class HostBuilder
{
  
    public static IHost CreateHost()
    {
        DotNetEnv.Env.Load();
        return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                RegisterUI(services);
                RegisterApplication(services);
                RegisterInfrastructure(services);
            })
            .Build();
    }

    private static void RegisterUI(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();

        //View  models
        services.AddTransient<MainViewModel>();
    }

    private static void RegisterApplication(IServiceCollection services)
    {
        //Application services
        services.AddScoped<ITaskService, TaskService>();
    }

    private static void RegisterInfrastructure(IServiceCollection services)
    {
        // 2. Retrieve the connection string securely from the environment
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? throw new InvalidOperationException("Database connection string is missing.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
}
