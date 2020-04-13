using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;

namespace KursAuth.Views.Messengers
{
    public class Messages : ReactiveUserControl<MainWindowViewModel>
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
