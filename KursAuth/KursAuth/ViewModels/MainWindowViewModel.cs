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
using KursAuth.Interfaces;

namespace KursAuth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private AutorizationVk vk;
        private readonly IUserService _userservice;

        public MainWindowViewModel(IUserService userService)
        {
            _userservice = userService;
        }

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
            _userservice.Authorization(login, password);
        }

        public void GetFriends()
        {
            Users = _userservice.GetFriends();
        }

        //public Message[] GetHistoryVM(long UserId)
        //{
        //    return vk.GetHistory(UserId).Messages.ToArray();          
        //}

    }
}
