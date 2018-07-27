using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string startUrl = txtStartUrl.Text;

            Crawler crawler = new Crawler(startUrl);

            CrawlerItemCollection items = new CrawlerItemCollection();

            items.Add(new CrawlerItem("Справочник ссузов", @"http://budny.by/abiturient/ssuzspravochnik"));

            var task = await crawler.GetLevelLinks(items, "h2.edn_articleTitle>a", false);
            task.AddRange(await crawler.GetLevelLinks(items, "a.page", false));

            DrawTree(task);

            var task1 = await crawler.GetLevelLinks(task, "h2.edn_articleTitle>a", false);
            task1.AddRange(await crawler.GetLevelLinks(task, "a.page", false));

            DrawTree(task1);
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
