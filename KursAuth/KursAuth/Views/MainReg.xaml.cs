using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using KursAuth.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace KursAuth.Views
{
    public class MainReg : ReactiveUserControl<MainWindowViewModel>
    {
        private Button register => this.FindControl<Button>("RegAcc");
        private Button back => this.FindControl<Button>("Back");
        private TextBox regLog => this.FindControl<TextBox>("LogReg");
        private TextBox regPass => this.FindControl<TextBox>("PassReg");

        public MainReg()
        {
            InitializeComponent();

            //this.WhenActivated(disposables =>
            //{
            //    this.Bind(ViewModel, x => x.LoginMain, x => x.regLog.Text).DisposeWith(disposables);
            //    this.Bind(ViewModel, x => x.PassMain, x => x.regPass.Text).DisposeWith(disposables);
            //    this.BindCommand(ViewModel, x => x.ToMainAuthCmd, x => x.back).DisposeWith(disposables);
            //    this.BindCommand(ViewModel, x => x.AuthorizationMainCmd, x => x.register).DisposeWith(disposables);
            //});
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
