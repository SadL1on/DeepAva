using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Interactivity;
using KursAuth.Models;
using KursAuth.ViewModels;
using System;

namespace KursAuth.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public TextBox login;
        public TextBox password;
        public Button auth;
        public Button reg;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            auth = this.FindControl<Button>("On");
            login = this.FindControl<TextBox>("Login");
            password = this.FindControl<TextBox>("Password");
            auth.Click += On_Click;

            reg = this.FindControl<Button>("Reg");
            reg.Click += Reg_Click;
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IsVisRegControl = !(ViewModel.IsVisRegControl);
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pass = password.Text;
            Authorization auth = new Authorization(log, pass);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
           
        }
       
    }
}
