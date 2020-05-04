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
using System.Threading.Tasks;
using KursAuth.Utils;

namespace KursAuth.Models
{
    public class VKModel : IMessengers
    {
        private VkApi api;
        private string login;
        private string password;

        /// <inheritdoc/> 
        public VKModel(string log, string pass)
        {
            login = log;
            password = pass;

            var services = new ServiceCollection();
            services.AddAudioBypass();
            api = new VkApi(services);

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
        public async Task<IEnumerable<object>> GetFriendsAsync()
        {
            var users = await api.Friends.GetAsync(new FriendsGetParams
            {
                UserId = api.UserId,
                Count = 1000,
                Fields = ProfileFields.FirstName,
            });
            return users;
        }

        public async Task<MessageGetHistoryObject> GetHistoryAsync(long userid)
        {

            var getHistory = await api.Messages.GetHistoryAsync(new MessagesGetHistoryParams
            {
                Count = 200,
                UserId = userid

            });
            return getHistory;
        }

        public async Task SendMessageAsync(long userid,string text)
        {

            await api.Messages.SendAsync(new MessagesSendParams
            {
                UserId = userid, //Id получателя
                Message = text, //Сообщение
                RandomId = new Random().Next(999999) //ужасный уникальный идентификатор
            });
        }

        public async Task<User> GetUserInfo()
        {
            var infoaboutuser = api.Users.Get(new long[] { api.UserId.Value }).FirstOrDefault();
            return infoaboutuser;

        }




    }

}