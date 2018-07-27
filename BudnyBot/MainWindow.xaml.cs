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

            var task = await crawler.GetCrawlerResults(null, @"http://budny.by/abiturient/ssuzspravochnik", 10);

            for(int i = 0;i<task.Count;i++)
            {
                if (!string.IsNullOrEmpty(task[i].Title))
                {
                    TreeViewItem node = new TreeViewItem();
                    node.Header = task[i].Title;
                    treeMain.Items.Add(node);

                    AddItemsToNode(node, task[i]);

                    //if (task[i].Items!=null)
                    //{
                    //    for (int j = 0; j < task[i].Items.Count; j++)
                    //    {
                    //        node.Items.Add(new TreeViewItem() { Header = task[i].Items[j].Title });
                    //    }
                    //}
                }
            }
        }

        private void AddItemsToNode(TreeViewItem node, CrawlerItem item)
        {
            if (item.Items!=null)
            {
                for (int j = 0; j < item.Items.Count; j++)
                {
                    TreeViewItem childNode = new TreeViewItem() { Header = item.Items[j].Title };
                    node.Items.Add(childNode);
                    if (item.Items[j].Items!=null)
                    {
                        AddItemsToNode(childNode, item.Items[j]);
                    }
                }
            }
        }
    }
}
