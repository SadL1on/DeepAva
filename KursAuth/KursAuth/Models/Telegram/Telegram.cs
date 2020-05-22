using Starksoft.Net.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp;
using TLSharp.Core;

namespace KursAuth.Models.Telegram
{
    public class Telegram
    {

        TelegramClient client;
        string hash;
        string phone;
        public bool IsAuth { get => client.IsUserAuthorized(); }

        private Telegram() { }

        private static Telegram _instance;
        public static Telegram GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Telegram();
            }
            return _instance;
        }

        static System.Net.Sockets.TcpClient TcpHandler(string address, int port)
        {
            var socks = new Socks5ProxyClient("orbtl.s5.opennetwork.cc", 999, "262852885", "vmNeknLh");
            var tcp = socks.CreateConnection(address, port);

            return tcp;
        }
        public async Task SendCodeToAuth(string phone)
        {
            var apiId = 1360689;
            var apiHash = "57c360c3d2605bbb0bc9930b151dd937";


            client = new TelegramClient(apiId, apiHash, handler: TcpHandler);
            await client.ConnectAsync();
            hash = await client.SendCodeRequestAsync(phone);
            this.phone = phone;

        }

        public async Task MakeAuth(string code)
        {
           
            var user = await client.MakeAuthAsync(phone, hash, code);

        }

        public async Task SendMessage(string message, string userphone)
        {


            var result = await client.GetContactsAsync();
            //find recipient in contacts
            var user1 = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Phone == userphone);

            //send message
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user1.Id }, message);


        }
        public async Task<IEnumerable<object>> GetFriendsAsync()
        {
           
            var dialogs = (TLDialogs)await client.GetUserDialogsAsync();

            var users = dialogs.Users.ToArray();
            
            return users;
           




        }
    }
}
