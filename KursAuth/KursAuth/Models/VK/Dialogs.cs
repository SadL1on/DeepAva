using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KursAuth.Models.VK
{
   public class Dialogs
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string LastMessage { get; set; }
        public Bitmap photo { get; set; }

    }
}
