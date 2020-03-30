using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;
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
        public Avalonia.Controls.Button on;
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
            string logintext = login.Text;
            string passwordtext = password.Text;

            try
            {
                Vk vk = new Vk();
                VkApi api = vk.auth(logintext, passwordtext);


                VkMain vkmain = new VkMain(api);
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