using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudnyBot
{
    class CrawlerItem
    {
        CrawlerItemCollection _items;

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

        public CrawlerItemCollection Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public CrawlerItem(string title, string url)
        {
            this.Url = url;
            this.Title = title;
        }
    }
}
