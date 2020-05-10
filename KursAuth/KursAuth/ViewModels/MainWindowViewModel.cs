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
using TeleSharp.TL.Messages;
using TeleSharp.TL;
using System.Runtime.Serialization;
using System.Reactive.Linq;

namespace KursAuth.ViewModels
{

    [DataContract]
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged, IScreen
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

        public ReactiveCommand<Unit, Task> MessLogin { get; }

        public ReactiveCommand<Unit, Unit> TlOpen { get; }

        public ReactiveCommand<string, Unit> VisPass { get; }

        public ReactiveCommand<string, Unit> AuthTl { get; }

        private readonly ReactiveCommand<Unit, Unit> _toTestView;

        public ICommand ToTestView => _toTestView;

        /// <summary>
        /// Логин из формы рег/авт приложения
        /// </summary>
        [Reactive]
        public string LoginMain { get; set; }
       

        /// <summary>
        /// Имя авторизовавшегося пользователя ВК
        /// </summary>
        [Reactive]
        public string NameVk { get; set; }

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

        [Reactive]
        public IEnumerable TelegramDialogs { get; set; }

        [Reactive]
        public IEnumerable Messages { get; set; }

        [Reactive]
        public bool IsVisAlertValid { get; set; }

        private RoutingState _router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public MainWindowViewModel()
        {
            ToMainAuthCmd = ReactiveCommand.Create(() => { IsVisMainReg = !IsVisMainReg; });
            AuthorizationMainCmd = ReactiveCommand.Create<string>(async (flag) => { await AuthorizationMainImpl(Convert.ToBoolean(flag)); });
            VkOpenCmd = ReactiveCommand.Create(() => { IsVisVkAuth = !IsVisVkAuth; });
            MessLogin = ReactiveCommand.Create(async () => { await AuthMessImpl(LoginMess, PassMess); });
            GetMessHist = ReactiveCommand.Create<long>(async (userID) => { await GetHisVMAsync(userID); });
            TlOpen = ReactiveCommand.Create(() => { IsVisTlAuth = !(IsVisTlAuth); });
            VisPass = ReactiveCommand.Create<string>(async (phone) => { await SendloginAsyncImpl(phone); });
            AuthTl = ReactiveCommand.Create<string>(async (code) => { await AuthTelegramImpl(code); });

            var canMoveToTest = this.WhenAnyObservable(x => x.Router.CurrentViewModel).Select(current => !(current is TestVM));
            _toTestView = ReactiveCommand.Create(() => { Router.Navigate.Execute(new TestVM()); }, canMoveToTest);
        }

        public async Task AuthTelegramImpl(string code)
        {
           
            await tl.MakeAuth(code);
            IsVisTlAuth = !IsVisTlAuth;
            IsVisContactsTelegram = !IsVisContactsTelegram;
            await GetFriendsTelegram();
        }

        public async Task SendloginAsyncImpl(string phone)
        {
            try
            {
                IsVisPass = !IsVisPass;
                tl = new Telegram();
                await tl.SendCodeToAuth(phone);
            }
            catch
            { 
            

            }
        }

        public async Task AuthorizationMainImpl(bool flag)
        {            
            user = new UserMain();
            var code = await user.MainAuthAsync(LoginMain, PassMain, flag);
            if (code != 0)
            {
                IsVisAlertValid = !IsVisAlertValid;
            }
            else
            {
                if (!flag) { IsVisMainReg = !IsVisMainReg; }
                else { IsVisMainAuth = !IsVisMainAuth; }
            }
        }

        public async Task AuthMessImpl(string login, string password)
        {
            vk = new VKModel();
            await vk.VkAuthAsync(login, password);
            IsVisConCtrl = !IsVisConCtrl;
            IsVisVkAuth = !IsVisVkAuth;
            _messenger = vk;
            await GetFriends();
            await GetUserInfo();
            
        }

        /// <summary>
        /// Список друзей ВКонтакте
        /// </summary>
        public async Task GetFriends()
        {
           Users = await vk.GetDialogsAsync();           
        }

        /// <summary>
        /// Информация об авторизовавшемся пользователе ВКонтакте
        /// </summary>
        public async Task GetUserInfo()
        {
          var  UserInfo = await vk.GetUserInfo();

            string firstname = UserInfo.FirstName;
            string lastname = UserInfo.LastName;

            NameVk = firstname + " " + lastname;           
        }

        /// <summary>
        /// Список друзей в телеграмме
        /// </summary>
        public async Task GetFriendsTelegram()
        {
           TelegramDialogs = await tl.GetFriendsAsync();
           
            //foreach (var element in TelegramDialogs.Users)
            //{
            //    if (element is TLUser)
            //    {
            //        TLUser chat = element as TLUser;
            //        Console.WriteLine(chat.FirstName);
            //    }
            //}
        }

        public async Task GetHisVMAsync(long userId)
        {
            var mess = await vk.GetHistoryAsync(userId); 
            Messages = mess.Messages.OrderBy(x => x.Date).ToArray();
        }


        /// <summary>
        /// Отправка сообщений ВКонтакте
        /// </summary>
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
