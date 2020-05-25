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
    public class TlAuth : ReactiveUserControl <TlAuthVM>
    {
        private Button sendcode => this.FindControl<Button>("SendCode");
        private TextBox phone => this.FindControl<TextBox>("Phone");
        private Button ontl => this.FindControl<Button>("OnTl");
        private TextBox code => this.FindControl<TextBox>("Code");
          
        public TlAuth()
        {
            this.InitializeComponent();
         
            this.WhenActivated((disposables =>
            { 
              this.BindCommand(ViewModel, x => x.VisPass, x => x.sendcode, phone.Text).DisposeWith(disposables); 
              this.BindCommand(ViewModel, x => x.AuthTl, x => x.ontl, code.Text).DisposeWith(disposables); 
            }));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
