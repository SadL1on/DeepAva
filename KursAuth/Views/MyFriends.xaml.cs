using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace KursAuth.Views
{
    public class MyFriends : Window
    {
        public VkApi api;
        ListBox friends;
       
      
        public MyFriends()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }


        public MyFriends(VkApi api)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.api = api;
            var users = api.Friends.Get(new FriendsGetParams
            {
                UserId = 587033839,
                Count = 10,
                Fields = ProfileFields.FirstName,
            });
            ;

            friends = this.FindControl<ListBox>("Friends");
            foreach (var item in users)
            {

                friends.Items = users;


                //friends.Items = item.FirstName + " " + item.LastName;
            }
           
             
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
