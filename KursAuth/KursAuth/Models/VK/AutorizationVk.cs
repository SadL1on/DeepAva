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
        private static string login;
        private static string password;

        public AutorizationVk auth(string log, string pass)
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
                    return "";
                }
            });
            this.api = api;
            return (this);
        }

        public  VkCollection<User> GetFriends(AutorizationVk vk)
        {      
            var users = vk.api.Friends.Get(new FriendsGetParams
            {
                UserId = vk.api.UserId,
                Count = 1000,
                Fields = ProfileFields.FirstName,
            });
            return (users);
        }

        public void GetHistory(AutorizationVk vk, userid)
        {



            var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
            {
                Count = 200,
                UserId = 107114970

            });



            var messages = getHistory.Messages.ToArray();

            for (int i = 0; i < getHistory.Messages.Count(); i++)
            {
                //Console.WriteLine(messages[i].Text);
            }



        }

        public void SendMessage(AutorizationVk vk, userid)
        {

            api.Messages.Send(new MessagesSendParams
            {
                UserId = userid, //Id получателя
                Message = "Message", //Сообщение
                RandomId = new Random().Next(999999) //ужасный уникальный идентификатор
            });
        }


    }

}