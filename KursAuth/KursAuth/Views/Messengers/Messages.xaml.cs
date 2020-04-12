using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KursAuth.Views.Messengers
{
    public class Messages : UserControl
    {
        public Messages()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
