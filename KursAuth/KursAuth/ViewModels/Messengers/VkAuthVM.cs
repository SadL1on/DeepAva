using KursAuth.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace KursAuth.ViewModels.Messengers
{
    public class VkAuthVM : ViewModelBase, IRoutableViewModel
    {
        private VKModel vk;

        public string UrlPathSegment => "/vkAuth";

        public IScreen HostScreen { get; }

        /// <summary>
        /// Логин из формы рег/авт в мессенджере
        /// </summary>
        [Reactive]
        public string LoginMess { get; set; }

        /// <summary>
        /// Пароль из формы рег/авт в мессенджере
        /// </summary>
        [Reactive]
        public string PassMess { get; set; }

        public ReactiveCommand<Unit, Task> MessLogin { get; }

        public VkAuthVM()
        {
            MessLogin = ReactiveCommand.Create(async () => { await AuthMessImpl(LoginMess, PassMess); });
        }

        public async Task AuthMessImpl(string login, string password)
        {
            vk = new VKModel();
            await vk.VkAuthAsync(login, password);
           // IsVisConCtrl = !IsVisConCtrl;
           // IsVisVkAuth = !IsVisVkAuth;
           // _messenger = vk;
           // await GetFriends();
           // await GetUserInfo();

        }
    }
}
