using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace SeizeDay
{
    public partial class ShowSeizeDay : PhoneApplicationPage
    {
        public ShowSeizeDay()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            LoadComponents();
        }

        // Data context for the local database
        private ViewModels.Component ComponentDB;

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<ViewModels.ComponentItem> _componentItems;
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

        void LoadComponents()
        {
            // TODO
            // check which components should be loaded
            var ComponentItemInDB = from ViewModels.ComponentItem weather in ComponentDB.ComponentItems select weather;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            ViewModels.ComponentItem firstOccurence = ComponentItems.FirstOrDefault(item => item.ItemName == "Weather");
            
            if (firstOccurence != null)
            {
                // get data for weather: 
                string city = firstOccurence.Data; // "Cracow,Poland"; // get from DB
                WebClient webclient = new WebClient();
                webclient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webclient_DownloadStringCompleted);

                webclient.DownloadStringAsync(new Uri("http://free.worldweatheronline.com/feed/weather.ashx?key=bed6524371124406111310&q=" + city + "&num_of_days=1&format=xml")); // weather location 
                // http://free.worldweatheronline.com/feed/weather.ashx?key=bed6524371124406111310&q=mumbai,india&num_of_days=3&format=xml
            }
         }

        void webclient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("error");

            }

            XElement XmlWeather = XElement.Parse(e.Result);

            listBoxc.ItemsSource = from weather in XmlWeather.Descendants("current_condition")

                                   select new WeatherInfoCurrent
                                   {
                                       observation_time = weather.Element("observation_time").Value,
                                       temp_C = weather.Element("temp_C").Value,
                                       temp_F = weather.Element("temp_F").Value,
                                       icon = weather.Element("weatherIconUrl").Value,
                                       condition = weather.Element("weatherDesc").Value
                                   };
        }
    }
}