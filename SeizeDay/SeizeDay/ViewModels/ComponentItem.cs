using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace SeizeDay.ViewModels
{
    /// <summary>
    /// Table in Database containing inforamtian about ...
    /// </summary>
    [Table]
    public class ComponentItem : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _componentItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ItemId
        {
            get
            {
                return _componentItemId;
            }
            set
            {
                if (_componentItemId != value)
                {
                    NotifyPropertyChanging("ComponentItemId");
                    _componentItemId = value;
                    NotifyPropertyChanged("ComponentItemId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // property for some more data like city&country for weather component
        private string _data;
        [Column]
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data != value)
                {
                    NotifyPropertyChanging("Data");
                    _data = value;
                    NotifyPropertyChanged("Data");
                }
            }
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
