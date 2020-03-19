using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;

namespace KursAuth.Views
{
    public class VkMain : Window
    {
        Vk vk;
        Button send;
        TextBox message;
        string messagetext;
        public VkMain()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public VkMain(Vk vk)
        {
            
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.vk = vk;
            message = this.FindControl<TextBox>("Message");
            messagetext = message.Text;

            send = this.FindControl<Button>("Send");
            send.Click += Send_Click1;

        }

        private void Send_Click1(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            vk.Message(messagetext);
        }

      

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
