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

namespace KursAuth.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public Button vkOpen;
        private ListBox contacts;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            vkOpen = this.FindControl<Button>("VkOpen");
            vkOpen.Click += VkOpen_Click;
        }

        private void VkOpen_Click(object sender, RoutedEventArgs e)
        {
            // ViewModel.IsVisConCtrl = !(ViewModel.IsVisConCtrl);
            ViewModel.IsVisVkAuth = !(ViewModel.IsVisVkAuth);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            contacts = this.FindControl<ListBox>("contacts");
        }

    }
}