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

            alarm.Sound = new Uri("/Ringtones/01.wma", UriKind.Relative);
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
            NavigationService.GoBack();
        }
        
    }
}