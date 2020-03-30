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
            MyFriends.Click += MyFriends_Click;
            this.api = api;
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