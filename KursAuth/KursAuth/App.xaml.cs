using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KursAuth.ViewModels;
using KursAuth.ViewModels.Messengers;
using KursAuth.Views;
using KursAuth.Views.Messengers;
using ReactiveUI;
using Splat;

namespace KursAuth
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };

                
            }
            
            Locator.CurrentMutable.RegisterConstant<IScreen>(new MainWindowViewModel());
            Locator.CurrentMutable.Register<IViewFor<TestVM>>(() => new TestView());
            Locator.CurrentMutable.Register<IViewFor<VkAuthVM>>(() => new VkAuth());
            

            // Получаем корневую модель представления и инициализируем контекст данных.
            // new MainWindow { DataContext = Locator.Current.GetService<IScreen>() }.Show();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
