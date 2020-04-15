using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;
using System.Collections;
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


        //public static readonly DirectProperty<Contacts, IEnumerable> ItemsssP =
        //    AvaloniaProperty.RegisterDirect<Contacts, IEnumerable>(
        //        nameof(Itemsss),
        //        o => o.Itemsss,
        //        (o, v) => o.Itemsss = v);

        //private IEnumerable _itemsss = new AvaloniaList<object>();

        //public IEnumerable Itemsss
        //{
        //    get => _itemsss;
        //    set => SetAndRaise(ItemsProperty, ref _itemsss, value);
        //}

        public ListBox contacts;
        private ListBox messHist;

        public Contacts()
        {
            this.InitializeComponent();
            messHist = this.FindControl<ListBox>("MessHist");
            contacts = this.FindControl<ListBox>("Contacts");
            contacts.Tapped += Contacts_Tapped;
            
        }


        private void Contacts_Tapped(object sender, RoutedEventArgs e)
        {
            User user = (User)contacts.SelectedItem;
            ViewModel.GetHisVM(user, messHist);

        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
