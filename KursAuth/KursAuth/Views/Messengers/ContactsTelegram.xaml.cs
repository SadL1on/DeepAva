using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;
using ReactiveUI;

namespace KursAuth.Views.Messengers
{
    public class ContactsTelegram : ReactiveUserControl<MainWindowViewModel>
    {

        private ListBox contacts => this.FindControl<ListBox>("ContactsTelegram");
        public ContactsTelegram()
        {
            this.InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.TelegramDialogs, x => x.contacts.Items);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
