using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
//  for xml parsing
using System.Xml.Linq;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SeizeDay
{
    public partial class WeatherSettings : PhoneApplicationPage
    {
        public WeatherSettings()
        {
            InitializeComponent();
        }

        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            // send data to DB
            //tbxCity.Text + "," + tbxCountry.Text

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml?component=weather&city=" + tbxCity.Text + "," + tbxCountry.Text,
                UriKind.Relative));
        }


    } 
}