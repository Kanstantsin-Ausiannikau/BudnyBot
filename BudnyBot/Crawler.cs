using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using AngleSharp;

namespace BudnyBot
{
    class Crawler
    {
        string _startUrl;

        int _deepLevwl;

        CrawlerItemCollection _items;

        public Crawler(string startUrl, int deepLevel = 1)
        {
            if (string.IsNullOrEmpty(startUrl)||deepLevel<1)
            {
                throw new ArgumentException("Некорректные параметры");
            }

            _startUrl = startUrl;
            _deepLevwl = deepLevel;
            _items = new CrawlerItemCollection();
        }

        public async Task<CrawlerItemCollection> GetCrawlerResults(CrawlerItemCollection items, string link, int deepLevel)
        {
            items = new CrawlerItemCollection();

            items.Add(new CrawlerItem("Справочник ссузов", link));

            var scan1 = await GetLevelLinks(items, "h2.edn_articleTitle>a");
            var scan2 = await GetLevelLinks(items, "a.page");

            //items[0].Items = new CrawlerItemCollection();
            //items[0].Items.AddRange(scan1[0].Items);
            //items[0].Items.AddRange(scan2[0].Items);

            return items;
        }

        public async Task<CrawlerItemCollection> GetLevelLinks(CrawlerItemCollection items, string selector)
        {
            if (items == null)
            {
                return null;
            }

            for(int i=0;i<items.Count;i++)
            {
                items[i].Items = new CrawlerItemCollection();
                var config = Configuration.Default.WithDefaultLoader();
                var document = await BrowsingContext.New(config).OpenAsync(items[i].Url);
                var hrefs = document.QuerySelectorAll(selector);

                foreach (var element in hrefs)
                {
                    items[i].Items.Add(new CrawlerItem(element.TextContent, element.GetAttribute("href")));
                }
            }
            return items;
        }
    }
}
