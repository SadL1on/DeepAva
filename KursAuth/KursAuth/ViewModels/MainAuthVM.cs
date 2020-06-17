using Avalonia.Controls;
using KursAuth.Models.Main;
using KursAuth.Utils.Messages;
using KursAuth.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KursAuth.ViewModels
{
    [DataContract]
    public class MainAuthVM : ViewModelBase, IRoutableViewModel
    {
        public string UrlPathSegment => "/Auth";

        public IScreen HostScreen { get; }

        private UserMain user = new UserMain();

        /// <summary>
        /// Команда регистрации в приложении
        /// </summary>
        public ReactiveCommand<string, Unit> AuthorizationMainCmd { get; }

        /// <summary>
        /// Логин из формы рег/авт приложения
        /// </summary>
        [Reactive]
        public string LoginMain { get; set; }

        /// <summary>
        /// Пароль из формы рег/авт приложения
        /// </summary>
        [Reactive]
        public string PassMain { get; set; }

        [Reactive]
        public bool IsVisAlertValid { get; set; }

        public string token { get; set; }

        string path = @"D:\Не мэйн токен";

        public MainAuthVM()
        {
            AuthorizationMainCmd = ReactiveCommand.Create<string>(async (flag) => { await AuthorizationMainImpl(Convert.ToBoolean(flag)); });
        }

        public async Task AuthorizationMainImpl(bool flag)
        {
            var code = await user.MainAuthAsync(LoginMain, PassMain, flag);
            if (code != 0)
            {
                IsVisAlertValid = !IsVisAlertValid;
            }
            else
            {               
                MessageBus.Current.SendMessage(new MainWindowViewModel());
                token = "mainToken";
                await Encode(token);
            }
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