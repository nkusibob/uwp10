using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using Windows.UI.Popups;
using System.Diagnostics;
using System.Globalization;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App4

{

    public sealed partial class MyUserControl : UserControl
    {

        private DispatcherTimer _welcomeTimer;
        private static List<string> myGuestColString = new List<string>();
        public List<string> ListName = new List<string>();
        public MainPage Page { get; set; }


        public MyUserControl()
        {
            this.InitializeComponent();

        }


        private async void WeatherDescription_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                TextBlockTitle2.Text = "Executive Briefing Center";
                //calling the web service for weather
                Proxy.RootObject2 weatherDesc = await Proxy.GetWeatherDesciption();
                Proxy.RootObject myweather = await Proxy.GetWeather();

                WeatherDescription.Text = weatherDesc.current_observation.weather;
                ResultTextBlock.Text = (Int32)myweather.current_condition.tmp + "°";
                _welcomeTimer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMinutes(13)
                };

                _welcomeTimer.Tick += _WeatherTimer_Tick;
                _welcomeTimer.Start();

                var year = DateTime.Now.ToString("yyyy");
                var day = DateTime.Now.ToString("dd");
                var month = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
                TextDate.Text = DateTime.Now.DayOfWeek.ToString() + ", " + month + " " + day + ", " + year;
                //string icon = string.Format("{0}", myweather.current_condition.icon);
                // ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

                ///calling the service for the guest

                //string name = string.Format("hey",myvalue.BriefingTitle);

            }
            catch (Exception)
            {

                WeatherDescription.Text = " no weather ";
            }

        }

        private async void _WeatherTimer_Tick(object sender, object e)
        {
            Proxy.RootObject2 weatherDesc = await Proxy.GetWeatherDesciption();
            Proxy.RootObject myweather = await Proxy.GetWeather();

            WeatherDescription.Text = weatherDesc.current_observation.weather;
            ResultTextBlock.Text = (Int32)myweather.current_condition.tmp + "°";
        }






    }
}
