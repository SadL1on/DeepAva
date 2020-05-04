using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace KursAuth.Views.Messengers
{
    public class TlAuth : ReactiveUserControl <MainWindowViewModel>
    {
        private Button sendcode => this.FindControl<Button>("SendCode");
        private TextBox phone => this.FindControl<TextBox>("Phone");
        private Button ontl => this.FindControl<Button>("OnTl");
        private TextBox code => this.FindControl<TextBox>("Code");

      
        
        public TlAuth()
        {
            this.InitializeComponent();


           
            this.WhenActivated((disposables =>
            { this.BindCommand(ViewModel, x => x.VisPass, x => x.sendcode, phone.Text).DisposeWith(disposables); }));
            this.WhenActivated((disposable =>
            { this.BindCommand(ViewModel, x => x.AuthTl, x => x.ontl, code.Text).DisposeWith(disposable); }));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
