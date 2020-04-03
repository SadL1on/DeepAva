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

namespace KursAuth.Models
{
    public class Vk
    {
        public VkApi api = new VkApi();
        private static string login;
        private static string password;


        public Vk auth(string log, string pass)
        {
            login = log;
            password = pass;


            var services = new ServiceCollection();
            services.AddAudioBypass();

            var api = new VkApi(services);

            // Авторизируемся для получения токена валидного для вызова методов Audio / Messages
            api.Authorize(new ApiAuthParams
            {
                Login = login,
                Password = password
            });
            this.api = api;
            return (this);
        }

        public  VkCollection<VkNet.Model.User> GetFriends(Vk vk)
        {
        
            var users = vk.api.Friends.Get(new FriendsGetParams
            {
                UserId = 587033839,
                Count = 1000,
                Fields = ProfileFields.FirstName,
            });
            return (users);
        }




    }

}