using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    /// <summary>
    /// Static coins list
    /// </summary>
    public static class LoadChangeCoins
    {
        public static List<ChangeCoin> ChangeCoins = new List<ChangeCoin>();

        /// <summary>
        /// For testing load example data
        /// </summary>
        static LoadChangeCoins()
        {
            ChangeCoins.Add(new ChangeCoin() { CoinAmount = 10, CoinCount = 50 });
            ChangeCoins.Add(new ChangeCoin() { CoinAmount = 5, CoinCount = 50 });
            ChangeCoins.Add(new ChangeCoin() { CoinAmount = 1, CoinCount = 100 });
        }
    }
}
