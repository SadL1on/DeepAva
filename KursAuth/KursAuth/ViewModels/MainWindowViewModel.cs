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
using System.Linq;

namespace KursAuth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private AutorizationVk vk;

        //private IEnumerable mess;

        //public IEnumerable Mess
        //{
        //    get => mess;
        //    set => this.RaiseAndSetIfChanged(ref mess, value);
        //}

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

        public void Auth(string login, string password)
        {
             vk = new AutorizationVk(login, password);
            var n = vk.api.IsAuthorized;
            // vk = vk.Authorization();
            Users = vk.GetFriends();
        }

        public void GetFriends()
        {
          //  
            
        }

        public void GetHisVM(long userId)
        {
            vk.GetHistory(userId).Messages.ToArray();
        }

        public void SendMessage(long userid, string text)
        {
            try
            {
                vk.SendMessage(vk, userid, text);
            }
            catch
            { }

        
        
        }

    }
}
