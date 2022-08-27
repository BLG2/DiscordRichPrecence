using DiscordRichPrecence.Models;
using DiscordRichPrecence.View;
using System;
using System.Windows;

namespace DiscordRichPrecence
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DiscordRpcVM vm = new DiscordRpcVM();
            DiscordRpcView RPCView = new DiscordRpcView();
            RPCView.DataContext = vm;
            RPCView.Show();
        }
    }
}
