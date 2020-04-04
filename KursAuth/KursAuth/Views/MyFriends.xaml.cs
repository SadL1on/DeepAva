using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using KursAuth.ViewModels;



namespace KursAuth.Views
{
    public class MyFriends : Window
    {
        public VkApi api;
        ListBox friends;
        Button back;
        Vk vk;

        public MyFriends()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }


        public MyFriends(Vk vk)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            back = this.FindControl<Button>("Back");
            this.vk = vk;
            friends = this.FindControl<ListBox>("Friends");

            ViewModels.MyFriendsViewModel.GetFriends(vk, friends);

            back.Click += Back_Click;
        }

        private void Back_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            VkMain vkmain = new VkMain(vk);
            vkmain.Show();
            this.Close();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}