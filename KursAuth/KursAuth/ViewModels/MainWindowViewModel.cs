using ReactiveUI;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI.Fody.Helpers;
using System.Runtime.Serialization;
using System.Reactive.Linq;
using KursAuth.ViewModels.Messengers;

namespace KursAuth.ViewModels
{
    [DataContract]
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged, IRoutableViewModel,IScreen
    {       
        /// <summary>
        /// Команда перехода к форме авторизации
        /// </summary>
        public ReactiveCommand<Unit, Unit> ToMainAuthCmd { get; }
       
        /// <summary>
        /// Команда открытия окна ВК
        /// </summary>
        private readonly ReactiveCommand<Unit, Unit> vkOpenCmd;

        private readonly ReactiveCommand<Unit, Unit> tlOpen;

        public ICommand VkOpenCmd => vkOpenCmd;
        public ICommand TlOpen => tlOpen;
   
        /// <summary>
        /// Видимость формы регистрации в приложении
        /// </summary>
        [Reactive]
        public bool IsVisMainReg { get; set; }

        /// <summary>
        /// Видимость формы авторизации в приложении
        /// </summary>
        [Reactive]
        public bool IsVisMainAuth { get; set; }

        private RoutingState _router = new RoutingState();

        [DataMember]
        public RoutingState Router
        {
            get => _router;
            set => this.RaiseAndSetIfChanged(ref _router, value);
        }

        public string UrlPathSegment => "/main";

        public IScreen HostScreen { get; }

        public MainWindowViewModel()
        {
            var canMoveToVkOpen = this.WhenAnyObservable(x => x.Router.CurrentViewModel).Select(current => !(current is VkAuthVM || current is VkContVM));
            vkOpenCmd = ReactiveCommand.Create(() => { Router.Navigate.Execute(new VkAuthVM()); }, canMoveToVkOpen);

            var canMoveToTlOpen = this.WhenAnyObservable(x => x.Router.CurrentViewModel).Select(current => !(current is TlAuthVM || current is TlContVM));
            tlOpen = ReactiveCommand.Create(() => { Router.Navigate.Execute(new TlAuthVM()); }, canMoveToTlOpen);

            MessageBus.Current.Listen<VkContVM>().Subscribe(Observer.Create<VkContVM>((e) => { Router.Navigate.Execute(new VkContVM()); }));
            MessageBus.Current.Listen<TlContVM>().Subscribe(Observer.Create<TlContVM>((e) => { Router.Navigate.Execute(new TlContVM()); }));
        }       
    }
}