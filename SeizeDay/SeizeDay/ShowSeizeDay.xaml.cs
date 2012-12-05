using System;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using SeizeDay.ViewModels;
using Microsoft.Phone.Scheduler;
using System.Collections.Generic;


namespace SeizeDay
{
    /// <summary>
    /// This class show significant components for user.
    /// </summary>
    public partial class ShowSeizeDay : PhoneApplicationPage
    {
        /// <summary>
        /// Data context for the local database
        /// </summary>
        private ViewModels.Component ComponentDB;

        /// <summary>
        /// Define an observable collection property that controls can bind to.
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
        /// Observable collection for property.
        /// </summary>
        private ObservableCollection<ViewModels.TimeItem> _timeItems;

        /// <summary>
        /// Define an observable collection property that controls can bind to.
        /// </summary>
        public ObservableCollection<ViewModels.TimeItem> TimeItems
        {
            get
            {
                return _timeItems;
            }
            set
            {
                if (_timeItems != value)
                {
                    _timeItems = value;
                    NotifyPropertyChanged("TimeItems");
                }
            }
        }

        /// <summary>
        ///  Define an observable collection property that controls can bind to.
        /// </summary>
        private ObservableCollection<ViewModels.ToDoItem> _toDoItems;

        /// <summary>
        ///  Define an observable collection property that controls can bind to.
        /// </summary>
        public ObservableCollection<ViewModels.ToDoItem> ToDoItems
        {
            get
            {
                return _toDoItems;
            }
            set
            {
                if (_toDoItems != value)
                {
                    _toDoItems = value;
                    NotifyPropertyChanged("ToDoItems");
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public ShowSeizeDay()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            //LoadComponents();

            // Checking if date is not validate, remove from database
            ValidateDate();

        }



        /// <summary>
        /// This method check, if date is not validate, remove from database
        /// </summary>
        private void ValidateDate()
        {

            // Define the query to gather all of the time items.
            var olderThanDate = from ViewModels.TimeItem times in ComponentDB.TimeItems.AsEnumerable() where times.DateField.toDate() < DateTime.Now.ToString().toDate() select times;

            TimeItems = new ObservableCollection<ViewModels.TimeItem>(olderThanDate);

            foreach (TimeItem timeItem in TimeItems)
            {
                ScheduledActionService.Remove(timeItem.ItemAlarmName);
                ScheduledActionService.Remove(timeItem.ItemReminderName);
            }

            // Delete time from the context.
            ComponentDB.TimeItems.DeleteAllOnSubmit(olderThanDate);

            // Save changes to the database.
            ComponentDB.SubmitChanges();

        }



        /// <summary>
        /// This method is load automaticlly with design xaml model.
        /// Set actual time for analog clock.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void Clock_Loaded(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;

            double hourAngle = ((float)now.Hour) / 12 * 360 + now.Minute / 2;

            double minuteAngle = ((float)now.Minute) / 60 * 360 + now.Second / 10;

            double secondAngle = ((float)now.Second) / 60 * 360;

            HourAnimation.From = hourAngle;
            HourAnimation.To = hourAngle + 360;

            MinuteAnimation.From = minuteAngle;
            MinuteAnimation.To = minuteAngle + 360;

            SecondAnimation.From = secondAngle;
            SecondAnimation.To = secondAngle + 360;

            ClockStoryboard.Begin();
        }



        /// <summary>
        /// Load data for the ViewModel Items
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Loading bar.
                myProgressBar.IsEnabled = true;
                myProgressBar.IsIndeterminate = true;
                myProgressBar.Visibility = System.Windows.Visibility.Visible;

                LoadComponents();

                Clock_Loaded(sender, e);

                // Hide loading.
                myProgressBar.IsEnabled = false;
                myProgressBar.IsIndeterminate = false;
                myProgressBar.Visibility = System.Windows.Visibility.Collapsed;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        private void LoadComponents()
        {

            // check which components should be loaded
            var ComponentItemInDB = from ViewModels.ComponentItem item in ComponentDB.ComponentItems select item;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            ListComponent(ComponentItems);

            WeatherComponent(ComponentItems);

            NewsComponent(ComponentItems);

            AphorismComponent(ComponentItems);

        }



        /// <summary>
        /// Load List to do component to list.
        /// </summary>
        /// <param name="ComponentItems">ObservableCollection<ViewModels.ComponentItem></param>
        private void ListComponent(ObservableCollection<ViewModels.ComponentItem> ComponentItems)
        {
            ViewModels.ComponentItem firstOccurence = ComponentItems.FirstOrDefault(item => item.ItemName == "List to do" );

            if (firstOccurence != null)
            {
                // Define the query to gather all of the to-do items which are not done.
                var toDoItemsInDB = from ViewModels.ToDoItem todo in ComponentDB.ToDoItems select todo;
                //var toDoItemsInDBProperly = ComponentDB.ToDoItems.Where(item => item.IsComplete == false);

                // Execute the query and place the results into a collection.
                ToDoItems = new ObservableCollection<ViewModels.ToDoItem>(toDoItemsInDB);
            }
            else
            {
                ListToDo.Visibility = Visibility.Collapsed;
            }
        }



        /// <summary>
        /// Load Weather component to list.
        /// </summary>
        /// <param name="ComponentItems">ObservableCollection<ViewModels.ComponentItem></param>
        private void WeatherComponent(ObservableCollection<ViewModels.ComponentItem> ComponentItems)
        {
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
            else
            {
                Weather.Visibility = Visibility.Collapsed;
            }
        }



        /// <summary>
        /// Load News component to list.
        /// </summary>
        /// <param name="ComponentItems">ObservableCollection<ViewModels.ComponentItem> ComponentItems</param>
        private void NewsComponent(ObservableCollection<ViewModels.ComponentItem> ComponentItems)
        {

            ViewModels.ComponentItem firstOccurence = ComponentItems.FirstOrDefault(item => item.ItemName == "News");

            if (firstOccurence != null)
            {
                
            }
            else
            {
                News.Visibility = Visibility.Collapsed;
            }
        }



        /// <summary>
        /// Load Aphorism component to list.
        /// </summary>
        /// <param name="ComponentItems">ObservableCollection<ViewModels.ComponentItem></param>
        private void AphorismComponent(ObservableCollection<ViewModels.ComponentItem> ComponentItems)
        {
            ViewModels.ComponentItem firstOccurence = ComponentItems.FirstOrDefault(item => item.ItemName == "Aphorism");

            if (firstOccurence != null)
            {
                
            }
            else
            {
                Aphorism.Visibility = Visibility.Collapsed;
            }
        }




        /// <summary>
        /// Method fill inforamtion in component inside list. Also get information form internet about weather.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DownloadStringCompletedEventArgs</param>
        void webclient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Show message about problem with connection.
                MessageBox.Show("You have problem with internet connection");

                WeatherInfoCurrent noInformationWeather = new WeatherInfoCurrent { observation_time = "No internet connection" };

                this.listBoxc.ItemsSource = new List<WeatherInfoCurrent>() { noInformationWeather };
            }
            else
            {
                XElement XmlWeather = XElement.Parse(e.Result);

                listBoxc.ItemsSource = from weather in XmlWeather.Descendants("current_condition")

                                       select new WeatherInfoCurrent
                                       {
                                           observation_time = weather.Element("observation_time").Value,
                                           temp_C = weather.Element("temp_C").Value + "°C",
                                           temp_F = weather.Element("temp_F").Value + "°F",
                                           icon = weather.Element("weatherIconUrl").Value,
                                           condition = weather.Element("weatherDesc").Value
                                       };
            }
        }



        /// <summary>
        /// Method called before going to another page.
        /// </summary>
        /// <param name="e">System.Windows.Navigation.NavigationEventArgs</param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            ComponentDB.SubmitChanges();
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