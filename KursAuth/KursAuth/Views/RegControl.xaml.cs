using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KursAuth.Views
{
    public class RegControl : UserControl
    {
        public RegControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
