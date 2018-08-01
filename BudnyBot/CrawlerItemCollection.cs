using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudnyBot
{
    class CrawlerItemCollection
    {
        List<CrawlerItem> _list;

        public CrawlerItemCollection()
        {
            _list = new List<CrawlerItem>();
        }

        public void Add(CrawlerItem item)
        {
            _list.Add(item);
        }

        public void AddRange(CrawlerItemCollection items, int index)
        {
            if (items==null)
            {
                throw new ArgumentException();
            }

            for (int i=0;i<items.Count;i++)
            {
                if (!IsContains(items[i]))
                {
                    //_list.Add(items[i]);
                    _list.Insert(index, items[i]);
                    index++;
                }
            }
        }

        public bool IsContains(CrawlerItem item)
        {
            return _list.Find(i=>i.Url==item.Url)!=null;
        }

        public int Count
        {
             get { return _list.Count; }
        }

        public CrawlerItem this[int index]
        {
            get { return _list[index]; }
        }
    }
}
