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
        Button back;

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
            back = this.FindControl<Button>("Back");
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

            back.Click += Back_Click;
        }

        private void Back_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            VkMain vkmain = new VkMain(api);
            vkmain.Show();
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}