using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;

namespace KursAuth.Views
{
    public class MainAuth : ReactiveUserControl<MainWindowViewModel>
    {
        private Button _mainAuth;
        private TextBox _login;
        private TextBox _password;

        public MainAuth()
        {
            this.InitializeComponent();
            _mainAuth = this.FindControl<Button>("MainAuth");
            _login = this.FindControl<TextBox>("Login");
            _password = this.FindControl<TextBox>("Pass");
            _mainAuth.Click += _mainAuth_Click;
        }

        //TODO Переделать в регистрацию
        private void _mainAuth_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ViewModel.Login(_login.Text, _password.Text);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
