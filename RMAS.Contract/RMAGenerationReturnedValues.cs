using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
   public class RMAGenerationReturnedValues:IContract
    {
        public RMAGenerationReturnedValues()
        {

        }
        public int DocumentNoCount { get; set; }
        public int MonthNo { get; set; }
        public string MonthName { get; set; }
        public int IncidentYear { get; set; }
    }
}
