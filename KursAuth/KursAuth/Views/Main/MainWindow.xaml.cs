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
        public Button on;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            on = this.FindControl<Button>("On");
            login = this.FindControl<TextBox>("Login");
            password = this.FindControl<TextBox>("Password");
            on.Click += On_Click;
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutorizationVk vk = VkVeiwModel.Auth(login,password);
                VkMain vkmain = new VkMain(vk);
                vkmain.Show();
                Close();
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