using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SeizeDay
{
    public partial class SoundCollectionPage : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor initalizing all components on current page
        /// </summary>
        public SoundCollectionPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This method send information to alarm page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_baroqueloop(object sender, System.Windows.Input.GestureEventArgs e)
        {
            baroqueloop.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml?sound=baroqueloop.wma", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to alarm page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_beyonthepain(object sender, System.Windows.Input.GestureEventArgs e)
        {
            beyonthepain.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml?sound=beyonthepain.wma", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to alarm page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_inbetween(object sender, System.Windows.Input.GestureEventArgs e)
        {
            inbetween.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml?sound=inbetween.wma", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to alarm page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_sensitivewalk(object sender, System.Windows.Input.GestureEventArgs e)
        {
            sensitivewalk.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml?sound=sensitivewalk.wma", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to alarm page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_spanishmusic(object sender, System.Windows.Input.GestureEventArgs e)
        {
            spanishmusic.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml?sound=spanishmusic.wma", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to alarm page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_ukrainwarship(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ukrainwarship.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml?sound=ukrainwarship.wma", UriKind.Relative));
        }



        /// <summary>
        ///  Method called after back from another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            baroqueloop.Background      = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            beyonthepain.Background     = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            inbetween.Background        = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            sensitivewalk.Background    = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            spanishmusic.Background     = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            ukrainwarship.Background    = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
        }
    }
}