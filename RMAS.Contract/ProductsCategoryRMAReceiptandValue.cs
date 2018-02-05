using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
   public class ProductsCategoryRMAReceiptandValue:IContract
    {
        public ProductsCategoryRMAReceiptandValue()
        {

        }
        public int ProductQty { get; set; }
        public decimal ProductValue { get; set; }
        public string ProductName { get; set; }

    }
}
