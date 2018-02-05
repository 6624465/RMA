using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace EZY.RMAS.Contract
{
    public class VendorRMADetails : IContract
    {
        public VendorRMADetails() { }
        public Int16 BranchID { get; set; }
        public string DoNo { get; set; }
        public string SerialNo { get; set; }

    }
}
