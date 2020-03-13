using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

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
           

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 7062393,
                Login = login,
                Password = password,
                Settings = Settings.All
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

