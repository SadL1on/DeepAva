using KursAuth.Utils.Messages;
using KursAuth.ViewModels.Messengers;
using KursAuth.Views.Messengers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;

namespace KursAuth.ViewModels
{
    [DataContract]
    public class TestVM : ViewModelBase, IRoutableViewModel, IScreen
    {
        public string UrlPathSegment => "/test";

        public IScreen HostScreen { get; }

        private RoutingState _router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public string token { get; set; }

        string path = @"D:\Не мэйн токен";

        public TestVM()
        {
            //try
            //{
            //    using (FileStream fstream = File.OpenRead($@"{path}\note.txt"))
            //    {
            //        byte[] array = new byte[fstream.Length];
            //        fstream.Read(array, 0, array.Length);
            //        string token = System.Text.Encoding.Default.GetString(array);
            //    }
            //    Router.Navigate.Execute(new MainWindowViewModel());
            //}
            //catch(DirectoryNotFoundException)
            //{
            //    Router.Navigate.Execute(new MainAuthVM());
            //}
            //MessageBus.Current.Listen<MainWindowViewModel>().Subscribe(Observer.Create<MainWindowViewModel>((e) => { Router.Navigate.Execute(new MainWindowViewModel()); }));

            Router.Navigate.Execute(new MainWindowViewModel());
        }
    }
}
