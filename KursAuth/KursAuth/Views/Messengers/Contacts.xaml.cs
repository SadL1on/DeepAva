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
            var UserId = user.Id;
            var messages = ViewModel.GetHisVM(UserId);
            messHist.Items = messages;
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
