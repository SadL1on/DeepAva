﻿using ReactiveUI;
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
using System.Net;
using System.IO;
using KursAuth.Models.Main;
using System.Reactive;
using System.Windows.Input;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;

namespace KursAuth.ViewModels
{

    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private UserMain user;
        private AutorizationVk vk;

        public ReactiveCommand<Unit, Unit> BackToAuth { get; }
        public ReactiveCommand<Unit, Unit> Registration { get; }

        public MainWindowViewModel()
        {
            BackToAuth = ReactiveCommand.Create(() => { ChangePageMeth(); });
            Registration = ReactiveCommand.Create(() => { RegistrationMeth(); });

        }

        [Reactive] public string LoginR { get; set; }

        private string _pass;
        public string Pass
        {
            get => _pass;
            set => this.RaiseAndSetIfChanged(ref _pass, value);
        }

        public void ChangePageMeth()
        {            
            IsVisMainReg = !IsVisMainReg;
        }

        public void RegistrationMeth()
        {
            var st = LoginR;
        }

        private IEnumerable users;
        public IEnumerable Users
        {
            get => users;
            set => this.RaiseAndSetIfChanged(ref users, value);
        }

        private bool _isVisMainReg = false;
        public bool IsVisMainReg
        {
            get => _isVisMainReg;
            set => this.RaiseAndSetIfChanged(ref _isVisMainReg, value);
        }

        private bool _isVisMainAuth = false;
        public bool IsVisMainAuth
        {
            get => _isVisMainAuth;
            set => this.RaiseAndSetIfChanged(ref _isVisMainAuth, value);
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
        }

        public async Task GetFriends()
        {
            Users = await vk.GetFriendsAsync();
        }

        public async Task<Message[]> GetHisVMAsync(long userId)
        {
            var mess = await vk.GetHistoryAsync(userId); 
            return mess.Messages.OrderBy(x => x.Date).ToArray();
        }

        public async Task SendMessage(long userid, string text)
        {
            try
            {
                await vk.SendMessageAsync(userid, text);
            }
            catch
            { }
        }

        public void Login(string log, string pass, bool flag)
        {
            user = new UserMain(log, pass, flag);
        }

    }
}
