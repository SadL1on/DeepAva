using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace KursAuth.Models
{
    class Authorization
    {
        private string login;
        private string password;

        public Authorization(string log, string pass)
        {
            login = log;
            password = pass;
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 7062393,
                Login = login,
                Password = password,
                Settings = Settings.Groups
            });

        }
        




    }

}

