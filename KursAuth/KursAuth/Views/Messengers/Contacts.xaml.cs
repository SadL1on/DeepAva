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
        private Avalonia.Controls.Button sendmessage;
        private TextBox messagetext;
        private User user;

        public Contacts()
        {
            this.InitializeComponent();
            messHist = this.FindControl<ListBox>("MessHist");
            contacts = this.FindControl<ListBox>("Contacts");
            sendmessage = this.FindControl<Avalonia.Controls.Button>("SendMessage");
            messagetext = this.FindControl<TextBox>("MessageText");

            contacts.Tapped += Contacts_Tapped;
            
        }


        private void Contacts_Tapped(object sender, RoutedEventArgs e)
        {
            User user = (User)contacts.SelectedItem;
            this.user = user;
            ViewModel.GetHisVM(user, messHist);
            sendmessage.Click += Sendmessage_Click;

        }

        private void Sendmessage_Click(object sender, RoutedEventArgs e)
        {
            string text = messagetext.Text;
            var userid = user.Id;
            ViewModel.SendMessage(userid, text);
            messagetext.Text = null;

           


        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
