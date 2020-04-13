using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KursAuth.Views
{
    public class MainAuth : UserControl
    {
        public MainAuth()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
