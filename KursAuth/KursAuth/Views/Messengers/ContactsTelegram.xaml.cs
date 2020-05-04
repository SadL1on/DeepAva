using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KursAuth.Views.Messengers
{
    public class ContactsTelegram : UserControl
    {
        public ContactsTelegram()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
