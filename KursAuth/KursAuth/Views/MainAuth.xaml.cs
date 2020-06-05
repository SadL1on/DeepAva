using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace KursAuth.Views
{
    public class MainAuth : ReactiveUserControl<MainWindowViewModel>
    {
        private Button mainAuth => this.FindControl<Button>("MainAuth");
        private Button mainAuthReg => this.FindControl<Button>("MainAuthReg");
        private TextBox login => this.FindControl<TextBox>("Login");
        private TextBox password => this.FindControl<TextBox>("Pass");
        private TextBlock alert => this.FindControl<TextBlock>("Alert");

        public MainAuth()
        {
            InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.LoginMain, x => x.login.Text).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.PassMain, x => x.password.Text).DisposeWith(disposables);
              //  this.Bind(ViewModel, x => x.IsVisAlertValid, x => x.alert.Text).DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.ToMainAuthCmd, x => x.mainAuthReg).DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.AuthorizationMainCmd, x => x.mainAuth).DisposeWith(disposables);
            });
            alert.Text = "Неверный логин или пароль";
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
