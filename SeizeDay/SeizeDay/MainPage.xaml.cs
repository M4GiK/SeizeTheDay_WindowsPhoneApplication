using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace SeizeDay
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
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

        //// Constructor
        //public MainPage()
        //{
        //    InitializeComponent();

        //     Set the data context of the listbox control to the sample data
        //    DataContext = App.ViewModel;
        //    this.Loaded += new RoutedEventHandler(MainPage_Loaded);


        //    ApplicationBar = new ApplicationBar();

        //    Button buttonAdd = new Button();
        //    buttonAdd.IconUri = new Uri("/Images/button+.png", UriKind.Relative);
        //    buttonAdd.Text = "Add";
        //    ApplicationBar.Buttons.Add(buttonAdd);
        //    buttonAdd.Click += new EventHandler(buttonAdd_Click);
        //}

        // Load data for the ViewModel Items
        //private void MainPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //loading bar
        //        myProgressBar.IsEnabled = true;
        //        myProgressBar.IsIndeterminate = true;
        //        myProgressBar.Visibility = System.Windows.Visibility.Visible;

        //        if (!App.ViewModel.IsDataLoaded)
        //        {
        //            App.ViewModel.LoadData();
        //        }

        //        //hide loading
        //        myProgressBar.IsEnabled = false;
        //        myProgressBar.IsIndeterminate = false;
        //        myProgressBar.Visibility = System.Windows.Visibility.Collapsed;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}
     

        //private void FirstListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
        //------------------------------------------------------------------------------------------------
       
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            ComponentDB = new ViewModels.Component(ViewModels.Component.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;
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

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Create a new to-do item based on the text box.
            ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = "component1" };

            // Add a to-do item to the observable collection.
            ComponentItems.Add(newComponent);

            // Add a to-do item to the local database.
            ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
        }

        //private void newToDoAddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Create a new to-do item based on the text box.
        //    ViewModels.ComponentItem newComponent = new ViewModels.ComponentItem { ItemName = newToDoTextBox.Text };

        //    // Add a to-do item to the observable collection.
        //    ViewModels.ComponentItem.Add(newComponent);

        //    // Add a to-do item to the local database.
        //    ComponentDB.ComponentItems.InsertOnSubmit(newComponent);
        //}

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            ComponentDB.SubmitChanges();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var ComponentItemInDB = from ViewModels.ComponentItem todo in ComponentDB.ComponentItems select todo;

            // Execute the query and place the results into a collection.
            ComponentItems = new ObservableCollection<ViewModels.ComponentItem>(ComponentItemInDB);

            // Call the base method.
            base.OnNavigatedTo(e);
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