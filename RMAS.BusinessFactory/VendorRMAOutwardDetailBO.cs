using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.BusinessFactory
{
    public class VendorRMAOutwardDetailBO
    {
        private VendorRMAOutwardDetailDAL vendorRMAOutwardDetailDAL;
        public VendorRMAOutwardDetailBO()
        {

            vendorRMAOutwardDetailDAL = new VendorRMAOutwardDetailDAL();
        }

        public List<VendorRMAOutwardDetail> VendorRMAOutWardDetailListByRMAInWardDocumentNo(string documentNo,Int16 branchID)
        {
             return (List<VendorRMAOutwardDetail>)vendorRMAOutwardDetailDAL.VendorRMAOutWardDetailListByRMAInWardDocumentNo(documentNo,branchID);
        }
    }
}
