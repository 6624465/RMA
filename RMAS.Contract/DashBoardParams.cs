using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
  public class DashBoardParams:IContract
    {
        public DashBoardParams()
        {

        }
        public Int16 BranchID { get; set; }
        public int Year { get; set; }
        public Int16 FromMonth { get; set; }
        public Int16 ToMonth { get; set; }
    }
}
