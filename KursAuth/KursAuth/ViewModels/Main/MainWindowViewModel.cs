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
        

        private bool _isVisVkMainControl = false;
        public bool IsVisVkMainControl
        {
            get => _isVisVkMainControl;
            set => this.RaiseAndSetIfChanged(ref _isVisVkMainControl, value);
        }


    }
}
