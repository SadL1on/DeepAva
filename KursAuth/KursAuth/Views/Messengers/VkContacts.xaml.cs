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
using VkNet.Model;

namespace KursAuth.Views.Messengers
{
    public class VkContacts : ReactiveUserControl<VkContVM>
    {

        private ListBox contacts => this.FindControl<ListBox>("Contacts");
        private ListBox messHist => this.FindControl<ListBox>("MessHist");
        private TextBlock DialogsName => this.FindControl<TextBlock>("DialogsName");

        public VkContacts()
        {
            this.InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.contacts.SelectedItem).InvokeCommand(ViewModel.GetMessHist);
                this.Bind(ViewModel, x => x.Messages, x => x.messHist.Items).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
    }
}
