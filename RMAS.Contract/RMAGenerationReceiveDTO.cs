using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
    public class RMAGenerationReceiveDTO : IContract
    {
        public RMAGenerationReceiveDTO()
        {

        }
        public Int16 Branch { get; set; }
        public int ProductCode { get; set; }
        public Int16 FromMonth { get; set; }
        public Int16 ToMonth { get; set; }
        public Int16 Year { get; set; }
        public List<RMAGenerationReturnedValues> RMAGeneration { get; set; }
        public List<RMAGenerationReturnedValues> RMAReturned { get; set; }
    }
}
