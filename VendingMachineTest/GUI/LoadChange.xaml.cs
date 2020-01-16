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
    public partial class LoadChange : UserControl
    {
        public LoadChange()
        {
            InitializeComponent();
            dgSimple.ItemsSource = VendingMachine.Model.LoadChangeCoins.ChangeCoins;
            dgSimple.Items.Refresh();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            VendingMachine.Model.LoadChangeCoins.ChangeCoins.Add(new ChangeCoin() { CoinAmount = Convert.ToInt32(txtCoinAmount.Text), CoinCount = Convert.ToInt32(txtCoinCount.Text) });

            txtCoinAmount.Text = "";
            txtCoinCount.Text = "";

            dgSimple.ItemsSource = VendingMachine.Model.LoadChangeCoins.ChangeCoins;
            dgSimple.Items.Refresh();
        }
    }
}
