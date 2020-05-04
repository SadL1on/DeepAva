using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Utils;

namespace KursAuth.Utils
{
    public interface IMessengers
    {
        public Task<IEnumerable<object>> GetFriendsAsync();

        public Task SendMessageAsync(long userid, string text);
    }
}
