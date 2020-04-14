using Avalonia;
using Avalonia.Collections;
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


        public static readonly DirectProperty<Contacts, IEnumerable> ItemsProperty =
            AvaloniaProperty.RegisterDirect<Contacts, IEnumerable>(
                nameof(Items),
                o => o.Items,
                (o, v) => o.Items = v);

        private IEnumerable _items = new AvaloniaList<object>();

        public IEnumerable Items
        {
            get => _items;
            set => SetAndRaise(ItemsProperty, ref _items, value);
        }


        public ListBox contacts;
        AutorizationVk vk;
       

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
