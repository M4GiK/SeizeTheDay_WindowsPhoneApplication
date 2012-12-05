using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeizeDay
{
    public class FeedItem
    {

        private string _title;
        private string _description;
        private string _link;
        private string _guid;
        private DateTime _published;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public DateTime Published
        {
            get { return _published; }
            set { _published = value; }
        }


    }
}
