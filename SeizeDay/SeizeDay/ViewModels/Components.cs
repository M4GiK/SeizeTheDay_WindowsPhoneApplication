using System.Collections.ObjectModel;

namespace SeizeDay.ViewModels
{//Do pózniejszego użycia
    public class Components
    {
        public string Name { get; set; }
        public ObservableCollection<Component> ComponentList;

        public Components(string inName)
        {
            Name = inName;
            ComponentList = new ObservableCollection<Component>();
        }
    }
}
