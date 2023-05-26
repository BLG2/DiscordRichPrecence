using DiscordRichPrecence.Services;
using DiscordRichPrecence.View;
using DiscordRPC;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DiscordRichPrecence.Properties;

namespace DiscordRichPrecence.Models
{
    public class DiscordRpcVM : ObservableObject
    {
        private readonly DispatcherTimer clock = new DispatcherTimer();
        private readonly DispatcherTimer stateInterval = new DispatcherTimer();
        private bool closedByTaskBarM = false;

        private double mainWindowHeightVal;
        public double mainWindowHeight
        {
            get { return mainWindowHeightVal; }
            set => SetProperty(ref mainWindowHeightVal, value);
        }

        private object boxToFolowHeightVal;
        public object boxToFolowHeight
        {
            get { return boxToFolowHeightVal; }
            set => SetProperty(ref boxToFolowHeightVal, value);
        }

        private TimeSpan timerDateValue;
        public TimeSpan timerDate
        {
            get { return timerDateValue; }
            set => SetProperty(ref timerDateValue, value);
        }

        private DateTime? rpcStartDateValue;
        public DateTime? rpcStartDate
        {
            get { return rpcStartDateValue; }
            set => SetProperty(ref rpcStartDateValue, value);
        }

        private string discordAppIdValue;
        public string DiscordAppId
        {
            get { return discordAppIdValue; }
            set => SetProperty(ref discordAppIdValue, value);
        }

        private string discordRpcState;
        public string DiscordRpcState
        {
            get { return discordRpcState; }
            set => SetProperty(ref discordRpcState, value);
        }

        private string discordRpcDetails;
        public string DiscordRpcDetails
        {
            get { return discordRpcDetails; }
            set => SetProperty(ref discordRpcDetails, value);
        }

        private string stateToAddValue;
        public string DetailToAdd
        {
            get { return stateToAddValue; }
            set => SetProperty(ref stateToAddValue, value);
        }

        private ObservableCollection<string> loopStateValue;
        public ObservableCollection<string> LoopDetail
        {
            get { return loopStateValue; }
            set => SetProperty(ref loopStateValue, value);
        }

        private bool button1Value;
        public bool Button1
        {
            get { return button1Value; }
            set => SetProperty(ref button1Value, value);
        }

        private string button1TextVal;
        public string button1Text
        {
            get { return button1TextVal; }
            set => SetProperty(ref button1TextVal, value);
        }

        private string button1UrlVal;
        public string button1Url
        {
            get { return button1UrlVal; }
            set => SetProperty(ref button1UrlVal, value);
        }

        private bool button2Value;
        public bool Button2
        {
            get { return button2Value; }
            set => SetProperty(ref button2Value, value);
        }

        private string button2TextVal;
        public string button2Text
        {
            get { return button2TextVal; }
            set => SetProperty(ref button2TextVal, value);
        }

        private string button2UrlVal;
        public string button2Url
        {
            get { return button2UrlVal; }
            set => SetProperty(ref button2UrlVal, value);
        }

        private string MinimizeTBTextVal;
        public string MinimizeTBText
        {
            get { return MinimizeTBTextVal; }
            set => SetProperty(ref MinimizeTBTextVal, value);
        }

        private bool loopDetailsButtonValue;
        public bool LoopDetailsButton
        {
            get { return loopDetailsButtonValue; }
            set => SetProperty(ref loopDetailsButtonValue, value);
        }

        private bool rpcTimerValue;
        public bool RpcTimer
        {
            get { return rpcTimerValue; }
            set => SetProperty(ref rpcTimerValue, value);
        }

        private bool RPCEnabledVal;
        public bool RPCEnabled
        {
            get { return RPCEnabledVal; }
            set => SetProperty(ref RPCEnabledVal, value);
        }

        private string StartButtonTextVal;
        public string StartButtonText
        {
            get { return StartButtonTextVal; }
            set => SetProperty(ref StartButtonTextVal, value);
        }

        private LinearGradientBrush StartButtonColortVal;
        public LinearGradientBrush StartButtonColor
        {
            get { return StartButtonColortVal; }
            set => SetProperty(ref StartButtonColortVal, value);
        }


        private bool largeImgButtonVal;
        public bool LargeImgButton
        {
            get { return largeImgButtonVal; }
            set => SetProperty(ref largeImgButtonVal, value);
        }


        private bool smallImgButtonVal;
        public bool SmallImgButton
        {
            get { return smallImgButtonVal; }
            set => SetProperty(ref smallImgButtonVal, value);
        }

