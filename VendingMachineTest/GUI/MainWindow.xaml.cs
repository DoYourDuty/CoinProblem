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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            userView.Content = new VMItemDisplay();
        }

        private void ChangeCoinsView(object sender, RoutedEventArgs e)
        {
            userView.Content = new LoadChange();
        }

        private void SellsItemView(object sender, RoutedEventArgs e)
        {
            userView.Content = new LoadItems();
        }

        private void MainView(object sender, RoutedEventArgs e)
        {
            userView.Content = new VMItemDisplay();
        }
    }
}
