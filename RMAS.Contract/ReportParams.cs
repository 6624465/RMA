using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EZY.RMAS.Contract
{
   public class ReportParams:IContract
    {
        public ReportParams()
        {

        }

        public int BranchID { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
        public int Year { get; set; }     
        public int ProductCode { get; set; }
     
    }
}
