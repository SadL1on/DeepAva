using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;

namespace KursAuth.Views
{
    public class MainWindow : Window
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

        private void On_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string log = login.Text;
            string pass = password.Text;
            Vk vk = new Vk(log, pass);
            VkMain vkmain = new VkMain(vk);
            vkmain.Show();
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
           
        }
       
    }
}
