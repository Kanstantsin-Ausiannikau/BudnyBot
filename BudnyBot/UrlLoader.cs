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
        static int _threadCount = 5;

        string _url;
        string _selector;

        public static UrlLoaderState State
        {
            get;
            set;
        }

        static UrlLoader()
        {
            State = UrlLoaderState.YES;
        }

        public UrlLoader(string url, string selector)
        {
            this._url = url;
            this._selector = selector;
        }

        public async Task<CrawlerItemCollection> Load()
        {
            CrawlerItemCollection list = new CrawlerItemCollection();

            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(_url);
            var hrefs = document.QuerySelectorAll(_selector);

            foreach (var item in hrefs)
            {
                list.Add(new CrawlerItem(item.TextContent, item.GetAttribute("href")));
            }

            return list;
        }
    }
}
