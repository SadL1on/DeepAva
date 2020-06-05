using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursAuth.Models.VK
{
   public class Message
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Идентификатор автора сообщения  
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Расположение сообщения в беседе
        /// </summary>
        public string Alignment { get; set; }
        /// <summary>
        /// Фото пользователя 
        /// </summary>
        public Bitmap Photo { get; set; }
    }
}
