using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
  public class RMACreditNoteValue:IContract
    {
        public RMACreditNoteValue()
        {

        }
        public decimal Cost { get; set; }

        public string MonthName { get; set; }
    }
}
