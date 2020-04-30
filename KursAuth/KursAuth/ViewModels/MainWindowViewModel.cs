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
using System.Net;
using System.IO;
using KursAuth.Models.Main;
using System.Reactive;
using System.Windows.Input;
using System.Threading.Tasks;

namespace KursAuth.ViewModels
{

    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private UserMain user;
        private AutorizationVk vk;

        public ReactiveCommand<Unit, Unit> BackToAuth { get; }
        public ReactiveCommand<string, Unit> Registration { get; }

        public MainWindowViewModel()
        {
            BackToAuth = ReactiveCommand.Create(() => { ChangePageMeth(); });
            Registration = ReactiveCommand.Create<string>(RegistrationMeth);

        }


        public void ChangePageMeth()
        {            
            IsVisMainReg = !IsVisMainReg;
        }

        public void RegistrationMeth(string arg)
        {
            
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

        public Message[] GetHisVM(long userId)
        {
            var mess = vk.GetHistoryAsync(userId).Result.Messages.OrderBy(x => x.Date).ToArray();
            return mess;
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
