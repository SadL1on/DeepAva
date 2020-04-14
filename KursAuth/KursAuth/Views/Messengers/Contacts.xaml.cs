using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;
using System.Collections;
using System.Collections.Generic;
using VkNet.Model;

namespace KursAuth.Views.Messengers
{
    public class Contacts : ReactiveUserControl<MainWindowViewModel>
    {
       public ListBox contacts;
        AutorizationVk vk;
        public IEnumerable Items { get => contacts.Items; set => contacts.Items = value; }

        public Contacts()
        {
            this.InitializeComponent();
        }

        public Contacts(AutorizationVk vk)
        {
            this.vk = vk;
            this.InitializeComponent();

            //contacts = this.FindControl<ListBox>("Contacts");
            //var cont = MainWindowViewModel.GetFriends(vk, contacts);
            //contacts.Items = cont;
           
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
