using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using SeizeDay.ViewModels;

namespace SeizeDay
{
    /// <summary>
    /// This class is the core of the entire application.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        /// <summary>
        /// Variable to storage temporary information about time list.
        /// </summary>
        private TimeItem itemTimeToDelete;

        /// <summary>
        /// Variable to storage temporary information about component list.
        /// </summary>
        private ComponentItem itemComponentToDelete;

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
        /// Contructor initializing all component of application
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            // Checking if date is not validate, remove from database
            ValidateDate();

            // Checking if any element is in lists
            CheckAddedElements();
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

                if (!App.ViewModel.IsDataLoaded)
                {
                    App.ViewModel.LoadData();
                }

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
        /// This method check, if date is not validate, remove from database
        /// </summary>
        private void ValidateDate()
        {

            // Define the query to gather all of the time items.
            var olderThanDate = from ViewModels.TimeItem times in ComponentDB.TimeItems.AsEnumerable() where times.DateField.toDate() < DateTime.Now.ToString().toDate() select times;
            
            // Delete time from the context.
            ComponentDB.TimeItems.DeleteAllOnSubmit(olderThanDate);

            // Save changes to the database.
            ComponentDB.SubmitChanges();
            
        }



        /// <summary>
        /// Method adding new component to main page.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void AddComponent_Click(object sender, EventArgs e)
        {
            // Navigate to the ComponentPage page when the add button is clicked.
            NavigationService.Navigate(new Uri("/ComponentPage.xaml", UriKind.Relative));

        }



        /// <summary>
        /// This method delete component from list and database.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void DeleteComponent_Click(object sender, RoutedEventArgs e)
        {
            if (itemComponentToDelete != null)
            {

                ComponentItems.Remove(itemComponentToDelete);
                ComponentDB.ComponentItems.DeleteOnSubmit(itemComponentToDelete);

                // Set to null itemTimeToDelete
                itemComponentToDelete = null;

                // Save changes to the database.
                ComponentDB.SubmitChanges();

                // Checking if any element is in lists
                CheckAddedElements();
            }
        }



        /// <summary>
        /// This method moved view to new window, where is possibility to add new alarm.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ButtonAddAlarm_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the AddAlarm page when the add button is clicked.
            NavigationService.Navigate(new Uri("/AddAlarm.xaml", UriKind.Relative));

        }



        /// <summary>
        /// This method delete alarm time form list and database.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ButtonDeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
      
            if (itemTimeToDelete != null)
            {
                // Call Remove to unregister the scheduled action with the service.
                ScheduledActionService.Remove(itemTimeToDelete.ItemAlarmName);
                ScheduledActionService.Remove(itemTimeToDelete.ItemReminderName);
                
                TimeItems.Remove(itemTimeToDelete);
                ComponentDB.TimeItems.DeleteOnSubmit(itemTimeToDelete);
                
                // Set to null itemTimeToDelete
                itemTimeToDelete = null;

                // Save changes to the database.
                ComponentDB.SubmitChanges();

                // Checking if any element is in lists
                CheckAddedElements();
            }
   
        }



        /// <summary>
        /// This method select current object from list and marked selected item.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SelectionChangedEventArgs</param>
        private void listBoxComponent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var mySelectedItem = e.AddedItems[0] as ComponentItem;
                if (mySelectedItem != null)
                {
                    itemComponentToDelete = mySelectedItem;
                }
            }

        }



        /// <summary>
        /// This method select current object from list and marked selected item.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SelectionChangedEventArgs</param>
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var mySelectedItem = e.AddedItems[0] as TimeItem;
                if (mySelectedItem != null)
                {
                    itemTimeToDelete = mySelectedItem;            
                }
            }
        }



        /// <summary>
        /// Method checking if any elements is added to alarm list or component list.
        /// If is added, remove arrows and descryption.
        /// </summary>
        private void CheckAddedElements()
        {
            var ComponentItemInDB = from ViewModels.ComponentItem component in ComponentDB.ComponentItems select component;

            if (ComponentItemInDB.Count() > 0)
            {
                text2.Visibility = Visibility.Collapsed;
                arrow2.Visibility = Visibility.Collapsed;
            }
            else
            {
                text2.Visibility = Visibility.Visible;
                arrow2.Visibility = Visibility.Visible;
            }

            var TimeItemInDB = from ViewModels.TimeItem times in ComponentDB.TimeItems select times;

            if (TimeItemInDB.Count() > 0)
            {
                text1.Visibility = Visibility.Collapsed;
                arrow1.Visibility = Visibility.Collapsed;
            }
            else
            {
                text1.Visibility = Visibility.Visible;
                arrow1.Visibility = Visibility.Visible;
            }

        }



        /// <summary>
        /// This method moves to chosen page after double click on current element.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Microsoft.Phone.Controls..GestureEventArgs</param>
         private void DoubleTap_action(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if ( itemComponentToDelete.ItemName == "List to do" )
            {
                // Navigate back to the ToDoPage page.
                NavigationService.Navigate(new Uri("/ToDoPage.xaml", UriKind.Relative));
            }
            else if (itemComponentToDelete.ItemName == "Weather")
            {
                // Navigate back to the WeatherSettings page.
                NavigationService.Navigate(new Uri("/WeatherSettings.xaml", UriKind.Relative));
            }
            else if (itemComponentToDelete.ItemName == "News")
            {
                // Navigate back to the NewsPage page.
                NavigationService.Navigate(new Uri("/NewsPage.xaml", UriKind.Relative));
            }
            else if (itemComponentToDelete.ItemName == "Aphorism")
            {


            }
        }



        /// <summary>
        /// Method called before going to another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            ComponentDB.SubmitChanges();
        }



        /// <summary>
        /// Method called after back from another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedTo(e);

            // Define the query to gather all of the to-do items.
            var ComponentItemInDB = from ViewModels.ComponentItem todo in ComponentDB.ComponentItems select todo;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            // Create a strings for containing data about chosen component
            string component;

            // Try get value for component
            if (NavigationContext.QueryString.TryGetValue("component", out component))
            {
                if (component.Equals("listToDo"))
                  {
                      var firstOccurence = ComponentItems.Where(item => item.ItemName == "List to do");
  
                      if (firstOccurence.Count() == 0)
                      {
                          // Create a new to-do item based on the text box.
                          ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "List to do"};
  
                          // Add a to-do item to the observable collection.
                          ComponentItems.Add(newComponent);
  
                          // Add a component item to the local database
                          ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
                      }
                }
                else if (component.Equals("aphorism"))
                {
                    var firstOccurence = ComponentItems.Where(item => item.ItemName == "Aphorism");

                    if (firstOccurence.Count() == 0)
                    {
                        // Create a new to-do item based on the text box.
                        ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "Aphorism" };

                        // Add a to-do item to the observable collection.
                        ComponentItems.Add(newComponent);

                        // Add a component item to the local database
                        ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
                    }
                }
            }


            // Define the query to gather all of the time items.
            var TimeItemInDB = from ViewModels.TimeItem times in ComponentDB.TimeItems select times;

            // Execute the query and place the results into a collection.
            TimeItems = new ObservableCollection<ViewModels.TimeItem>(TimeItemInDB);

            // Save changes to the database.
            ComponentDB.SubmitChanges();

            // Checking if any element is in lists
            CheckAddedElements();
            
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