using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DynamicData;
using KursAuth.Models;
using KursAuth.Models.VK;
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
        Dialogs UserInDialog { get; set; }

        [Reactive]
        public IEnumerable Messages { get; set; }

        /// <summary>
        /// Имя авторизовавшегося пользователя ВК
        /// </summary>
        [Reactive]
        public string NameVk { get; set; }

        [Reactive]
        public string MessageText { get; set; }

        [Reactive]
        public Models.VK.Message SelItem { get; set; }

        [Reactive]
        public bool IsVisRecip { get; set; }

        [Reactive]
        public string RecipTitle { get; set; }

        [Reactive]
        public bool IsVisSendMess { get; set; }

        public ReactiveCommand<Dialogs, Unit> GetMessHist { get; }
        public ReactiveCommand<Unit, Task> SendMessage { get; }

        public VkContVM()
        {
            vk = VKModel.GetInstance();
            GetFriends();
            GetMessHist = ReactiveCommand.Create<Dialogs>(async (selectedItem) => { await GetHisVMAsync(selectedItem); });
            SendMessage = ReactiveCommand.Create(async () => { await SendMessageAsync(); });

        }
        
        public async Task GetHisVMAsync(Dialogs selectedItem)
        {
            UserInDialog = selectedItem;
            if (selectedItem == null)
                return;
            IsVisRecip = true;
            RecipTitle = selectedItem.Name;
            var MessagesHistory = vk.GetMessagesByUserId(selectedItem.Id);
            var meshist = MessagesHistory.Messages.OrderBy(x=>x.Date).ToArray();
            Models.VK.Message[] ms = new Models.VK.Message[meshist.Length];
            for (int i = 0; i < meshist.Length; i++)
            {
                Models.VK.Message mes = new Models.VK.Message();
                mes.Text = meshist[i].Text;
                for (int j = 0; j<MessagesHistory.Users.Count(); j++)
                {
                    if (MessagesHistory.Users.ToArray()[j].Id == meshist[i].FromId)
                        mes.Name = MessagesHistory.Users.ToArray()[j].FirstName;
                    if (MessagesHistory.Users.ToArray()[j].Id == vk.Api.UserId)
                        mes.Name = MessagesHistory.Users.ToArray()[j].FirstName;
                }

                if(meshist[i].FromId == selectedItem.Id)
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
            IsVisSendMess = true;
            SelItem = ms[ms.Length - 1];
        }

        /// <summary>
        /// Список друзей ВКонтакте
        /// </summary>
        public async Task GetFriends()
        {
          var dialogs = await vk.GetDialogsAsync();

            Dialogs[] MyDialogs = new Dialogs[dialogs.Items.Count];
            for (int i = 0; i < dialogs.Items.Count; i++)
            {
                Models.VK.Dialogs MyDialog = new Models.VK.Dialogs();
                MyDialog.Id = dialogs.Items[i].Conversation.Peer.Id;
                MyDialog.LastMessage = dialogs.Items[i].LastMessage.Text;
                
                if (dialogs.Items[i].Conversation.ChatSettings != null)
                {
                    MyDialog.Title = dialogs.Items[i].Conversation.ChatSettings.Title;
                }

                for (int j = 0; j < dialogs.Profiles.Count; j++)
                {
                    if (MyDialog.Id == dialogs.Profiles[j].Id)
                    {
                        string FirstName = dialogs.Profiles[j].FirstName;
                        string LastName = dialogs.Profiles[j].LastName;
                        MyDialog.Name = FirstName + " " + LastName;

                    //    MyDialog.photo = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>()
                    //.Open(new Uri($" avares://ASSEMBLYNAME/relative/project/path/{dialogs.Profiles[i].Photo100}.ico")));
                    }

                    if (MyDialog.Name != null) { break; }                   
                }
                MyDialogs[i] = MyDialog;
            }
            Dialogs = MyDialogs;
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
        public async Task SendMessageAsync()
        {
            var dialog = UserInDialog;
            try
            {
                await vk.SendMessageAsync(UserInDialog.Id, MessageText);
                MessageText = null;
                await GetFriends();
                await GetHisVMAsync(UserInDialog);
                UserInDialog = dialog;
            }
            catch
            { }
        }
    }
}
