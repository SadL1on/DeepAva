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
using ReactiveUI.Fody.Helpers;
using KursAuth.Models.Telegram;
using KursAuth.Utils;

namespace KursAuth.ViewModels
{

    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private UserMain user;
        private Telegram tl;
        private VKModel vk;
         
        private IMessengers _messenger;

        /// <summary>
        /// Команда перехода к форме авторизации
        /// </summary>
        public ReactiveCommand<Unit, Unit> ToMainAuthCmd { get; }

        /// <summary>
        /// Команда регистрации в приложении
        /// </summary>
        public ReactiveCommand<string, Unit> AuthorizationMainCmd { get; }

        /// <summary>
        /// Команда открытия окна ВК
        /// </summary>
        public ReactiveCommand<Unit, Unit> VkOpenCmd { get; }

        public ReactiveCommand<long, Unit> GetMessHist { get; }

        public ReactiveCommand<Unit, Unit> MessLogin { get; }

        public ReactiveCommand<Unit, Unit> TlOpen { get; }

        public ReactiveCommand<string, Unit> VisPass { get; }

        public ReactiveCommand<string, Unit> AuthTl { get; }

        /// <summary>
        /// Логин из формы рег/авт приложения
        /// </summary>
        [Reactive] 
        public string LoginMain { get; set; }

        /// <summary>
        /// Пароль из формы рег/авт приложения
        /// </summary>
        [Reactive] 
        public string PassMain { get; set; }

        [Reactive]
        public bool IsVisContactsTelegram { get; set; }
        
        [Reactive]
        public bool IsVisMess { get; set; }

        [Reactive]
        public bool IsVisTlAuth { get; set; }
      
        [Reactive]
        public bool IsVisPass { get; set; }
        /// <summary>
        /// Логин из формы рег/авт в мессенджере
        /// </summary>
        [Reactive]
        public string LoginMess { get; set; }

        /// <summary>
        /// Пароль из формы рег/авт в мессенджере
        /// </summary>
        [Reactive]
        public string PassMess { get; set; }

        /// <summary>
        /// Видимость формы регистрации в приложении
        /// </summary>
        [Reactive] 
        public bool IsVisMainReg { get; set; }

        /// <summary>
        /// Видимость формы авторизации в приложении
        /// </summary>
        [Reactive] 
        public bool IsVisMainAuth { get; set; }

        /// <summary>
        /// Видимость формы списка контактов
        /// </summary>
        [Reactive] 
        public bool IsVisConCtrl { get; set; }

        /// <summary>
        /// Видимость формы авторизации ВК
        /// </summary>
        [Reactive] 
        public bool IsVisVkAuth { get; set; }

        /// <summary>
        /// Список контактов
        /// </summary>
        [Reactive]
        public IEnumerable Users { get; set; }

        public MainWindowViewModel()
        {
            ToMainAuthCmd = ReactiveCommand.Create(() => { IsVisMainReg = !IsVisMainReg; });
            AuthorizationMainCmd = ReactiveCommand.Create<string>((flag) => { AuthorizationMainImpl(Convert.ToBoolean(flag)); });
            VkOpenCmd = ReactiveCommand.Create(() => { IsVisVkAuth = !IsVisVkAuth; });
            MessLogin = ReactiveCommand.Create(() => { AuthMessImpl(LoginMess, PassMess); });
            GetMessHist = ReactiveCommand.Create<long>((userID) => { GetHisVMAsync(userID); });
            TlOpen = ReactiveCommand.Create(() => { IsVisTlAuth = !(IsVisTlAuth); });
            VisPass = ReactiveCommand.Create<string>(async (phone) => { await sendloginAsync(phone); });
            AuthTl = ReactiveCommand.Create<string>(async (code) => { await AuthTelegram(code); });
        }

        public async Task AuthTelegram(string code)
        {
            IsVisTlAuth = !IsVisTlAuth;
            IsVisContactsTelegram = !IsVisContactsTelegram;
            await tl.MakeAuth(code);
        }
        public async Task sendloginAsync(string phone)
        {
            IsVisPass = !IsVisPass;
            tl = new Telegram();
            await tl.SendCodeToAuth(phone);
        }

        public void AuthorizationMainImpl(bool flag)
        {            
            user = new UserMain(LoginMain, PassMain, flag);
            if (!flag) { IsVisMainReg = !IsVisMainReg; }
            else { IsVisMainAuth = !IsVisMainAuth; }
        }

        public async Task AuthMessImpl(string login, string password)
        {
            vk = new VKModel(login, password);
            IsVisConCtrl = !IsVisConCtrl;
            IsVisVkAuth = !IsVisVkAuth;
            _messenger = vk;
            await GetFriends();
        }

        public async Task GetFriends()
        {
            Users = await vk.GetFriendsAsync();
        }

        [Reactive]
        public IEnumerable Messages { get; set; }

        public async Task GetHisVMAsync(long userId)
        {
            var mess = await vk.GetHistoryAsync(userId); 
            Messages = mess.Messages.OrderBy(x => x.Date).ToArray();
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
    }
}
