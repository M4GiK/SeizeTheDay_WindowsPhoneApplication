using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;


namespace SeizeDay
{
    /// <summary>
    /// This class is the core of the entire application.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        /// <summary>
        /// Variable to storage notifications.
        /// </summary>
        IEnumerable<ScheduledNotification> notifications;

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

                // Hide loading.
                myProgressBar.IsEnabled = false;
                myProgressBar.IsIndeterminate = false;
                myProgressBar.Visibility = System.Windows.Visibility.Collapsed;

                Clock_Loaded(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Create a new to-do item based on the text box.
            ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "component1" };

            // Add a to-do item to the observable collection.
            ComponentItems.Add(newComponent);

            // Add a to-do item to the local database.
            ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
        }



        /// <summary>
        /// This method check, if date is not validate, remove from database
        /// </summary>
        private void ValidateDate()
        {
            for (int i = 0; i < 10; i++)
            {

                // Define the query to gather all of the time items.
                //IQueryable TimeQuery = from ViewModels.TimeItem times in ComponentDB.TimeItems where toDate(times.DataField) < DateTime.Now select times;

                //// Execute the query and place the results into a collection.
                ////TimeItems = new ObservableCollection<ViewModels.TimeItem>(TimeQuery);

                //// Delete city from the context.
                //ComponentDB.TimeItems.DeleteOnSubmit(TimeQuery);

                //// Save changes to the database.
                //ComponentDB.SubmitChanges();

            }

        }



        /// <summary>
        /// This method changing date in string to object in DataTime
        /// </summary>
        /// <param name="dateString">string</param>
        /// <returns>Date in DateTime format</returns>
        private DateTime toDate(string dateString)
        {
            return DateTime.Parse(dateString);
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
        /// Method adding new component to main page.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void AddComponent_Click(object sender, EventArgs e)
        {
            // Navigate to the ComponentPage page when the add button is clicked.
            NavigationService.Navigate(new Uri("/ComponentPage.xaml", UriKind.Relative));

            //// Create a new to-do item based on the text box.
            //ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "component1" };

            //// Add a to-do item to the observable collection.
            //ComponentItems.Add(newComponent);

            //// Add a to-do item to the local database.
            //ComponentDB.ComponentItems.InsertOnSubmit(newComponent);

        }



        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            // The scheduled action name is stored in the Tag property
            // of the delete button for each reminder.
            string name = (string)((Button)sender).Tag;

            // Call Remove to unregister the scheduled action with the service.
            ScheduledActionService.Remove(name);

            // Reset the ReminderListBox items
            //ResetItemsList();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SelectionChangedEventArgs</param>
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("KLIKNIETE" + sender + " ,  " + e);

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
        ///  Method called after back from another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var ComponentItemInDB = from ViewModels.ComponentItem todo in ComponentDB.ComponentItems select todo;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            // Define the query to gather all of the time items.
            var TimeItemInDB = from ViewModels.TimeItem times in ComponentDB.TimeItems select times;

            // Execute the query and place the results into a collection.
            TimeItems = new ObservableCollection<ViewModels.TimeItem>(TimeItemInDB);

            //Reset the ReminderListBox items when the page is navigated to.
            //ResetItemsList();

            // Call the base method.
            base.OnNavigatedTo(e);

            // Create a strings for containing data about alarm
            string alarmName;
            string reminderName;
            string time;

            // Get vaule from AddAlarm page
            NavigationContext.QueryString.TryGetValue("alarm", out alarmName);
            NavigationContext.QueryString.TryGetValue("reminder", out reminderName);
            NavigationContext.QueryString.TryGetValue("time", out time);

            // Create a new to-do item based on the string.
            ViewModels.TimeItem newComponent = new ViewModels.TimeItem
            {
                ItemAlarmName = alarmName,
                ItemReminderName = reminderName,
                DataField = time
            };

            // Add a to-do item to the observable collection.
            TimeItems.Add(newComponent);

            // Add a to-do item to the local database.
            ComponentDB.TimeItems.InsertOnSubmit(newComponent);


        }











        //private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Cast parameter as a button.
        //    var button = sender as Button;

        //    if (button != null)
        //    {
        //        // Get a handle for the to-do item bound to the button.
        //        ToDoItem toDoForDelete = button.DataContext as ToDoItem;

        //        // Remove the to-do item from the observable collection.
        //        ToDoItems.Remove(toDoForDelete);

        //        // Remove the to-do item from the local database.
        //        toDoDB.ToDoItems.DeleteOnSubmit(toDoForDelete);

        //        // Save changes to the database.
        //        toDoDB.SubmitChanges();

        //        // Put the focus back to the main page.
        //        this.Focus();
        //    }
        //}

        //private void newToDoTextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    // Clear the text box when it gets focus.
        //    newToDoTextBox.Text = String.Empty;
        //}




        //private void newToDoAddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Create a new to-do item based on the text box.
        //    ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = newToDoTextBox.Text };

        //    // Add a to-do item to the observable collection.
        //    ViewModels.ComponentItem.Add(newComponent);

        //    // Add a to-do item to the local database.
        //    ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
        //}






        //private void ResetItemsList()
        //{
        //    // Use GetActions to retrieve all of the scheduled actions
        //    // stored for this application. The type <Reminder> is specified
        //    // to retrieve only Reminder objects.
        //    //reminders = ScheduledActionService.GetActions<Reminder>();
        //    notifications = ScheduledActionService.GetActions<ScheduledNotification>();

        //    // If there are 1 or more reminders, hide the "no reminders"
        //    // TextBlock. IF there are zero reminders, show the TextBlock.
        //    //if (reminders.Count<Reminder>() > 0)
        //    if (notifications.Count<ScheduledNotification>() > 0)
        //    {
        //        EmptyTextBlock.Visibility = Visibility.Collapsed;
        //        btnAddAlarm.Visibility = Visibility.Collapsed;
        //        NotificationListBox.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        EmptyTextBlock.Visibility = Visibility.Visible;
        //        btnAddAlarm.Visibility = Visibility.Visible;
        //        NotificationListBox.Visibility = Visibility.Collapsed;
        //    }

            
        //    // Update the ReminderListBox with the list of reminders.
        //    // A full MVVM implementation can automate this step.
        //    NotificationListBox.ItemsSource = notifications;
        //}


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