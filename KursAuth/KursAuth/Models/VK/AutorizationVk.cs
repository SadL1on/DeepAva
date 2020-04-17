using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.AudioBypassService.Extensions;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Utils;
using System.Linq;

namespace KursAuth.Models
{
    public class AutorizationVk
    {
        public VkApi api = new VkApi();
        private string login;
        private string password;

        /// <inheritdoc/> 
        public AutorizationVk(string log, string pass)
        {
            login = log;
            password = pass;

            var services = new ServiceCollection();
            services.AddAudioBypass();
            var api = new VkApi(services);

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 7062393,
                Login = login,
                Password = password,
                Settings = Settings.All,
                TwoFactorAuthorization = () =>
                {
                    throw new Exception("двухфакторка");
                }
            });
        }

        /// <inheritdoc/> 
        public VkCollection<User> GetFriends()
        {
            var users = api.Friends.Get(new FriendsGetParams
            {
                UserId = api.UserId,
                Count = 1000,
                Fields = ProfileFields.FirstName,
            });
            return users;
        }

        public MessageGetHistoryObject GetHistory(long userid)
        {

            var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
            {
                Count = 200,
                UserId = userid

            });
            return getHistory;
        }

        public void SendMessage(AutorizationVk vk, long userid,string text)
        {

            vk.api.Messages.Send(new MessagesSendParams
            {
                UserId = userid, //Id получателя
                Message = text, //Сообщение
                RandomId = new Random().Next(999999) //ужасный уникальный идентификатор
            });
        }


    }

}