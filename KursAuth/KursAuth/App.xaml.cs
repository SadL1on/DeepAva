using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KursAuth.Models;
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
                desktop.MainWindow = new TestView
                {
                    DataContext = new TestVM()
                };                
            }
            Locator.CurrentMutable.RegisterConstant<IScreen>(new TestVM());
            Locator.CurrentMutable.Register<IViewFor<MainWindowViewModel>>(() => new MainWindow());
            Locator.CurrentMutable.Register<IViewFor<MainAuthVM>>(() => new MainAuth());
            Locator.CurrentMutable.Register<IViewFor<VkAuthVM>>(() => new VkAuth());
            Locator.CurrentMutable.Register<IViewFor<TlAuthVM>>(() => new TlAuth());
            Locator.CurrentMutable.Register<IViewFor<TlContVM>>(() => new TlContacts());
            Locator.CurrentMutable.Register<IViewFor<VkContVM>>(() => new VkContacts());
            
            base.OnFrameworkInitializationCompleted();
        }
    }
}
