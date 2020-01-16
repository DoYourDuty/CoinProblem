using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
    /// <summary>
    /// Sells item model
    /// </summary>
    public class SellsItem
    {
        public int ItemPrice { get; set; }
        public int ItemCount { get; set; }
        public int ItemNumber { get; set; }
    }
}
