using KursAuth.Models.Telegram;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;

namespace KursAuth.ViewModels.Messengers
{
    public class TlContVM : ViewModelBase, IRoutableViewModel
    {
        private Telegram tl;

        public string UrlPathSegment => "/tlCont";

        public IScreen HostScreen { get; }

        [Reactive]
        public IEnumerable TelegramDialogs { get; set; }

        [Reactive]
        public IEnumerable Messages { get; set; }

        public ReactiveCommand<TLUser, Unit> GetMessHist { get; }

        [Reactive]
        public bool IsVisRecip { get; set; }

        [Reactive]
        public string RecipTitle { get; set; }

        [Reactive]
        public bool IsVisSendMess { get; set; }

        [Reactive]
        public Models.VK.Message SelItem { get; set; }

        public TlContVM()
        {
            tl = Telegram.GetInstance();
            GetFriendsTelegram();
            GetMessHist = ReactiveCommand.Create<TLUser>(async (selectedItem) => { await GetHisVMAsync(selectedItem); });
        }

        public async Task GetHisVMAsync(TLUser selectedItem)
        {
            if (selectedItem == null)
                return;

            var hist = await tl.GetHistory(selectedItem.Id);
            Messages = hist.Messages.ToArray();
            IsVisSendMess = true;
           // SelItem = ms[ms.Length - 1];
        }

        /// <summary>
        /// Список друзей в телеграмме
        /// </summary>
        public async Task GetFriendsTelegram()
        {
            TelegramDialogs = await tl.GetFriendsAsync();
        }
    }
}
