using DynamicData;
using KursAuth.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace KursAuth.ViewModels.Messengers
{
    public class VkContVM : ViewModelBase, IRoutableViewModel
    {
        public string UrlPathSegment => "/vkCont";

        public IScreen HostScreen { get; }

        private VKModel vk;

        /// <summary>
        /// Список диалогов
        /// </summary>
        [Reactive]
        public IEnumerable Dialogs { get; set; }
       
        [Reactive]
        public IEnumerable DialogsName { get; set; }

        [Reactive]
        public IEnumerable Messages { get; set; }

        /// <summary>
        /// Имя авторизовавшегося пользователя ВК
        /// </summary>
        [Reactive]
        public string NameVk { get; set; }

        public ReactiveCommand<ConversationAndLastMessage, Unit> GetMessHist { get; }

        public VkContVM()
        {
            vk = VKModel.GetInstance();
            GetFriends();
            //GetUserInfo();
            GetMessHist = ReactiveCommand.Create<ConversationAndLastMessage>(async (selectedItem) => { await GetHisVMAsync(selectedItem); });
        }

        public async Task GetHisVMAsync(ConversationAndLastMessage selectedItem)
        {
            if (selectedItem == null)
                return;
            
            // var mess = await vk.GetHistoryAsync(selectedindex.);
            // Messages = mess.Messages.OrderBy(x => x.Date).ToArray();
            var Messages1 = vk.GetMessagesByUserId(selectedItem.Conversation.Peer.Id).OrderBy(x=>x.Date).ToArray();
            Models.VK.Message[] ms = new Models.VK.Message[Messages1.Length];
            for (int i = 0; i < Messages1.Length; i++)
            {
                Models.VK.Message mes = new Models.VK.Message();
                mes.Text = Messages1[i].Text;
                if(Messages1[i].FromId == selectedItem.Conversation.Peer.Id)
                {
                    mes.Alignment = "Left";

                }
                else
                {

                    mes.Alignment = "Right";
                }

                ms[i] = mes; 
            
            }
            Messages = ms;
        }

        /// <summary>
        /// Список друзей ВКонтакте
        /// </summary>
        public async Task GetFriends()
        {
          var dialogs = await vk.GetDialogsAsync();
            Dialogs = dialogs.Items;
            //DialogsName = dialogs.Profiles;
        
        }

        /// <summary>
        /// Информация об авторизовавшемся пользователе ВКонтакте
        /// </summary>
        public async Task GetUserInfo()
        {
            var UserInfo = await vk.GetUserInfo();

            string firstname = UserInfo.FirstName;
            string lastname = UserInfo.LastName;

            NameVk = firstname + " " + lastname;
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
