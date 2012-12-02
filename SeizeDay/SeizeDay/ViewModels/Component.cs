using System.Data.Linq;

namespace SeizeDay.ViewModels
{
   public class Component : DataContext
    {
        /// <summary>
        /// Specify the connection string as a static, used in main page and app.xaml.
        /// </summary>
        public static string DBConnectionString = "Data Source=isostore:/Seizeday.sdf";

        /// <summary>
        /// Pass the connection string to the base class.
        /// </summary>
        /// <param name="connectionString"></param>
        public Component(string connectionString): base(connectionString) { }

        /// <summary>
        /// Specify a single table for the to-do items.
        /// </summary>
        public Table<ComponentItem> ComponentItems;

        /// <summary>
        /// Specify a single table for the time items.
        /// </summary>
        public Table<TimeItem> TimeItems;
    }
}

