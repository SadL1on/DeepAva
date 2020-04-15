﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
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
            try
            {
                AutorizationVk vk = ViewModel.Auth(login, password);
                ViewModel.IsVisConCtrl = !(ViewModel.IsVisConCtrl);
                ViewModel.IsVisVkAuth = !(ViewModel.IsVisVkAuth);
                ViewModel.GetFriends(vk);
                
            }
            catch
            {
                login.Text = null;
                password.Text = null;
            }
            
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
