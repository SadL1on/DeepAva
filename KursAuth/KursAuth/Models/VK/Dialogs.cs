using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursAuth.Models.VK
{
   public class Dialogs
    {
        /// <summary>
        /// Идентификатор пользователя 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Имя пользователя (FirstName+LastName в одном поле)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Наименование беседы
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Последнее сообщение в диалоге
        /// </summary>
        public string LastMessage { get; set; }
        /// <summary>
        /// Фотография пользователя или беседы
        /// </summary>
        public Bitmap photo { get; set; }

    }
}
