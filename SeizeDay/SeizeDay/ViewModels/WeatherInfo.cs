using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SeizeDay
{

    public class WeatherInfo
    {
        public string day_of_week { get; set; }
        public string low { get; set; }
        public string high { get; set; }
        public string icon { get; set; }
        public string condition { get; set; }
    }

    public class WeatherInfoCurrent
    {
        public string observation_time { get; set; }
        public string temp_C { get; set; }
        public string temp_F { get; set; }
        public string icon { get; set; }
        public string condition { get; set; }
    }

}
