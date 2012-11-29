using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;

namespace AlarmAlarm
{
    public partial class AddAlarm : PhoneApplicationPage
    {
        public AddAlarm()
        {
            InitializeComponent();
        }
        private void ApplicationBarSaveButton_Click(object sender, EventArgs e)
        {
            // The code in the following steps goes here.

            // Generate a unique name for the new notification. You can choose a
            // name that is meaningful for your app, or just use a GUID.
            String name = System.Guid.NewGuid().ToString();

            //// Get the begin time for the notification by combining the DatePicker
            //// value and the TimePicker value.
            //DateTime date = (DateTime)beginDatePicker.Value;
            //DateTime time = (DateTime)beginTimePicker.Value;
            //DateTime beginTime = date + time.TimeOfDay;

            //// Make sure that the begin time has not already passed.
            //if (beginTime < DateTime.Now)
            //{
            //    MessageBox.Show("the begin date must be in the future.");
            //    return;
            //}

            //// Get the expiration time for the notification.
            //date = (DateTime)expirationDatePicker.Value;
            //time = (DateTime)expirationTimePicker.Value;
            //DateTime expirationTime = date + time.TimeOfDay;

            //// Make sure that the expiration time is after the begin time.
            //if (expirationTime < beginTime)
            //{
            //    MessageBox.Show("expiration time must be after the begin time.");
            //    return;
            //}




            Uri navigationUri = new Uri("/ShowParams.xaml", UriKind.Relative);

     //       if ((bool)reminderRadioButton.IsChecked)
            {
                Reminder reminder = new Reminder(name);
             //   reminder.Title = titleTextBox.Text;
             //   reminder.Content = contentTextBox.Text;
                reminder.BeginTime = DateTime.Now.AddMinutes(1); //beginTime;
                reminder.ExpirationTime = DateTime.Now.AddMinutes(2); //expirationTime;
                reminder.NavigationUri = navigationUri;

                // Register the reminder with the system.
                ScheduledActionService.Add(reminder);
            }
    //        else
            {
                Alarm alarm = new Alarm(name);
         //       alarm.Content = contentTextBox.Text;
                alarm.Sound = new Uri("/Ringtones/Ring01.wma", UriKind.Relative);
                alarm.BeginTime = DateTime.Now.AddMinutes(1); // beginTime;
                alarm.ExpirationTime = DateTime.Now.AddMinutes(2); // expirationTime;
  //              alarm.RecurrenceType = recurrence;

   //             ScheduledActionService.Add(alarm);
            }

            // Navigate back to the main reminder list page.
            NavigationService.GoBack();
        }

        private void btnSaveAlarm_Click(object sender, RoutedEventArgs e)
        {
            // parse time of alarm
            int h = Convert.ToInt16(tbxAlarmHour.Text);
            int min = Convert.ToInt16(tbxAlarmMin.Text);
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, h, min, 0);
            if (
                dt < DateTime.Now) dt = dt.AddDays(1);
            
            Uri navigationUri = new Uri("/ShowAlarm.xaml", UriKind.Relative);

            String name = System.Guid.NewGuid().ToString();
            // create reminder
            Reminder reminder = new Reminder(name);
           // reminder.Title = titleTextBox.Text;
           // reminder.Content = contentTextBox.Text;
            reminder.BeginTime = dt;//DateTime.Now.AddMinutes(1); //beginTime;
            reminder.ExpirationTime = dt.AddMinutes(2);// DateTime.Now.AddMinutes(2); //expirationTime;
            reminder.NavigationUri = navigationUri;

            // Register the reminder with the system.
            ScheduledActionService.Add(reminder);

            NavigationService.GoBack();
        }
    }
}