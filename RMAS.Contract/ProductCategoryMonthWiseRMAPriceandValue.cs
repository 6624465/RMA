using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
public class ProductCategoryMonthWiseRMAPriceandValue:IContract
    {
        public ProductCategoryMonthWiseRMAPriceandValue()
        {

        }
        public int ProductQty { get; set; }
        public decimal ProductValue { get; set; }
        public int MonthNo { get; set; }
        public string MonthName { get; set; }
    }
}
