using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;
using ReactiveUI;
using System.Collections;
using System.Reactive.Disposables;
using System.Threading;
using VkNet.Model;

namespace KursAuth.Views.Messengers
{
    public class Contacts : ReactiveUserControl<MainWindowViewModel>
    {
        //public static readonly DirectProperty<Contacts, IEnumerable> ItemsProperty =
        //    AvaloniaProperty.RegisterDirect<Contacts, IEnumerable>(
        //        nameof(Items),
        //        o => o.Items,
        //        (o, v) => o.Items = v);

        //private IEnumerable _items = new AvaloniaList<object>();

        //public IEnumerable Items
        //{
        //    get => _items;
        //    set => SetAndRaise(ItemsProperty, ref _items, value);
        //}

        private ListBox contacts => this.FindControl<ListBox>("Contacts");
        private ListBox MessHist => this.FindControl<ListBox>("MessHist");
        private Avalonia.Controls.Button SendMessage => this.FindControl<Avalonia.Controls.Button>("SendMessage");
        private TextBlock title => this.FindControl<TextBlock>("Title");
        private TextBox MessageText => this.FindControl<TextBox>("MessageText");
        private Avalonia.Controls.Button sendmessage => this.FindControl<Avalonia.Controls.Button>("SendMessage");
        private User user;

        public Contacts()
        {
            this.InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.NameVk, x => x.title.Text);
                this.Bind(ViewModel, x => x.Messages, x => x.MessHist.Items).DisposeWith(disposables);
                
                //Листбокс и команда не биндятся
                //  this.BindCommand(ViewModel, x => x.GetMessHist, x => x.contacts.).DisposeWith(disposables);
            });
            contacts.Tapped += Contacts_Tapped;
        }

        private async void  Contacts_Tapped(object sender, RoutedEventArgs e)
        {
            user = (User)contacts.SelectedItem;
            await ViewModel.GetHisVMAsync(user.Id);
            sendmessage.Click += Sendmessage_Click;
        }

        private async void Sendmessage_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SendMessage(user.Id, MessageText.Text);
            MessageText.Text = null;
            ViewModel.IsVisConCtrl = !(ViewModel.IsVisConCtrl);
            ViewModel.IsVisConCtrl = !(ViewModel.IsVisConCtrl);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
