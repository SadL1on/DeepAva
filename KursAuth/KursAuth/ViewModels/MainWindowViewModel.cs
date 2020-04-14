using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VkNet;
using KursAuth.Models;
using KursAuth.Views;
using Avalonia.Controls;
using VkNet.Utils;
using VkNet.Model;
using System.Collections;

namespace KursAuth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IEnumerable users;


        public IEnumerable Users
        {
            get => users;
            set => this.RaiseAndSetIfChanged(ref users, value);
        }

        private bool _isVisConCtrl = false;
        public bool IsVisConCtrl
        {
            get => _isVisConCtrl;
            set => this.RaiseAndSetIfChanged(ref _isVisConCtrl, value);
        }

        private bool _isVisVkAuth = false;
        public bool IsVisVkAuth
        {
            get => _isVisVkAuth;
            set => this.RaiseAndSetIfChanged(ref _isVisVkAuth, value);
        }

        public  AutorizationVk Auth(TextBox login, TextBox password)
        {
            string logintext = login.Text;
            string passwordtext = password.Text;

            AutorizationVk vk = new AutorizationVk();
            vk = vk.auth(logintext, passwordtext);

            return (vk);
        }

        public void  GetFriends(AutorizationVk vk)
        {
            VkCollection<User> users = vk.GetFriends(vk);

            this.users = users;
           
        }

    }
}
