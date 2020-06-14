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
        /// Логин из формы авторизации в мессенджере
        /// </summary>
        [Reactive]
        public string LoginMess { get; set; }

        /// <summary>
        /// Пароль из формы авторизации в мессенджере
        /// </summary>
        [Reactive]
        public string PassMess { get; set; }

        /// <summary>
        /// Команда авторизации ВКонтакте
        /// </summary>
        public ReactiveCommand<Unit, Task> MessLogin { get; }

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

        

        private ReactiveCommand<Unit, Unit> toVkCont { get; }

        string path = @"D:\Не токен";
        public VkAuthVM()
        {
            try
            {
               

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
           
            string token = await vk.VkAuthAsync(login, password);
            vk.IsAuth = true;
            await Encode(token);
            MessageBus.Current.SendMessage(new RouteToVkContMessage());
            
        }

        public async Task Encode(string token)
        {
           
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            // запись в файл
            using (FileStream fstream = new FileStream($@"{path}\note.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(token);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }

        }
    }
}
