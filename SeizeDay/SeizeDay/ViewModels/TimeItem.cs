using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace SeizeDay.ViewModels
{
    /// <summary>
    /// Table in Database containing inforamtian about saved alarms
    /// </summary>
    [Table]
    public class TimeItem : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _timeItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get
            {
                return _timeItemId;
            }
            set
            {
                if (_timeItemId != value)
                {
                    NotifyPropertyChanging("TimeItemId");
                    _timeItemId = value;
                    NotifyPropertyChanged("TimeItemId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _itemAlarmName;

        [Column]
        public string ItemAlarmName
        {
            get
            {
                return _itemAlarmName;
            }
            set
            {
                if (_itemAlarmName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemAlarmName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _itemReminderName;

        [Column]
        public string ItemReminderName
        {
            get
            {
                return _itemReminderName;
            }
            set
            {
                if (_itemReminderName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemReminderName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define completion value: private field, public property and database column.
        private string _dataField;

        [Column]
        public string DataField
        {
            get
            {
                return _dataField;
            }
            set
            {
                if (_dataField != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _dataField = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

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
