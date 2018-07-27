using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudnyBot
{
    class CrawlerItem
    {

        public string Url
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public bool IsNewItem
        {
            get;
            set;
        }

        public CrawlerItem(string title, string url)
        {
            Url = url;
            Title = title;
            IsNewItem = true;
        }
    }
}
