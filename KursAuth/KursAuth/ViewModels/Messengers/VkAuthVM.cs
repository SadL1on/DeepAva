using KursAuth.Models;
using KursAuth.Utils.Messages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
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

        [Reactive]
        public IEnumerable Messages { get; set; }

        /// <summary>
        /// Видимость формы списка контактов
        /// </summary>
        [Reactive]
        public bool IsVisConCtrl { get; set; }

        /// <summary>
        /// Видимость формы авторизации ВК
        /// </summary>
        [Reactive]
        public bool IsVisVkAuth { get; set; }

        public ReactiveCommand<Unit, Task> MessLogin { get; }

        private ReactiveCommand<Unit, Unit> toVkCont { get; }

        public VkAuthVM()
        {
            try
            {
                string path = @"D:\SomeDir2";

                vk = VKModel.GetInstance();
                using (FileStream fstream = File.OpenRead($@"{path}\note.txt"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string token = System.Text.Encoding.Default.GetString(array);
                    vk.VkAuthTokenAsync(token);
                    vk.IsAuth = true;
                    MessageBus.Current.SendMessage(new RouteToVkContMessage());
                }
            }
            catch
            {



                vk = VKModel.GetInstance();
                if (vk.IsAuth == true)
                {
                    MessageBus.Current.SendMessage(new RouteToVkContMessage());
                }
                else
                {
                    MessLogin = ReactiveCommand.Create(async () => { await AuthMessImpl(LoginMess, PassMess); });
                }
            }
        }

        public async Task AuthMessImpl(string login, string password)
        {
                await vk.VkAuthAsync(login, password);
                vk.IsAuth = true;
            MessageBus.Current.SendMessage(new RouteToVkContMessage());
        }
    }
}
