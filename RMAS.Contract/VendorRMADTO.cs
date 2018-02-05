using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
  public class VendorRMADTO : IContract
   
    {
        public VendorRMADTO()
        {

        }
        public List<VendorRMAHeader> VendorRMAHeader { get; set; }
    }
}
