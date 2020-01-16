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
    /// Interaction logic for VMItemDisplay.xaml
    /// </summary>
    public partial class VMItemDisplay : UserControl
    {
        public VMItemDisplay()
        {
            InitializeComponent();

            dgSimple.ItemsSource = LoadSellsItems.SellsItems;
            dgSimple.Items.Refresh();

            dgCoins.ItemsSource = LoadChangeCoins.ChangeCoins;
            dgCoins.Items.Refresh();
        }

        private void OrderPlace(object sender, RoutedEventArgs e)
        {
            var inputCoins = new List<ChangeCoin>();
            inputCoins.Add(new ChangeCoin() { CoinAmount = Convert.ToInt32(cmbCoinAmount1.Text), CoinCount = Convert.ToInt32(txtCoinCount1.Text) });
            inputCoins.Add(new ChangeCoin() { CoinAmount = Convert.ToInt32(cmbCoinAmount2.Text), CoinCount = Convert.ToInt32(txtCoinCount2.Text) });
            inputCoins.Add(new ChangeCoin() { CoinAmount = Convert.ToInt32(cmbCoinAmount3.Text), CoinCount = Convert.ToInt32(txtCoinCount3.Text) });

            var OrderNumner = Convert.ToInt32(txtOrderNumber.Text);
            var inputAmount = inputCoins.Select(a => a.CoinAmount * a.CoinCount).Aggregate((a, b) => a + b);
            txtMessage.Text = "";
            var ordredItem = LoadSellsItems.SellsItems.FindAll(a => a.ItemNumber == OrderNumner).FirstOrDefault();
            if (ordredItem != null && ordredItem.ItemCount > 0 && inputAmount >= ordredItem.ItemPrice)
            {
                var chagneAmount = inputAmount - ordredItem.ItemPrice;

                if (chagneAmount > 0)
                {
                    var x = ChangeAmount.GetBestCoins(chagneAmount);

                    if (x != null)
                    {
                        ordredItem.ItemCount = ordredItem.ItemCount - 1;
                        txtMessage.Text = "Please collect item and coins"+ System.Environment.NewLine+x.Select(a => a.ToString()).Aggregate((a, b) => a + ", " + b);

                    }
                    else
                    {
                        txtMessage.Text = "Don't have change plese select some other item";
                    }
                }

                if (chagneAmount == 0)
                {
                    ordredItem.ItemCount = ordredItem.ItemCount - 1;
                    txtMessage.Text = "Please collect item";

                }
            }
            else
            {
                txtMessage.Text = "Please give input properly";
            }

            txtCoinCount1.Text = "0";
            txtCoinCount2.Text = "0";
            txtCoinCount3.Text = "0";

            dgSimple.ItemsSource = LoadSellsItems.SellsItems;
            dgSimple.Items.Refresh();

            dgCoins.ItemsSource = LoadChangeCoins.ChangeCoins;
            dgCoins.Items.Refresh();

        }
    }
}
