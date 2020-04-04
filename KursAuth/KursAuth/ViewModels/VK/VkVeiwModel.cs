using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VkNet;
using KursAuth.Models;
using KursAuth.Views;
using Avalonia.Controls;
using VkNet.Utils;
using VkNet.Model;

namespace KursAuth.ViewModels
{
    class VkVeiwModel : ViewModelBase, INotifyPropertyChanged
    {
        public static AutorizationVk Auth(TextBox login, TextBox password)
        {
            string logintext = login.Text;
            string passwordtext = password.Text;

            AutorizationVk vk = new AutorizationVk();
            vk = vk.auth(logintext, passwordtext);

            return (vk);
        }

        public static void GetFriends(AutorizationVk vk, ListBox friends)
        {
            VkCollection<User> users = vk.GetFriends(vk);
            foreach (var item in users)
            {
                friends.Items = users;
                //friends.Items = item.FirstName + " " + item.LastName;
            }

        }
    }
}
