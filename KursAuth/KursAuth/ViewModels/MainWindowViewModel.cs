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


        public static VkApi Auth(TextBox login, TextBox password)
        {

            string logintext = login.Text;
            string passwordtext = password.Text;

            Vk vk = new Vk();
            VkApi api = vk.auth(logintext, passwordtext);



            return (api);



        }
            public string Greeting => "Welcome to Avalonia!";
            private bool _isVisRegControl = false;
            public bool IsVisRegControl
            {
                get => _isVisRegControl;
                set => this.RaiseAndSetIfChanged(ref _isVisRegControl, value);
            }

    }
}
