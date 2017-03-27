using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App10;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }
        private async void confirmButton_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Windows.UI.Popups.MessageDialog(

                    "Do you confirm this guest name:   " +
                    nameInput.Text);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Confirm", (UICommandInvokeHandler) =>
            {
                var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
                var tileAttributes = tileXml.GetElementsByTagName("text");

                var testGuest = App.GuestFromService.FirstOrDefault();
                if (testGuest == null)
                {


                }
                else
                {
                    tileAttributes[0].AppendChild(tileXml.CreateTextNode(App.GuestFromService.FirstOrDefault()));

                }


                var tileNotication = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotication);
                nameInput.Visibility = Visibility.Collapsed;
                confirmButton.Visibility = Visibility.Collapsed;


                Frame.Navigate(typeof(MainPage), nameInput.Text);
            })

            {
                Id = 0
            });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel", (UICommandInvokeHandler) =>
            {
                Frame.Navigate(typeof(MainPage));
            })
            {
                Id = 1
            });


            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            var btn = sender as Button;
            btn.Content = $"Result: {result.Label} ({result.Id})";


        }



        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {



            Frame.Navigate(typeof(MainPage));


        }
    }
}

