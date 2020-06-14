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
using System.IO;
using System.Security.Cryptography;

namespace KursAuth
{
    public class App : Application
    {
        public string token { get; set; }

        string path = @"D:\Не мэйн токен";

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

            try
            {
                using (FileStream fstream = File.OpenRead($@"{path}\note.txt"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string token = System.Text.Encoding.Default.GetString(array);
                }
            }
            catch
            {
                
            }

            
            

            



            Locator.CurrentMutable.RegisterConstant<IScreen>(new MainWindowViewModel());
            Locator.CurrentMutable.RegisterConstant<IScreen>(new MainAuthVM());


            Locator.CurrentMutable.Register<IViewFor<TestVM>>(() => new TestView());
            Locator.CurrentMutable.Register<IViewFor<VkAuthVM>>(() => new VkAuth());
            Locator.CurrentMutable.Register<IViewFor<TlAuthVM>>(() => new TlAuth());
            Locator.CurrentMutable.Register<IViewFor<TlContVM>>(() => new TlContacts());
            Locator.CurrentMutable.Register<IViewFor<VkContVM>>(() => new VkContacts());
            

            base.OnFrameworkInitializationCompleted();
        }
    }
}
