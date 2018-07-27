using AngleSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudnyBot
{
    enum UrlLoaderState { YES, NO }

    class UrlLoader
    {
        static int _threadCount;

        public static UrlLoaderState State
        {
            get;
            set;
        }

        static UrlLoader()
        {
            _threadCount = 5;
            State = UrlLoaderState.YES;
        }

        public async static Task<CrawlerItemCollection> LoadUrlsFromPage(string link, string selector)
        {
            _threadCount--;

            Debug.WriteLine(_threadCount);

            if (_threadCount == 0)
            {
                State = UrlLoaderState.NO;
            }

            CrawlerItemCollection list = new CrawlerItemCollection(); ;

            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(link);
            var hrefs = document.QuerySelectorAll(selector);

            _threadCount++;
            State = UrlLoaderState.YES;

            foreach (var item in hrefs)
            {
                list.Add(new CrawlerItem(item.TextContent, item.GetAttribute("href")));
            }

            return list;
        }
    }
}
