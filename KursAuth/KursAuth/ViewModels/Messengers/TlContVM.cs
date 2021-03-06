﻿using KursAuth.Models.Telegram;
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
using TeleSharp.TL.Messages;

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
        TLUser UserInDialog { get; set; }

        [Reactive]
        public string MessageText { get; set; }
        public ReactiveCommand<TLUser, Unit> GetMessHist { get; }
        public ReactiveCommand<Unit,Task> SendMessage { get; }

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
            SendMessage = ReactiveCommand.Create(async () => { await SendMessageAsync(); });
        }

        public async Task GetHisVMAsync(TLUser selectedItem)
        {
            UserInDialog = selectedItem;
            if (selectedItem == null)
                return;
            var hist = await tl.GetHistory(selectedItem.Id);
            var meshist = hist.Messages.ToArray();
            Message[] History = new Message[hist.Messages.Count];
            //foreach (var msg in Messages)
            for (int i = 0; i < hist.Messages.Count; i++)
            {
                if (hist.Messages[i] is TLMessage)
                {
                    Message mes = new Message();
                    TLMessage sms = hist.Messages[i] as TLMessage;
                    mes.Text = sms.Message;
                    mes.Date = sms.Date;
                    if (sms.FromId.Value != tl.client.Session.TLUser.Id)
                    {
                        mes.Alignment = "Left";
                    }
                    else
                    {
                        mes.Alignment = "Right";
                    }
                    History[i] = mes;
                }
                if (hist.Messages[i] is TLMessageService)
                    continue;
            }
            Messages = History.OrderBy(x => x.Date);
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

        public async Task SendMessageAsync()
        {
            tl.SendMessage(MessageText, UserInDialog.Phone);

        }
    }
}
