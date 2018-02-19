using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
    public class COODetail : IContract
    {
        // Constructor 
        public COODetail() { }

		// Public Members 

		public Int16 BranchID { get; set; }

        public string DocumentNo { get; set; }

        public string ModelNo { get; set; }

        public string Description { get; set; }

        public Int16 Qty { get; set; }

        public string Origin { get; set; }


    }
}
