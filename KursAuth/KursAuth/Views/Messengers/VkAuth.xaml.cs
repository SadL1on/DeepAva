﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Interfaces;
using KursAuth.Models;
using KursAuth.ViewModels;

namespace KursAuth.Views.Messengers
{
    public class VkAuth : ReactiveUserControl<MainWindowViewModel>
    {
        private TextBox login;
        private TextBox password;
        private Button on;
        //private ListBox contacts;

//public VkAuth()
//        {

//        }

        public VkAuth()
        {
            this.InitializeComponent();

            login = this.FindControl<TextBox>("Login");
            password = this.FindControl<TextBox>("Password");
            on = this.FindControl<Button>("On");
            //contacts = this.FindControl<ListBox>("Contacts");
            on.Click += On_Click1;
        }

        private void On_Click1(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //try
            //{
                var Login = login.Text;
                var Password = password.Text;
                ViewModel.Auth(Login, Password);
                ViewModel.IsVisConCtrl = !(ViewModel.IsVisConCtrl);
                ViewModel.IsVisVkAuth = !(ViewModel.IsVisVkAuth);
                ViewModel.GetFriends();
                
            //}
            //catch
            //{
            //    login.Text = null;
            //    password.Text = null;
            //}
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
