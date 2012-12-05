using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Linq;

namespace SeizeDay
{
    /// <summary>
    /// This class get inforamtion about user location.
    /// </summary>
    public partial class WeatherSettings : PhoneApplicationPage, INotifyPropertyChanged
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
        public WeatherSettings()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);
        }



        /// <summary>
        /// Send information about chosen location to main page.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var ComponentItemInDB = from ViewModels.ComponentItem todo in ComponentDB.ComponentItems select todo;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            // Finding existing element from database
            var firstOccurence = ComponentItems.Where(item => item.ItemName == "Weather");

            if (firstOccurence.Count() == 0)
            {

                // Create a new to-do item based on the text box.
                ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "Weather", Data = tbxCity.Text + "," + tbxCountry.Text };

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
                ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "Weather", Data = tbxCity.Text + "," + tbxCountry.Text };

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