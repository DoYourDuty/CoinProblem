using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VendingMachine.Model
{

    /// <summary>
    /// static sells items
    /// </summary>
    public static class LoadSellsItems
    {

        public static List<SellsItem> SellsItems = new List<SellsItem>();

        /// <summary>
        /// For testing example items
        /// </summary>
        static LoadSellsItems()
        {
            SellsItems.Add(new SellsItem() { ItemCount = 5, ItemNumber = 101, ItemPrice = 25 });
            SellsItems.Add(new SellsItem() { ItemCount = 7, ItemNumber = 102, ItemPrice = 12 });
            SellsItems.Add(new SellsItem() { ItemCount = 10, ItemNumber = 103, ItemPrice = 2 });
            SellsItems.Add(new SellsItem() { ItemCount = 6, ItemNumber = 104, ItemPrice = 6 });
            SellsItems.Add(new SellsItem() { ItemCount = 4, ItemNumber = 105, ItemPrice = 5 });
            SellsItems.Add(new SellsItem() { ItemCount = 1, ItemNumber = 106, ItemPrice = 8 });
            SellsItems.Add(new SellsItem() { ItemCount = 2, ItemNumber = 107, ItemPrice = 13 });
            SellsItems.Add(new SellsItem() { ItemCount = 6, ItemNumber = 108, ItemPrice = 17 });
            SellsItems.Add(new SellsItem() { ItemCount = 8, ItemNumber = 109, ItemPrice = 20 });
        }

    }
}
