using System.Data.Linq;

namespace SeizeDay.ViewModels
{
   public class Component : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/Seizeday.sdf";

        // Pass the connection string to the base class.
        public Component(string connectionString): base(connectionString) { }

        // Specify a single table for the to-do items.
        public Table<ComponentItem> ComponentItems;
    }
}

