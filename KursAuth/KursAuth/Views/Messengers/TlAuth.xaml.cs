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
        private TextBox password => this.FindControl<TextBox>("PasswordTl");
        private Button sendpass => this.FindControl<Button>("OnTl");
        
        public TlAuth()
        {
            this.InitializeComponent();


            this.WhenActivated((disposables =>
            { this.BindCommand(ViewModel, x => x.VisPass, x => x.sendpass).DisposeWith(disposables); }));

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
