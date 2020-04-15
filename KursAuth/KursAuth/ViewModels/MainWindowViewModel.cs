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

        public AutorizationVk Auth(TextBox login, TextBox password)
        {
            string logintext = login.Text;
            string passwordtext = password.Text;

            AutorizationVk vk = new AutorizationVk();
            vk = vk.auth(logintext, passwordtext);

            return (vk);
        }

        public void GetFriends(AutorizationVk vk)
        {
            Users = vk.GetFriends(vk);
            this.vk = vk;
        }

        public void GetHisVM(User user, ListBox messHist)
        {
            var UserId = user.Id;
            var getHistory = vk.GetHistory(vk, UserId);
            var messages = getHistory.Messages.ToArray();
            for (int i = 0; i < getHistory.Messages.Count(); i++)
            {
                
                messHist.Items = messages[i].Text;
            }
        }

    }
}
