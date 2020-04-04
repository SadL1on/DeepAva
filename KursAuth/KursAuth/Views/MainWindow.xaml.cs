using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;
using KursAuth.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Model;

namespace KursAuth.Views
{
    public class MainWindow : Window
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
            on = this.FindControl<Avalonia.Controls.Button>("On");
            login = this.FindControl<TextBox>("Login");
            password = this.FindControl<TextBox>("Password");



            on.Click += On_Click;


        }

        private void On_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

            try
            {
                Vk vk = ViewModels.MainWindowViewModel.Auth(login,password);
                VkMain vkmain = new VkMain(vk);
                vkmain.Show();
                this.Close();
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