using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudnyBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private  async void  btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string startUrl = txtStartUrl.Text;

            Crawler crawler = new Crawler(startUrl);

            CrawlerItemCollection items = new CrawlerItemCollection();

            items.Add(new CrawlerItem("Справочник ссузов", @"http://budny.by/abiturient/ssuzspravochnik"));

            items = await crawler.GetLinks(items, 0, "a.page", false, false);
            items = await crawler.GetLinks(items, 0 , "h2.edn_articleTitle>a", false, false);

            for(int i=0;i<items.Count;i++)
            {
                DrawTree(items);

                items = await crawler.GetLinks(items, i, "a.page", false, false);
                items = await crawler.GetLinks(items, i, "h2.edn_articleTitle>a", false, true);
            }

            Debug.WriteLine("End");

            DrawTree(items);


            //for (int i=0;i<items.Count;i++)
            //{
            //    if (UrlLoader.State==UrlLoaderState.YES)
            //    {
            //        crawler.GetLevelLinks(items, "h2.edn_articleTitle>a", false);
            //    }
            //}
        }

        private void DrawTree(CrawlerItemCollection task1)
        {
            treeMain.Items.Clear();

            for (int i = 0; i < task1.Count; i++)
            {
                if (!string.IsNullOrEmpty(task1[i].Title))
                {
                    TreeViewItem node = new TreeViewItem();
                    node.Header = task1[i].Title;
                    treeMain.Items.Add(node);
                }
            }
        }

        //private void AddItemsToNode(TreeViewItem node, CrawlerItem item)
        //{
        //    if (item.Items!=null)
        //    {
        //        for (int j = 0; j < item.Items.Count; j++)
        //        {
        //            TreeViewItem childNode = new TreeViewItem() { Header = item.Items[j].Title };
        //            node.Items.Add(childNode);
        //            if (item.Items[j].Items!=null)
        //            {
        //                AddItemsToNode(childNode, item.Items[j]);
        //            }
        //        }
        //    }
        //}
    }
}
