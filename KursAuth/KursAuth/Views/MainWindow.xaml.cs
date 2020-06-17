using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace KursAuth.Views
{
    public class MainWindow : ReactiveUserControl<MainWindowViewModel>
    {
        public Button vkOpen => this.FindControl<Button>("VkOpen");
        public Button tlOpen => this.FindControl<Button>("TlOpen");        

        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated((disposables => 
            {
                this.BindCommand(ViewModel, x => x.VkOpenCmd, x => x.vkOpen).DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.TlOpen, x => x.tlOpen).DisposeWith(disposables);
            }));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}