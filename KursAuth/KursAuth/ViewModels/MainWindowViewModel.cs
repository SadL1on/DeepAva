using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KursAuth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        
            public string Greeting => "Welcome to Avalonia!";
            private bool _isVisRegControl = false;
            public bool IsVisRegControl
            {
                get => _isVisRegControl;
                set => this.RaiseAndSetIfChanged(ref _isVisRegControl, value);
            }

    }
}
