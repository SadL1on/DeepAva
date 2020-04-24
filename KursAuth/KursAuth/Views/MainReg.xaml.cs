using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;

namespace KursAuth.Views
{
    public class MainReg : ReactiveUserControl<MainWindowViewModel>
    {
        private Button _register;
        private Button _back;
        private TextBox _regLog;
        private TextBox _regPass;
        private bool _flag;

        public MainReg()
        {
            InitializeComponent();
            _register = this.FindControl<Button>("RegAcc");
            _back = this.FindControl<Button>("Back");
            _regLog = this.FindControl<TextBox>("LogReg");
            _regPass = this.FindControl<TextBox>("PassReg");
            _register.Click += _register_Click;
            _back.Click += _back_Click;
        }

        private void _back_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ViewModel.IsVisMainReg = !(ViewModel.IsVisMainReg);
        }

        private void _register_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _flag = false;
            ViewModel.Login(_regLog.Text, _regPass.Text, _flag);
            ViewModel.IsVisMainReg = !(ViewModel.IsVisMainReg);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
