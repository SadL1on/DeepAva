using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VkNet;
using KursAuth.Models;
using KursAuth.Views;
using Avalonia.Controls;

namespace KursAuth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        

        private bool _isVisRegControl = false;
        public bool IsVisRegControl
        {
            get => _isVisRegControl;
            set => this.RaiseAndSetIfChanged(ref _isVisRegControl, value);
        }

    }
}
