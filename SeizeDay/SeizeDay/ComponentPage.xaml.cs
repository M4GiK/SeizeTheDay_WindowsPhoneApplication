﻿using System;
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
    /// <summary>
    /// Class sending choosen component to MainPage
    /// </summary>
    public partial class ComponentPage : PhoneApplicationPage
    {
        /// <summary>
        /// Constructor initalizing all components on current page
        /// </summary>
        public ComponentPage()
        {
            InitializeComponent();
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_ListToDo(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ListToDo.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml?component=listToDo", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_Weather(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Weather.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml?component=weather", UriKind.Relative));
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_News(object sender, System.Windows.Input.GestureEventArgs e)
        {
            News.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml?component=news", UriKind.Relative));
        }




        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_Aphorism(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Aphorism.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml?component=aphorism", UriKind.Relative));
        }



        /// <summary>
        ///  Method called after back from another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ListToDo.Background = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            Weather.Background = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            News.Background = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            Aphorism.Background = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
        }
    }
}