using KursAuth.Models.Main;
using KursAuth.Utils.Messages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KursAuth.ViewModels
{
    [DataContract]
    public class MainAuthVM : ViewModelBase, IRoutableViewModel, IScreen
    {
        public string UrlPathSegment => "/Auth";

        public IScreen HostScreen { get; }

        private UserMain user;

        /// <summary>
        /// Команда регистрации в приложении
        /// </summary>
        public ReactiveCommand<string, Unit> AuthorizationMainCmd { get; }

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
        public bool IsVisAlertValid { get; set; }

        private RoutingState _router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public MainAuthVM()
        {
            AuthorizationMainCmd = ReactiveCommand.Create<string>(async (flag) => { await AuthorizationMainImpl(Convert.ToBoolean(flag)); });
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
                MessageBus.Current.SendMessage(new RouteToMain());
            }
        }
    }
}
