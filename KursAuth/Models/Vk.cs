using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.AudioBypassService.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace KursAuth.Models
{
   public class Vk
    {
        public VkApi api = new VkApi();
        private string login;
        private string password;
        
        
        public VkApi auth(string log, string pass)
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
            return (api);
        }

        //public void GetFriends() 
        //{

        //    var users = api.Friends.Get(new FriendsGetParams
        //    {
        //        UserId = 170426526,
        //        Count = 10,
        //        Fields = ProfileFields.FirstName,
        //    });
           

        //    foreach (var item in users)
        //    {
        //        Console.WriteLine(item.Id + item.FirstName + item.LastName);
        //    }





        //}
      



    }

}

