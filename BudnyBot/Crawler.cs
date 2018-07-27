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

        public async Task<CrawlerItemCollection> GetLevelLinks(CrawlerItemCollection items, string selector, bool onlyExternalLinks)
        {
            if (items == null)
            {
                return null;
            }

            CrawlerItemCollection collection = new CrawlerItemCollection();

            //List<CrawlerItem> links = new List<CrawlerItem>();

            for (int i = 0; i < items.Count; i++)
            {
                while (UrlLoader.State != UrlLoaderState.YES)
                {
                    Thread.Sleep(500);



                    //links.AddRange(linkRange);

                }

                collection = await UrlLoader.LoadUrlsFromPage(items[i].Url, selector);

                Debug.Write("Ok");

                //var config = Configuration.Default.WithDefaultLoader();
                //var document = await BrowsingContext.New(config).OpenAsync(items[i].Url);
                //var hrefs = document.QuerySelectorAll(selector);

                //    foreach (var item in hrefs)
                //    {
                //        if (onlyExternalLinks)
                //        {
                //            if (IsExternal(item.GetAttribute("href")))
                //            {
                //                collection.Add(new CrawlerItem(item.TextContent, item.GetAttribute("href")));
                //            }
                //        }
                //        else
                //        {
                //            collection.Add(new CrawlerItem(item.TextContent, item.GetAttribute("href")));
                //        }
                //    }
                //}
                //return collection;


            }
            return collection;
        }

        private bool IsExternal(string link)
        {
            return link.IndexOf(_domainName) == 0;
        }
    }
}
