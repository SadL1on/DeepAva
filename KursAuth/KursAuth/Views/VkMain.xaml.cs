using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KursAuth.Models;
using VkNet;


namespace KursAuth.Views
{

    public class VkMain : Window
    {
        Button MyFriends;
        Button MyMessages;
        VkApi api;
        public VkMain()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif


        }
        public VkMain(VkApi api)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            MyFriends = this.FindControl<Button>("MyFriends");
            MyMessages = this.FindControl<Button>("MyMessages");
            this.api = api;
           
            MyFriends.Click += MyFriends_Click;
            MyMessages.Click += MyMessages_Click;
        }

        private void MyMessages_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MyMessages mymessages = new MyMessages(api);
            mymessages.Show();
            this.Close();
        }

        private void MyFriends_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MyFriends myfriends = new MyFriends(api);
            myfriends.Show();
            this.Close();
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}