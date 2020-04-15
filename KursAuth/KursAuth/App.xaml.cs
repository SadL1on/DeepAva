using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Castle.Windsor;
using KursAuth.Interfaces;
using KursAuth.Services;
using KursAuth.ViewModels;
using KursAuth.Views;
using SimpleInjector;

namespace KursAuth
{
    public class App : Application
    {
        private readonly IUserService _userService;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Bootstrap();
        }

        private static Container container;

        [System.Diagnostics.DebuggerStepThrough]
        public static TService GetInstance<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(_userService),
                };
            }
            Bootstrap();

            base.OnFrameworkInitializationCompleted();
        }

        private static void Bootstrap()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance:
            //container.RegisterSingle<IUserRepository, SqlUserRepository>();
            container.Register<IUserService, UserService>();

            // Optionally verify the container.
            container.Verify();

            // Store the container for use by the application.
            App.container = container;
        }
    }
}