        private string discordRpcLargeImgVal;
        public string DiscordRpcLargeImgUrl
        {
            get { return discordRpcLargeImgVal; }
            set => SetProperty(ref discordRpcLargeImgVal, value);
        }

        private string discordRpcSmallImgVal;
        public string DiscordRpcSmallImgUrl
        {
            get { return discordRpcSmallImgVal; }
            set => SetProperty(ref discordRpcSmallImgVal, value);
        }

        private string DiscordRpcLargeImgTextVal;
        public string DiscordRpcLargeImgText
        {
            get { return DiscordRpcLargeImgTextVal; }
            set => SetProperty(ref DiscordRpcLargeImgTextVal, value);
        }

        private string DiscordRpcSmallImgTextVal;
        public string DiscordRpcSmallImgText
        {
            get { return DiscordRpcSmallImgTextVal; }
            set => SetProperty(ref DiscordRpcSmallImgTextVal, value);
        }


        public DiscordRpcVM()
        {
            MinimizeTBText = "Minimize Window";

            AddDetailCommand = new RelayCommand(AddDetail);
            SetTimerCommand = new RelayCommand(timeStart);
            SetDetailsLoopCommand = new RelayCommand(detailsIntervalStart);
            StartRPCCommand = new RelayCommand(StartRPC);
            UpdateRPCCommand = new RelayCommand(UpdateRPC);
            ResetRPCTimeCommand = new RelayCommand(ResetRPCTime);
            RemoveDetailCommand = new RelayCommand<object>((x) => RemoveDetail(x));
            MinimizeAppCommand = new RelayCommand<DiscordRpcView>((x) => MinimizeApplication(x));
            CloseAppCommand = new RelayCommand<DiscordRpcView>((x) => CloseApplication(x));
            AfsluitenEvent = new RelayCommand<CancelEventArgs>((x) => Afsluiten(x));
            GridSizeChangedEvent = new RelayCommand<SizeChangedEventArgs>((x) => GridSizeChanged(x));

            clock.Tick += Clock_Tick;
            clock.Interval = new TimeSpan(0, 0, 1);

            stateInterval.Tick += StateInterval_Tick;
            stateInterval.Interval = new TimeSpan(0, 0, 10);

            DetailToAdd = "Detail text to add";
            StartButtonText = "Enable RPC";
            StartButtonColor = (LinearGradientBrush)Application.Current.Resources["ButtonGradient"];
            ApplySettings();

        }

        public ICommand AddDetailCommand { get; }
        public ICommand RemoveDetailCommand { get; }
        public ICommand SetTimerCommand { get; }
        public ICommand SetDetailsLoopCommand { get; }
        public ICommand StartRPCCommand { get; }
        public ICommand UpdateRPCCommand { get; }
        public ICommand ResetRPCTimeCommand { get; }
        public ICommand AfsluitenEvent { get; }
        public ICommand MinimizeAppCommand { get; }
        public ICommand CloseAppCommand { get; }
        public ICommand GridSizeChangedEvent { get; }


        private void GridSizeChanged(SizeChangedEventArgs e)
        {
            mainWindowHeight = e.NewSize.Height + 100;
        }

