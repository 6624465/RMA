using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
   public class RMACreditNoteProductCategory:IContract
    {
        public RMACreditNoteProductCategory()
        {

        }
        public int ProductCount { get; set; }
        public string ProductName { get; set; }
    }
}
