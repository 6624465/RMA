using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public  class VendorRMAHeaderBO
    {
        private VendorRMAHeaderDAL vendorrmaheaderDAL;

        public VendorRMAHeaderBO()
        {
            vendorrmaheaderDAL = new VendorRMAHeaderDAL();
        }
        public bool SaveVendorRMAHeader(List<VendorRMAHeaderList> newItem)
        {

            return vendorrmaheaderDAL.SaveList(newItem);

        }
        public List<RMAHeader> GetVendorOutwardList(DataTableObject Obj)
        {
            return vendorrmaheaderDAL.GetVendorOutwardList(Obj);
        }
        public bool  CloseVendorRMAHeader(short BranchID,string ReferenceNo,string DocumentNo)
        {
            return  vendorrmaheaderDAL.CloseVendorRMAHeader(BranchID,ReferenceNo,DocumentNo);
        }

        public List<VendorRMAHeader> GetListByBranchID(Int16 branchID)
        {
            return vendorrmaheaderDAL.GetListByBranchID(branchID);
        }
    }
}
