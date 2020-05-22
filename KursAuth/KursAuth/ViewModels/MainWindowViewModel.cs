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
using KursAuth.ViewModels.Messengers;
using KursAuth.Views.Messengers;
using KursAuth.Utils.Messages;

namespace KursAuth.ViewModels
{

    [DataContract]
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged, IScreen, IRoutableViewModel
    {
        private UserMain user;

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
        private readonly ReactiveCommand<Unit, Unit> vkOpenCmd;

        private readonly ReactiveCommand<Unit, Unit> tlOpen;

        private readonly ReactiveCommand<Unit, Unit> toTestView;

        public ICommand ToTestView => toTestView;
        public ICommand VkOpenCmd => vkOpenCmd;
        public ICommand TlOpen => tlOpen;

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

        [Reactive]
        public bool IsVisAlertValid { get; set; }

        private RoutingState _router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }
        
        public string UrlPathSegment => "/main";

        public IScreen HostScreen { get; }

        public MainWindowViewModel()
        {
            ToMainAuthCmd = ReactiveCommand.Create(() => { IsVisMainReg = !IsVisMainReg; });
            AuthorizationMainCmd = ReactiveCommand.Create<string>(async (flag) => { await AuthorizationMainImpl(Convert.ToBoolean(flag)); });           

            var canMoveToVkOpen = this.WhenAnyObservable(x => x.Router.CurrentViewModel).Select(current => !(current is VkAuthVM || current is VkContVM));
            vkOpenCmd = ReactiveCommand.Create(() => { Router.Navigate.Execute(new VkAuthVM()); }, canMoveToVkOpen);

            var canMoveToTlOpen = this.WhenAnyObservable(x => x.Router.CurrentViewModel).Select(current => !(current is TlAuthVM));
            tlOpen = ReactiveCommand.Create(() => { Router.Navigate.Execute(new TlAuthVM()); }, canMoveToTlOpen);

            var canMoveToTest = this.WhenAnyObservable(x => x.Router.CurrentViewModel).Select(current => !(current is TestVM));
            toTestView = ReactiveCommand.Create(() => { Router.Navigate.Execute(new TestVM()); }, canMoveToTest);

            MessageBus.Current.Listen<RouteToVkContMessage>().Subscribe(Observer.Create<RouteToVkContMessage>((e) => { Router.Navigate.Execute(new VkContVM()); }));
            MessageBus.Current.Listen<RouteToTlContMessage>().Subscribe(Observer.Create<RouteToTlContMessage>((e) => { Router.Navigate.Execute(new TlContVM()); }));
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
    }
}
