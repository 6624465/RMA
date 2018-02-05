using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{

    public class RMAHeaderBO
    {
        private RMAHeaderDAL rmaheaderDAL;
        public RMAHeaderBO()
        {

            rmaheaderDAL = new RMAHeaderDAL();
        }

        public List<RMAHeader> GetList(string countryCode)
        {
            return rmaheaderDAL.GetList(countryCode);
        }

        public List<RMAHeader> GetListByBranchID(Int16 branchID)
        {
            return rmaheaderDAL.GetListByBranchID(branchID);
        }
        public List<RMAHeader> GetListByBranchIDForVendor(Int16 branchID)
        {
            return rmaheaderDAL.GetListByBranchIDForVendor(branchID);
        }
        public bool SaveRMAHeader(RMAHeader newItem, Int16 branchID)
        {

            return rmaheaderDAL.Save(newItem, branchID);

        }

        public bool DeleteRMAHeader(RMAHeader item)
        {
            return rmaheaderDAL.Delete(item);
        }

        public RMAHeader GetRMAHeader(RMAHeader item)
        {
            return (RMAHeader)rmaheaderDAL.GetItem<RMAHeader>(item);
        }

        public RMAHeader GetRMAHeaderByDocumentNo(RMAHeader item)
        {
            return (RMAHeader)rmaheaderDAL.GetItemByDocumentNo<RMAHeader>(item);
        }
    }
}
