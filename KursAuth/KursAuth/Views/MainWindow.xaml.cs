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
        public Button vkOpen;
        public Button tlOpen;
        private ListBox contacts;
        

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            vkOpen = this.FindControl<Button>("VkOpen");
            tlOpen = this.FindControl<Button>("TlOpen");
            vkOpen.Click += VkOpen_Click;
            //tlOpen.Click += TlOpen_Click;
            this.WhenActivated((disposables =>
            { this.BindCommand(ViewModel, x => x.TlOpen, x => x.tlOpen).DisposeWith(disposables); }));
        }

        //private void TlOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    ViewModel.IsVisTlAuth = !(ViewModel.IsVisTlAuth);
        //}

        private void VkOpen_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IsVisVkAuth = !(ViewModel.IsVisVkAuth);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            contacts = this.FindControl<ListBox>("contacts");
        }

    }
}