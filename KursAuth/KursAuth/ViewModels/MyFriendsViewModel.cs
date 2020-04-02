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

namespace KursAuth.ViewModels
{
    class MyFriendsViewModel : ViewModelBase, INotifyPropertyChanged
    {

       
        public static void  GetFriends(VkApi api, ListBox friends)
        {
            
            var users = api.Friends.Get(new FriendsGetParams
            {
                UserId = 587033839,
                Count = 10,
                Fields = ProfileFields.FirstName,
            });
            ;

           
            foreach (var item in users)
            {

                friends.Items = users;


                //friends.Items = item.FirstName + " " + item.LastName;
            }

        }


    }
}
