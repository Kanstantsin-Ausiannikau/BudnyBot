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
                    treeMain.Items.Add($"{task[i].Title} ({task[i].Url})");
                }
            }
        }
    }
}
