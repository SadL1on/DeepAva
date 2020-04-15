using KursAuth.Interfaces;
using KursAuth.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace KursAuth.Services
{
    /// <inheritdoc/>
    class UserService : IUserService
    {
        public VkApi api = new VkApi();
        private string login;
        private string password;

        /// <inheritdoc/>
        public void Authorization(string log, string pass)
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
    }
}
