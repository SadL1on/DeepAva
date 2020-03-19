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

        public Vk(string log, string pass)
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

        }

        public void Message(string message)
        {
           
            api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                RandomId = 123, // уникальный
                UserId = 107114970,
                Message = message
            });


        }



    }

}

