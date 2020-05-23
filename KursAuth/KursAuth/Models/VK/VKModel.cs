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
using System.IO;

namespace KursAuth.Models
{
    public class VKModel : IMessengers
    {
        private VKModel() { }

        private static VKModel _instance;
        public static VKModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VKModel();
            }
            return _instance;
        }

        private VkApi api;
        private string login;
        private string password;
        public VkApi Api => api;

        public IEnumerable<Message> GetMessagesByUserId(long peerid)
        {
            var mess = Api.Messages.GetHistory(new MessagesGetHistoryParams() { PeerId = peerid });
           
            return mess.Messages;
        }

        public bool IsAuth = false;


        public async Task VkAuthTokenAsync(string token)
        {

            var services = new ServiceCollection();
            services.AddAudioBypass();
            api = new VkApi(services);
            api.Authorize(new ApiAuthParams
            {
                AccessToken = token
            });


        }

        /// <summary>
        /// Метод авторизации Вконтакте
        /// </summary>
        /// <inheritdoc/> 
        public async Task<string> VkAuthAsync(string log, string pass)
        {
            login = log;
            password = pass;
         

            var services = new ServiceCollection();
            services.AddAudioBypass();
            api = new VkApi(services);
                await api.AuthorizeAsync(new ApiAuthParams
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
            return api.Token;
         
        }
        /// <summary>
        /// Метод возвращает список друзей авторизовавшегося пользователя
        /// </summary>
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


        public async Task<GetConversationsResult> GetDialogsAsync()
        {
            var dialogs = await api.Messages.GetConversationsAsync(new GetConversationsParams
            {
                Count = 200,
                Extended = true

            });; ;

            return dialogs;
        }

        /// <summary>
        /// Метод возвращает историю диалога 
        /// </summary>
        public async Task<MessageGetHistoryObject> GetHistoryAsync(long userid)
        {

            var getHistory = await api.Messages.GetHistoryAsync(new MessagesGetHistoryParams
            {
                Count = 200,
                UserId = userid

            });
            return getHistory;
        }
        /// <summary>
        /// Метод отправляет сообщение пользователю
        /// </summary>
        public async Task SendMessageAsync(long userid,string text)
        {

            await api.Messages.SendAsync(new MessagesSendParams
            {
                UserId = userid, //Id получателя
                Message = text, //Сообщение
                RandomId = new Random().Next(999999) //ужасный уникальный идентификатор
            });
        }
        /// <summary>
        /// Метод возвращает информацию об авторизовавшемся пользователе
        /// </summary>
        public async Task<User> GetUserInfo()
        {
            var infoaboutuser = api.Users.Get(new long[] { api.UserId.Value }).FirstOrDefault();
            return infoaboutuser;

        }

        public string GetUserInfo(long id)
        {
            var infoaboutuser = api.Users.Get(new long[] { id }).FirstOrDefault();
            string fn = infoaboutuser.FirstName;
            string ln = infoaboutuser.LastName;
            string name = fn + " " + ln;
            return name;

        }




    }

}