using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VkNet;

namespace KursAuth.Views
{
    public class MyMessages : Window
    {
        public MyMessages()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public MyMessages(VkApi api)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