        private async void CloseApplication(DiscordRpcView win)
        {
            closedByTaskBarM = true;
            var closeQuestion = await Task.Run(() => MessageBox.Show("Do you want to close Application?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No));
            if (closeQuestion == MessageBoxResult.Yes)
            {
                var saveQuestion = await Task.Run(() => MessageBox.Show("Do you want to save the current application settings?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes));
                if (saveQuestion == MessageBoxResult.Yes)
                {
                    SaveAppSettings();
                }
                win.Close();
            }
        }

        private void MinimizeApplication(DiscordRpcView win)
        {
            if (win.Visibility != Visibility.Visible)
            {
                win.Show();
                MinimizeTBText = "Minimize Window";
            }
            else
            {
                win.Hide();
                MinimizeTBText = "Open Window";
                win.nicon.ShowBalloonTip("RPC APP", "Minimized to taskbar", BalloonIcon.Info);
            }
        }

        public void Afsluiten(CancelEventArgs e)
        {
            if (!closedByTaskBarM)
                if (MessageBox.Show("Do you want to close Application?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                    e.Cancel = true;
                else
                    if (MessageBox.Show("Do you want to save the current application settings?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                    SaveAppSettings();
        }

        private void ApplySettings()
        {
            DiscordAppId = Settings.Default.applicationId;
            DiscordRpcState = Settings.Default.rpcState;
            DiscordRpcDetails = Settings.Default.rpcDetails;
            DiscordRpcLargeImgUrl = Settings.Default.largeImageUrl;
            DiscordRpcSmallImgUrl = Settings.Default.smallImageUrl;
            DiscordRpcLargeImgText = Settings.Default.largeImageText;
            DiscordRpcSmallImgText = Settings.Default.smallImageText;
            button1Text = Settings.Default.button1Title;
            button1Url = Settings.Default.button1Url;
            button2Text = Settings.Default.button2Title;
            button2Url = Settings.Default.button2Url;
            LargeImgButton = Settings.Default.largeImageButton;
            SmallImgButton = Settings.Default.smallImageButton;
            Button1 = Settings.Default.rpcButtonButton1;
            Button2 = Settings.Default.rpcButtonButton2;
            RpcTimer = Settings.Default.rpcTimerButton;
            if (RpcTimer) timeStart();
            LoopDetailsButton = Settings.Default.loopDetailsButton;
            if (Settings.Default.loopStateStates != null && Settings.Default.loopStateStates.Count > 0)
            {
                if (LoopDetail == null) LoopDetail = new ObservableCollection<string>();
                foreach (string str in Settings.Default.loopStateStates)
                    LoopDetail.Add(str);

                if (LoopDetailsButton) detailsIntervalStart();
            }
        }

        private void SaveAppSettings()
        {
            Settings.Default.applicationId = DiscordAppId;
            Settings.Default.rpcState = DiscordRpcState;
            Settings.Default.rpcDetails = DiscordRpcDetails;
            Settings.Default.smallImageUrl = DiscordRpcSmallImgUrl;
            Settings.Default.largeImageUrl = DiscordRpcLargeImgUrl;
            Settings.Default.button1Title = button1Text;
            Settings.Default.button1Url = button1Url;
            Settings.Default.button2Title = button2Text;
            Settings.Default.button2Url = button2Url;
            Settings.Default.largeImageButton = LargeImgButton;
            Settings.Default.smallImageButton = SmallImgButton;
            Settings.Default.rpcButtonButton1 = Button1;
            Settings.Default.rpcButtonButton2 = Button2;
            Settings.Default.rpcTimerButton = RpcTimer;
            Settings.Default.loopDetailsButton = LoopDetailsButton;
            Settings.Default.largeImageText = DiscordRpcLargeImgText;
            Settings.Default.smallImageText = DiscordRpcSmallImgText;
            Settings.Default.loopStateStates = new System.Collections.Specialized.StringCollection();
            if (LoopDetail != null && LoopDetail.Count > 0)
                foreach (string str in LoopDetail)
                    Settings.Default.loopStateStates.Add(str);


            Settings.Default.Save();
        }

        private void ResetRPCTime()
        {
            rpcStartDate = DateTime.Now;
            RichPresenceAPI.Update(true);
        }

        private void UpdateRPC() => SetRpcStatus(true, true);

        private void StartRPC()
        {
            try
            {
                if (!RPCEnabled)
                {
                    RPCEnabled = true;
                    StartButtonText = "Disable RPC";
                    StartButtonColor = (LinearGradientBrush)Application.Current.Resources["ButtonGradient2"];
                    SetRpcStatus(true);
                }
                else
                {
                    RPCEnabled = false;
                    StartButtonText = "Enable RPC";
                    StartButtonColor = (LinearGradientBrush)Application.Current.Resources["ButtonGradient"];
                    SetRpcStatus(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Somthing went whrong: {ex.Message}", "Error");
            }

        }

        private void SetRpcStatus(bool enable, bool update = false)
        {
            try
            {
                if (enable)
                {
                    if (!string.IsNullOrEmpty(DiscordAppId))
                        if (DiscordAppId != "Discord Application ID")
                        {
                            if (RichPresenceAPI.Client.IsInitialized)
                            {
                                RichPresenceAPI.Client.Dispose();
                            }
                            RichPresenceAPI.Client = new DiscordRpcClient(DiscordAppId);
                        }
                        else
                        {
                            RichPresenceAPI.Client = new DiscordRpcClient("1012301758229401682");
                        }

                    List<DiscordRPC.Button> btns = new List<DiscordRPC.Button>();
                    if (Button1)
                        btns.Add(new DiscordRPC.Button()
                        {
                            Label = button1Text,
                            Url = button1Url
                        });
                    if (Button2)
                        btns.Add(new DiscordRPC.Button()
                        {
                            Label = button2Text,
                            Url = button2Url
                        });
                    RichPresenceAPI.Presence.Buttons = btns.ToArray();

                    if (!string.IsNullOrEmpty(DiscordRpcDetails))
                        RichPresenceAPI.Presence.Details = DiscordRpcState;

                    if (!string.IsNullOrEmpty(DiscordRpcState))
                        RichPresenceAPI.Presence.State = DiscordRpcDetails;

                    if (LargeImgButton)
                    {
                        if (!string.IsNullOrEmpty(DiscordRpcLargeImgUrl))
                            RichPresenceAPI.Presence.Assets.LargeImageKey = DiscordRpcLargeImgUrl;
                        if (!string.IsNullOrEmpty(DiscordAppId))
                            RichPresenceAPI.Presence.Assets.LargeImageText = DiscordRpcLargeImgText;
                    }
                    else
                    {
                        RichPresenceAPI.Presence.Assets.LargeImageKey = null;
                        RichPresenceAPI.Presence.Assets.LargeImageText = null;
                    }

                    if (SmallImgButton)
                    {
                        if (!string.IsNullOrEmpty(DiscordRpcSmallImgUrl))
                            RichPresenceAPI.Presence.Assets.SmallImageKey = DiscordRpcSmallImgUrl;
                        if (!string.IsNullOrEmpty(DiscordAppId))
                            RichPresenceAPI.Presence.Assets.SmallImageText = DiscordRpcSmallImgText;
                    }
                    else
                    {
                        RichPresenceAPI.Presence.Assets.SmallImageKey = null;
                        RichPresenceAPI.Presence.Assets.SmallImageText = null;
                    }

                    if (RpcTimer)
                    {
                        if (RichPresenceAPI.Presence.Timestamps == null)
                            RichPresenceAPI.Presence.Timestamps = Timestamps.Now;
                    }
                    else
                        RichPresenceAPI.Presence.Timestamps = null;

                    RichPresenceAPI.Update();
                }
                else
                {
                    RichPresenceAPI.Disable();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops: {ex.Message}", "Error");
            }
        }

        private void AddDetail()
        {
            if (!string.IsNullOrEmpty(DetailToAdd) && DetailToAdd != "Detail text to add")
            {
                if (LoopDetail != null)
                {
                    if (LoopDetail.Count <= 10)
                        if (!LoopDetail.Contains(DetailToAdd))
                        {
                            LoopDetail.Add(DetailToAdd);
                            DetailToAdd = "";
                        }
                        else
                            MessageBox.Show("You can not add a dupplicated detail");
                    else
                        MessageBox.Show("Maximmum details reached.");
                }
                else
                {
                    LoopDetail = new ObservableCollection<string> { DetailToAdd };
                }
            }
        }

        private void RemoveDetail(object listBoxObj)
        {
            if (listBoxObj != null)
            {
                ListBox lb = (ListBox)listBoxObj;
                if (lb != null && lb.Items.Count > 0 && LoopDetail.Count > 0)
                {
                    LoopDetail.Remove(lb.SelectedItem.ToString());
                }
            }
        }

        private void timeStart()
        {
            if (RpcTimer && !clock.IsEnabled)
            {
                rpcStartDate = DateTime.Now;
                clock.Start();
            }
            else if (!RpcTimer)
            {
                rpcStartDate = null;
                clock.Stop();
            }
        }

        private void Clock_Tick(object? sender, EventArgs? e)
        {
            if (rpcStartDate != null && DateTime.Now > rpcStartDate)
                timerDate = DateTime.Now - (DateTime)rpcStartDate;
            else
                timerDate = new TimeSpan();
        }

        private void detailsIntervalStart()
        {
            if (LoopDetailsButton && !stateInterval.IsEnabled)
            {
                stateInterval.Start();
            }
            else if (!LoopDetailsButton)
            {
                stateInterval.Stop();
            }
        }

        private void StateInterval_Tick(object? sender, EventArgs? e)
        {
            if (LoopDetail != null && LoopDetail.Count > 0)
            {
                Dictionary<int, string> rpcStateList = new Dictionary<int, string>();
                foreach (string state in LoopDetail) rpcStateList.Add(rpcStateList.Count, state);

                int chooseThis = 0;
                foreach (string state in rpcStateList.Values)
                {
                    var key = rpcStateList.FirstOrDefault(x => x.Value == state).Key;
                    if (state == DiscordRpcDetails) chooseThis = key + 1;
                }
                DiscordRpcDetails = rpcStateList.FirstOrDefault(s => s.Key == (chooseThis < rpcStateList.Count ? chooseThis : 0)).Value;
                if (RPCEnabled) SetRpcStatus(true, true);
            }
        }

    }

}
