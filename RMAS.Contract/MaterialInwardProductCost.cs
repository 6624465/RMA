using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
   public class MaterialInwardProductCost:IContract
    {
        public MaterialInwardProductCost()
        {

        }

        public int Cost { get; set; }
        public string ProductName { get; set; }

      //  public int MonthNo { get; set; }
        public int InvoiceYear { get; set; }
    }
}
