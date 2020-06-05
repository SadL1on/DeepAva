using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;
using KursAuth.ViewModels.Messengers;
using ReactiveUI;
using System.Reactive.Disposables;

namespace KursAuth.Views.Messengers
{
    public class TlContacts : ReactiveUserControl<TlContVM>
    {
        private ListBox messHist => this.FindControl<ListBox>("MessHist");
        private ListBox сontactsTelegram => this.FindControl<ListBox>("ContactsTelegram");
        private TextBox MessageText => this.FindControl<TextBox>("MessageText");
        private Button SendMessage => this.FindControl<Button>("SendMessage");

        public TlContacts()
        {
            this.InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(x => x.сontactsTelegram.SelectedItem).InvokeCommand(ViewModel.GetMessHist);
                this.Bind(ViewModel, x => x.Messages, x => x.messHist.Items).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.MessageText, x => x.MessageText.Text).DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.SendMessage, x => x.SendMessage).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.SelItem, x => x.messHist.SelectedItem).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
