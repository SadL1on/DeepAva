﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;

namespace KursAuth.Views
{
    public class MainAuth : ReactiveUserControl<MainWindowViewModel>
    {
        private Button _mainAuth;
        private Button _mainAuthReg;
        private TextBox _login;
        private TextBox _password;
        private TextBlock _alert;
        private bool _flag;

        public MainAuth()
        {
            InitializeComponent();
            _mainAuth = this.FindControl<Button>("MainAuth");
            _mainAuthReg = this.FindControl<Button>("MainAuthReg");
            _login = this.FindControl<TextBox>("Login");
            _password = this.FindControl<TextBox>("Pass");
            _alert = this.FindControl<TextBlock>("Alert");
            _mainAuth.Click += _mainAuth_Click;
            _mainAuthReg.Click += _mainAuthReg_Click;
        }

        private void _mainAuthReg_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ViewModel.IsVisMainReg = !(ViewModel.IsVisMainReg);
        }

        private void _mainAuth_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           // try
          //  {
                _flag = true;
                ViewModel.Login(_login.Text, _password.Text, _flag);
                ViewModel.IsVisMainAuth = !(ViewModel.IsVisMainAuth);
            //}
            //catch
            //{
                _login.Text = null;
                _password.Text = null;
                _alert.Text = "Неправильный логин или пароль";
        //    }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
