using DiscordRichPrecence.Models;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;
using System.Windows.Input;

namespace DiscordRichPrecence.View
{
    /// <summary>
    /// Interaction logic for DiscordRpcView.xaml
    /// </summary>
    public partial class DiscordRpcView : Window
    {
        public DiscordRpcView() => InitializeComponent();

        private void MoveWindowOnHold(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
                {
                    TransOnDrag.Opacity = 0.7;
                    this.DragMove();
                }
                TransOnDrag.Opacity = 1;
            }
            catch (Exception)
            {
                //ignored
            }
        }

    }
}
