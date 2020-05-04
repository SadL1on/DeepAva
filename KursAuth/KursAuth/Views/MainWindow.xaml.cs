using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Interactivity;
using KursAuth.Models;
using KursAuth.ViewModels;
using System;
using KursAuth.Views.Messengers;
using ReactiveUI;
using System.Reactive.Disposables;

namespace KursAuth.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public Button vkOpen => this.FindControl<Button>("VkOpen");
        public Button tlOpen => this.FindControl<Button>("TlOpen");        

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
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