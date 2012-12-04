using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using SeizeDay.ViewModels;

namespace AlarmAlarm
{
    /// <summary>
    /// Class responsible for adding alarm.
    /// </summary>
    public partial class AddAlarm : PhoneApplicationPage
    {
        /// <summary>
        /// Varible to need to set sound and alarm time
        /// </summary>
        private string alarmSound;

        /// <summary>
        /// Constructor initializing all needs components
        /// </summary>
        public AddAlarm()
        {
            InitializeComponent();
        }



        /// <summary>
        /// This method save selected time to remainder and alarm.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {

            // Generate a unique name for the new notification. You can choose a
            // name that is meaningful for your app, or just use a GUID.
            String nameReminder = System.Guid.NewGuid().ToString();
            String nameAlarm = System.Guid.NewGuid().ToString();

            // Get the begin time for the notification by combining the DatePicker
            // value and the TimePicker value.
            DateTime time = (DateTime)timePicker.Value;

            if ( time < DateTime.Now ) time = time.AddDays(1);

            Uri navigationUri = new Uri("/ShowSeizeDay.xaml", UriKind.Relative);

            Reminder reminder = new Reminder(nameReminder);

            reminder.Title = "“Determination is the wake-up call to the human will”";
            reminder.BeginTime = time.AddSeconds(15);        //beginTime;
            reminder.ExpirationTime = time.AddMinutes(2);    //expirationTime;
            reminder.NavigationUri = navigationUri;

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

            Alarm alarm = new Alarm(nameAlarm);

            if (alarmSound.Equals(""))
            {
                alarm.Sound = new Uri("/Ringtones/sensitivewalk.wma", UriKind.Relative);
            }
            else
            {
                alarm.Sound = new Uri(alarmSound, UriKind.Relative);
            }

            alarm.BeginTime = time;                         // beginTime;
            alarm.ExpirationTime = time.AddMinutes(2);      // expirationTime;

            // Register the alarm with the system.
            ScheduledActionService.Add(alarm);

            // Get vaule from AddAlarm page

            // Create a new to-do item based on the string.
            TimeItem newTimeItem = new TimeItem
            {
                ItemAlarmName = nameAlarm,
                ItemReminderName = nameReminder,
                DateField = time.ToString()
            };

            // Connect to the database and instantiate data context.
            Component ComponentDB = new Component(Component.DBConnectionString);

            // Add a alarm item to the local database.
            ComponentDB.TimeItems.InsertOnSubmit(newTimeItem);
            
            // Save changes to the database.
            ComponentDB.SubmitChanges();
            
            // Navigate back to the main page.
            NavigationService.Navigate(new Uri("/mainPage.xaml", UriKind.Relative)); ;
        }



        /// <summary>
        /// Method call new page with option to choose music
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ChooseSoundButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SoundCollectionPage.xaml", UriKind.Relative));
        }



        /// <summary>
        ///  Method called after back from another page.
        /// </summary>
        /// <param name="e">NavigationEventArgs</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedTo(e);

            string sound;

            // Try get value for component
            if (NavigationContext.QueryString.TryGetValue("sound", out sound))
            {
                alarmSound = "/Ringtones/" + sound;
            }
        }
    }
}