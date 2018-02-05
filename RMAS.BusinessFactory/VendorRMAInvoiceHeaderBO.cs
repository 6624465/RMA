using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class VendorRMAInvoiceHeaderBO
    {


        private VendorRMAInvoiceHeaderDAL vendorrmainvoiceheaderDAL;
        public VendorRMAInvoiceHeaderBO()
        {

            vendorrmainvoiceheaderDAL = new VendorRMAInvoiceHeaderDAL();
        }

        public List<VendorRMAInvoiceHeader> GetList(short branchID)
        {
            return vendorrmainvoiceheaderDAL.GetList(branchID);
        }



        public bool Save(VendorRMAInvoiceHeader newItem)
        {
            return vendorrmainvoiceheaderDAL.Save(newItem);

        }

        public bool Delete(short branchID, string invoiceNo)
        {
            return vendorrmainvoiceheaderDAL.Delete(branchID, invoiceNo);
        }

        public VendorRMAInvoiceHeader GetItem(VendorRMAInvoiceHeader item)
        {
            return (VendorRMAInvoiceHeader)vendorrmainvoiceheaderDAL.GetItem<VendorRMAInvoiceHeader>(item);
        }

    }
}
