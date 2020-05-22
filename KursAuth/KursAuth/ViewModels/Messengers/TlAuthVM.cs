using KursAuth.Models.Telegram;
using KursAuth.Utils.Messages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KursAuth.ViewModels.Messengers
{
    public class TlAuthVM : ViewModelBase, IRoutableViewModel
    {
        private Telegram tl;

        public string UrlPathSegment => "/tlAuth";

        public IScreen HostScreen { get; }

        public ReactiveCommand<string, Unit> VisPass { get; }

        public ReactiveCommand<string, Unit> AuthTl { get; }

        [Reactive]
        public bool IsVisTlAuth { get; set; }

        [Reactive]
        public bool IsVisPass { get; set; }

        [Reactive]
        public bool IsVisContactsTelegram { get; set; }

        public TlAuthVM()
        {
            tl = Telegram.GetInstance();
            VisPass = ReactiveCommand.Create<string>(async (phone) => { await SendloginAsyncImpl(phone); });
            AuthTl = ReactiveCommand.Create<string>(async (code) => { await AuthTelegramImpl(code); });
        }

        public async Task SendloginAsyncImpl(string phone)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(phone))
                {
                    IsVisPass = !IsVisPass;
                    await tl.SendCodeToAuth(phone);
                }
                else { throw new Exception() ; }
            }
            catch { }
        }

        public async Task AuthTelegramImpl(string code)
        {
            await tl.MakeAuth(code);

            if (tl.IsAuth)
            {
                MessageBus.Current.SendMessage(new RouteToTlContMessage());
            }
        }
    }
}