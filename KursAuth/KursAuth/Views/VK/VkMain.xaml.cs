using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;
using VkNet;


namespace KursAuth.Views
{

    public class VkMain : ReactiveWindow<MainWindowViewModel>
    {
        Button MyFriends;
        Button MyMessages;
        AutorizationVk vk;
        public VkMain()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public VkMain(AutorizationVk vk)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            MyFriends = this.FindControl<Button>("MyFriends");
            MyMessages = this.FindControl<Button>("MyMessages");
            this.vk = vk;
           
            MyFriends.Click += MyFriends_Click;
            //MyMessages.Click += MyMessages_Click;
        }

        //private void MyMessages_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        //{
        //    MyMessages mymessages = new MyMessages(vk);
        //    mymessages.Show();
        //    this.Close();
        //}

        private void MyFriends_Click(object sender, RoutedEventArgs e)
        {
            MyFriends myfriends = new MyFriends(vk);
            myfriends.Show();
            Close();
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}