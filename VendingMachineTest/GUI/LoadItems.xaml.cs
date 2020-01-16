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
using VendingMachine.Model;

namespace GUI
{
    /// <summary>
    /// Interaction logic for LoadItems.xaml
    /// </summary>
    public partial class LoadItems : UserControl
    {
        public LoadItems()
        {
            InitializeComponent();
            dgSimple.ItemsSource = VendingMachine.Model.LoadSellsItems.SellsItems;
            dgSimple.Items.Refresh();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            VendingMachine.Model.LoadSellsItems.SellsItems.Add(new SellsItem() { ItemCount = Convert.ToInt32(txtItemCount.Text), ItemNumber = Convert.ToInt32(txtItemNumber.Text), ItemPrice = Convert.ToInt32(txtItemPrice.Text) });

            txtItemCount.Text = "";
            txtItemPrice.Text = "";
            txtItemNumber.Text = "";

            dgSimple.ItemsSource = VendingMachine.Model.LoadSellsItems.SellsItems;
            dgSimple.Items.Refresh();
        }
    }
}
