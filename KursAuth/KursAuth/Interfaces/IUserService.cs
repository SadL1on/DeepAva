using KursAuth.Models;
using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Model;
using VkNet.Utils;

namespace KursAuth.Interfaces
{

    /// <summary>
    /// Интерфейс для работы с пользователями
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// Метод авторизации пользователя
        /// </summary>
        public void Authorization(string log, string pass);

        /// <summary>
        /// Метод для получения списка контактов
        /// </summary>
        /// <returns>Возвращает коллекцию VkCollection<User></returns>
        public VkCollection<User> GetFriends();


    }
}
