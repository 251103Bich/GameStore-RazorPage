using BussinessLogic.Interfaces;
using BussinessLogic.Service;
using DataAccess.DAOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using Repository.Interfaces;
using Repository.Repositories;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Use DI to create and show the main window
            var mainWindow = ServiceProvider.GetRequiredService<Login>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register your services and repositories
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IProfileRepository, ProfileRepository>();

            // Register ViewModels
            services.AddTransient<GameViewModel>();
            services.AddTransient<GameDeleteModel>();

            services.AddTransient<ProfileSellerViewModel>();
            services.AddTransient<ProfileSellerDelete>();

            services.AddTransient<ApproveSellerViewModel>();
            services.AddTransient<ApproveGameViewModel>();



            // Register DAOs
            services.AddTransient<GameDAO>();
            services.AddTransient<ProfileDAO>();
            services.AddTransient<UserViewModels>();
            services.AddTransient<FeedbackDAO>();


            services.AddTransient<LoginViewModels>();

            services.AddTransient<WindowUser>();
            services.AddTransient<Login>();
            // Register windows with DI
            services.AddTransient<GameWindow>();
            services.AddTransient<GameDeleteWindow>();
            services.AddTransient<ApproveWindow>();
            services.AddTransient<ApproveGameWindow>();

            services.AddTransient<ProfileSellerWindow>();
            services.AddTransient<ProfileDeleteWindow>();

            services.AddSingleton<Login>(); // Main window
        }

    }

}
