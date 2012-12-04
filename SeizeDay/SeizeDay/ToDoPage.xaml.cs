using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SeizeDay.ViewModels;
using Microsoft.Phone.Controls;

namespace SeizeDay
{
    /// <summary>
    /// This class storage task to do. 
    /// Possibility to added or remove from database.
    /// </summary>
    public partial class ToDoPage : PhoneApplicationPage, INotifyPropertyChanged
    {

        /// <summary>
        /// Data context for the local database
        /// </summary>
        private ViewModels.Component ComponentDB;

        /// <summary>
        ///  Define an observable collection property that controls can bind to.
        /// </summary>
        private ObservableCollection<ToDoItem> _toDoItems;

        /// <summary>
        ///  Define an observable collection property that controls can bind to.
        /// </summary>
        public ObservableCollection<ToDoItem> ToDoItems
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
        /// Constructor initializing all needs components.
        /// </summary>
        public ToDoPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;
        }



        /// <summary>
        /// This method back to MainPage and send info to add component to database.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/MainPage.xaml?component=listToDo", UriKind.Relative));
        }



        /// <summary>
        /// This method delete current task from list and database
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                ToDoItem toDoForDelete = button.DataContext as ToDoItem;

                // Remove the to-do item from the observable collection.
                ToDoItems.Remove(toDoForDelete);

                // Remove the to-do item from the local database.
                ComponentDB.ToDoItems.DeleteOnSubmit(toDoForDelete);

                // Save changes to the database.
                ComponentDB.SubmitChanges();

                // Put the focus back to the main page.
                this.Focus();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void newToDoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text box when it gets focus.
            newToDoTextBox.Text = String.Empty;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void newToDoAddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new to-do item based on the text box.
            ToDoItem newToDo = new ToDoItem { ItemName = newToDoTextBox.Text };

            // Add a to-do item to the observable collection.
            ToDoItems.Add(newToDo);

            // Add a to-do item to the local database.
            ComponentDB.ToDoItems.InsertOnSubmit(newToDo);

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



        /// <summary>
        /// Method called after back from another page.
        /// </summary>
        /// <param name="e">System.Windows.Navigation.NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var toDoItemsInDB = from ToDoItem todo in ComponentDB.ToDoItems select todo;

            // Execute the query and place the results into a collection.
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);

            // Call the base method.
            base.OnNavigatedTo(e);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    
    }
}