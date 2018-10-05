using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
    public class JobFormDetail : IContract
    {
        // Constructor 
        public JobFormDetail() { }

		// Public Members 

		public Int16 ItemID { get; set; }

        public Int64 JobID { get; set; }

        public string ProductDescription { get; set; }

        public string ModelNo { get; set; }

        public string SerialNo { get; set; }

        public string Remarks { get; set; }

        public string FaultDescription { get; set; }

    }
}
