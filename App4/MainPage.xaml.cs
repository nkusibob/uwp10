
using App10;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private double dLeftColWidth = 0;
        private double dRightColWidth = 0;
        private bool bLoaded = false;

        private IList<Uri> _videos;// = new List<Uri>();
        private Queue<Uri> _playlist;
        private DispatcherTimer _welcomeTimer;
        private static List<string> myGuestColString = new List<string>();
        public List<string> ListName = new List<string>();
        public MainPage Page { get; set; }


        public delegate double delegateVideoSize();

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            //CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            //if (App.checkGuestNameEnded != 1) WelcomeRelativePanel.Visibility = Visibility.Collapsed;
            dLeftColWidth = LeftCol.ActualWidth;
            dRightColWidth = RightCol.ActualWidth;
        }
        public double calcul()
        {

            return textBlockMessage.ActualWidth;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.AddedGuest.Add((string)e.Parameter);

        }
        private async void toggleFullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
            }
            else
            {
                var succeeded = view.TryEnterFullScreenMode();
                if (!succeeded)
                {
                    var dialog = new MessageDialog("Unable to enter the full-screen mode.");
                    await dialog.ShowAsync();
                }
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            myMainFrame.Navigate(typeof(BlankPage1));

        }










        private async void textBlockMessage_Loaded(object sender, RoutedEventArgs e)
        {


            textBlockMessage.Text = await Proxy.GetAddedMessage();

        }
        private async Task<List<string>> GuestGreetings()
        {
            DateTime enterDate = new DateTime();
            enterDate = DateTime.Now;
            DateTime exitDate = new DateTime();
            exitDate = enterDate.AddDays(1);
            try
            {
                IList<ServiceNameGuest.MyObject> myvalue = await ServiceNameGuest.GetBriefingsByDateAndCenter
                                                        (enterDate, exitDate, "Brussels");
                foreach (var item in myvalue)
                {
                    var datesession = String.Format("{0:MM/dd/yy}", DateTime.Parse(item.SessionDate));
                    var today = String.Format("{0:MM/dd/yy}", enterDate);
                    if (datesession == today)

                    {
                        string lines = item.BriefingTitle.ToString();

                        ListName.Add(lines);
                    }


                    // Write the string to a file.

                }

                foreach (var item in App.AddedGuest)
                {
                    if (item != "")
                        ListName.Add(item);
                }
                ListName = ListName.Distinct().ToList();
                ListName.RemoveAll(item => item == null);
            }
            catch (Exception)
            {
                throw;
            }



            return ListName;

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void StartWelcomeGuest()
        {

            if (ListName.Count == 0)
            {
                VideoWithNoGuest();

            }
            else
            {
                fv.Visibility = Visibility.Collapsed;
                mediaPlayer.Visibility = Visibility.Collapsed;

                WelcomeRelativePanelUserControl.Visibility = Visibility.Visible;
                fvWelcome.Visibility = Visibility.Visible;

                if (App.AddedGuest.Count > 0)
                {
                    foreach (var item2 in App.AddedGuest)
                    {
                        if (item2 == "")
                            ListName.Remove(item2);

                    };
                }
                App.GuestFromService = App.GuestFromService.Distinct().ToList();
                fvWelcome.ItemsSource = ListName.Distinct().ToList(); ;
                FlpVOpacity.Seek(TimeSpan.Zero);
                fvWelcome.SelectedIndex = 0;




                _welcomeTimer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(9)
                };

                _welcomeTimer.Tick += __welcomeTimer_Tick;
                _welcomeTimer.Start();
            }

        }

        private void __welcomeTimer_Tick(object sender, object e)
        {
            try
            {
                if (fvWelcome.SelectedIndex < fvWelcome.Items.Count - 1)
                {
                    fvWelcome.SelectedIndex++;
                    FlpVOpacity.Seek(TimeSpan.Zero);

                }

                else
                {
                    //_welcomeFade.Stop();
                    _welcomeTimer.Stop();
                    StartVideos();
                }
            }
            catch (Exception ex)
            {
                WelcomeTextException2.Text = "I am at __welcomeTimer_Tick";
                WelcomeTextException2.Text = ex.StackTrace.Substring(0) + "\n   ";
                WelcomeTextException2.Text = "HResult :" + ex.HResult.ToString() + "\n   ";
                WelcomeTextException2.Text += "InnerException :" + ex.InnerException + " \n  ";
                WelcomeTextException2.Text += "Source :" + ex.Source + " \n  ";
                WelcomeTextException2.Text += " linenum :" + ex.ToString() + " \n  ";
                WelcomeTextException2.Text += "Message :" + ex.Message;
            }
        }

        private void StartVideos()
        {
            int iLine = 0;
            try
            {
                _playlist = new Queue<Uri>();
                foreach (Uri uri in _videos)
                    _playlist.Enqueue(uri);
                iLine = 225;

                if (_videos == null)
                {
                    //WelcomeTextException2.Text += "_video is null";
                    iLine = 1000;
                    StartPictures();
                }
                else if (_playlist.Count == 0)
                {
                    StartPictures();
                }
                else
                {
                    iLine = 235;

                    iLine = 236;


                    fv.Visibility = Visibility.Collapsed;
                    iLine = 239;
                    fvWelcome.Visibility = Visibility.Collapsed;
                    iLine = 241;

                    App.checkGuestNameEnded = 1;
                    WelcomeRelativePanelUserControl.Visibility = Visibility.Collapsed;
                    iLine = 243;
                    mediaPlayer.Visibility = Visibility.Visible;
                    iLine = 245;

                    mediaPlayer.Source = _playlist.Dequeue();
                    iLine = 247;
                    mediaPlayer.Play();
                    iLine = 249;
                }


            }
            catch (Exception ex)
            {
                //throw;
                WelcomeTextException2.Text = "I am at Start videos: Line" + iLine.ToString() + "\n";
                WelcomeTextException2.Text += App.ProxyErrors.ToString();
                WelcomeTextException2.Text += " Videos Count: " + _videos.Count.ToString() + "\n";
                WelcomeTextException2.Text += "HResult :" + ex.HResult.ToString() + "\n   ";
                WelcomeTextException2.Text += "InnerException :" + ex.InnerException + " \n  ";
                WelcomeTextException2.Text += "Source :" + ex.Source + " \n  ";
                WelcomeTextException2.Text += " linenum :" + ex.ToString() + " \n  ";
                WelcomeTextException2.Text += "Message :" + ex.Message;
            }
        }


        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {

            if (_playlist.Count > 0)
            {
                var mywidth = mediaPlayer.ActualWidth;
                mediaPlayer.Source = _playlist.Dequeue();
            }




            else
                StartPictures();
        }

        private void StartPictures()
        {
            fv.Visibility = Visibility.Visible;
            mediaPlayer.Visibility = Visibility.Collapsed;
            if (fv.SelectedIndex < 0)
            {
                // _welcomeTimer.Stop();
                StartWelcomeGuest();
            }
            else
            {
                fv.SelectedIndex = 0;

                _welcomeTimer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(3)
                };

                _welcomeTimer.Tick += _imagesTimer_Tick;
                _welcomeTimer.Start();
            }
        }


        private void _imagesTimer_Tick(object sender, object e)
        {
            if (fv.SelectedIndex < fv.Items.Count - 1)
                fv.SelectedIndex++;
            else
            {
                _welcomeTimer.Stop();
                StartWelcomeGuest();
            }
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {

            var coef = mediaPlayer.NaturalVideoWidth / (float)mediaPlayer.NaturalVideoHeight;
            var coefMax = 16 / 9.0f;

            if (coef > coefMax)
            {

                //var windowsSize = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                //LeftCol.MaxWidth = 0;
                //myMainGrid.MaxWidth = windowsSize;
                //RightCol.MaxWidth = windowsSize;
                textBlockMessage.Visibility = Visibility.Collapsed;
                Grid.SetColumn(Body, 0);
                Grid.SetColumnSpan(Body, 2);
                // coef = coefMax;
            }
            else
            {
                textBlockMessage.Visibility = Visibility.Visible;
                Grid.SetColumn(Body, 1);
                Grid.SetColumnSpan(Body, 1);
                //LeftCol.MaxWidth = 640;
                //RightCol.MaxWidth = 1280;               
            }

            mediaPlayer.Width = mediaPlayer.Height * coef;

        }
        private async void VideoWithNoGuest()
        {
            _videos = await Proxy.GetMedia();
            var i = _videos.Count();
            StartVideos();
        }
        private async void UserControl_Loading(FrameworkElement sender, object args)
        {
            try
            {
                await GuestGreetings();
                StartWelcomeGuest();
                App.AddedMessage = await Proxy.GetAddedMessage();
                _videos = await Proxy.GetMedia();
                fv.ItemsSource = await Proxy.GetPictures();

            }
            catch (Exception ex)
            {

                WelcomeTextException2.Text = "I am at UserControl_Loading";
                WelcomeTextException2.Text += "HResult :" + ex.HResult.ToString() + "\n   ";
                WelcomeTextException2.Text += "InnerException :" + ex.InnerException + " \n  ";
                WelcomeTextException2.Text += "Source :" + ex.Source + " \n  ";
                WelcomeTextException2.Text += " linenum :" + ex.ToString() + " \n  ";
                WelcomeTextException2.Text += "Message :" + ex.Message;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await GuestGreetings();
                StartWelcomeGuest();
                App.AddedMessage = await Proxy.GetAddedMessage();
                _videos = await Proxy.GetMedia();
                fv.ItemsSource = await Proxy.GetPictures();
            }
            catch (Exception ex)
            {

                WelcomeTextException2.Text = "I am at UserControl_Loading";
                WelcomeTextException2.Text += "HResult :" + ex.HResult.ToString() + "\n   ";
                WelcomeTextException2.Text += "InnerException :" + ex.InnerException + " \n  ";
                WelcomeTextException2.Text += "Source :" + ex.Source + " \n  ";
                WelcomeTextException2.Text += " linenum :" + ex.ToString() + " \n  ";
                WelcomeTextException2.Text += "Message :" + ex.Message;
            }
        }
    }
}
