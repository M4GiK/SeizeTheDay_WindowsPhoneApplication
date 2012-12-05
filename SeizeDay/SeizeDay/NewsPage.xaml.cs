using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// This class get inforamtion about chosen news.
    /// </summary>
    public partial class NewsPage : PhoneApplicationPage, INotifyPropertyChanged
    {

        /// <summary>
        /// Data context for the local database
        /// </summary>
        private ViewModels.Component ComponentDB;

        /// <summary>
        /// Observable collection for property.
        /// </summary>
        private ObservableCollection<ViewModels.ComponentItem> _componentItems;

        /// <summary>
        /// Define an observable collection property that controls can bind to.
        /// </summary>
        public ObservableCollection<ViewModels.ComponentItem> ComponentItems
        {
            get
            {
                return _componentItems;
            }
            set
            {
                if (_componentItems != value)
                {
                    _componentItems = value;
                    NotifyPropertyChanged("ComponentItems");
                }
            }
        }



        /// <summary>
        /// Constructor initializing all needs components.
        /// </summary>
        public NewsPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_World(object sender, System.Windows.Input.GestureEventArgs e)
        {
            World.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            SaveChosen("http://feeds.bbci.co.uk/news/world/rss.xml");
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_Future(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Future.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            SaveChosen("http://www.bbc.com/travel/feed.rss");
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_Travel(object sender, System.Windows.Input.GestureEventArgs e)
        {
            World.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            SaveChosen("http://www.bbc.com/future/feed.rss");
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_History(object sender, System.Windows.Input.GestureEventArgs e)
        {
            History.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            SaveChosen("http://www.bbc.co.uk/history/0/rss.xml");
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_Nature(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Nature.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            SaveChosen("http://feeds.bbci.co.uk/nature/rss.xml");
        }



        /// <summary>
        /// This method send information to main page about chosen component
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GestureEventArgs</param>
        private void Grid_Tap_Health(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Health.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;

            SaveChosen("http://www.bbc.co.uk/health/0/rss.xml");
        }



        /// <summary>
        /// Save information about chosen category.
        /// </summary>
        private void SaveChosen(string data)
        {
            // Define the query to gather all of the to-do items.
            var ComponentItemInDB = from ViewModels.ComponentItem todo in ComponentDB.ComponentItems select todo;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            // Finding existing element from database
            var firstOccurence = ComponentItems.Where(item => item.ItemName == "News");

            if (firstOccurence.Count() == 0)
            {

                // Create a new to-do item based on the text box.
                ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "News", Data = data };

                // Add a to-do item to the observable collection.
                ComponentItems.Add(newComponent);

                // Add a component item to the local database
                ComponentDB.ComponentItems.InsertOnSubmit(newComponent);

            }
            else
            {
                // Delete Old record from database.
                ComponentDB.ComponentItems.DeleteAllOnSubmit(firstOccurence);

                // Add new record to database.
                // Create a new to-do item based on the text box.
                ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "News", Data = data };

                // Add a to-do item to the observable collection.
                ComponentItems.Add(newComponent);

                // Add a component item to the local database
                ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
            }


            // Save changes to the database.
            ComponentDB.SubmitChanges();

            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }



        /// <summary>
        ///  Method called after back from another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            World.Background    = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            Travel.Background   = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            Future.Background   = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            History.Background  = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            Nature.Background   = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
            Health.Background   = Application.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}