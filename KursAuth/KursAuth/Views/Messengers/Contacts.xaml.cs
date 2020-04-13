using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;

namespace KursAuth.Views.Messengers
{
    public class Contacts : ReactiveUserControl<MainWindowViewModel>
    {
        ListBox contacts;

        public Contacts()
        {
            this.InitializeComponent();
        }

        public Contacts(AutorizationVk vk)
        {
            this.InitializeComponent();

            contacts = this.FindControl<ListBox>("Contacts");
            MainWindowViewModel.GetFriends(vk, contacts);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
