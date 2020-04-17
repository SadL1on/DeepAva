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
           // var n = vk.api.IsAuthorized;
            
        }

        public void GetFriends()
        {
            Users = vk.GetFriends();
        }

        public Message[] GetHisVM(long userId)
        {
            var id = vk.api.UserId;
            var mess = vk.GetHistory(userId).Messages.OrderBy(x => x.Date).ToArray();
            //for (int i = 0; i < mess.Length; i++)
            //{
            //    mess[i].Text = mess[i].Text + "   " + mess[i]
            //}
            return mess;
        }

        public void SendMessage(long userid, string text)
        {
            try
            {
                vk.SendMessage(userid, text);
            }
            catch
            { }

        
        
        }

    }
}
