using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.Models;
using KursAuth.ViewModels;
using KursAuth.ViewModels.Messengers;
using ReactiveUI;
using System.Reactive.Disposables;

namespace KursAuth.Views.Messengers
{
    public class VkAuth : ReactiveUserControl<VkAuthVM>
    {
        private TextBox login => this.FindControl<TextBox>("Login");
        private TextBox password => this.FindControl<TextBox>("Password");
        private Button on => this.FindControl<Button>("On");

        public VkAuth()
        {
            this.InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.LoginMess, x => x.login.Text).DisposeWith(disposables);
                this.Bind(ViewModel, x => x.PassMess, x => x.password.Text).DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.MessLogin, x => x.on).DisposeWith(disposables);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
