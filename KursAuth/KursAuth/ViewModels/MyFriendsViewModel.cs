using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VkNet;
using KursAuth.Models;
using KursAuth.Views;
using Avalonia.Controls;
using VkNet.Model.RequestParams;
using VkNet.Enums.Filters;
using VkNet.Utils;

namespace KursAuth.ViewModels
{
    class MyFriendsViewModel : ViewModelBase, INotifyPropertyChanged
    {

       
        public static void  GetFriends(Vk vk, ListBox friends)
        {

            VkCollection<VkNet.Model.User> users = vk.GetFriends(vk);
           

           
            foreach (var item in users)
            {

                friends.Items = users;


                //friends.Items = item.FirstName + " " + item.LastName;
            }

        }


    }
}
