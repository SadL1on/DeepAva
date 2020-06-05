using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;
using KursAuth.ViewModels.Messengers;
using ReactiveUI;
using System.Collections;
using System.Reactive.Disposables;
using System.Threading;


namespace KursAuth.Views.Messengers
{
    public class VkContacts : ReactiveUserControl<VkContVM>
    {

        private ListBox contacts => this.FindControl<ListBox>("Contacts");
        private ListBox messHist => this.FindControl<ListBox>("MessHist");
        private Button SendMessage => this.FindControl<Button>("SendMessage");
        private TextBox MessageText => this.FindControl<TextBox>("MessageText");

        public VkContacts()
        {
            this.InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.contacts.SelectedItem).InvokeCommand(ViewModel.GetMessHist);
                this.Bind(ViewModel, x => x.Messages, x => x.messHist.Items).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.SelItem, x => x.messHist.SelectedItem).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.MessageText, x => x.MessageText.Text).DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.SendMessage, x => x.SendMessage).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
