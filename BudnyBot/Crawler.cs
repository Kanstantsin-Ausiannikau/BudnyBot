using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AngleSharp;

namespace BudnyBot
{
    class Crawler
    {
        string _startUrl;

        int _deepLevwl;

        string _domainName;

        CrawlerItemCollection _items;

        public Crawler(string startUrl, int deepLevel = 1, string domainName = "budny.by")
        {
            if (string.IsNullOrEmpty(startUrl)||deepLevel<1)
            {
                throw new ArgumentException("Некорректные параметры");
            }

            _startUrl = startUrl;
            _deepLevwl = deepLevel;
            _domainName = domainName;

            _items = new CrawlerItemCollection();
        }

        public async Task<CrawlerItemCollection> GetLinks(CrawlerItemCollection items, int nodeIndex, string selector, bool onlyExternalLinks, bool checkOldLink)
        {
            if (items == null||items[nodeIndex].IsNewItem ==false)
            {
                return items;
            }


            if (items == null)
            {
                items = new CrawlerItemCollection();
            }


            CrawlerItemCollection collection = new CrawlerItemCollection();

            collection = await new UrlLoader(items[nodeIndex].Url, selector).Load();

            items[nodeIndex].IsNewItem = !checkOldLink;

            items.AddRange(collection, nodeIndex+1);

            return items;
        }


        private bool IsExternal(string link)
        {
            return link.IndexOf(_domainName) == 0;
        }
    }
}
