using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;


namespace EZY.RMAS.BusinessFactory
{
    public class VendorRMAInvoiceDetailBO
    {


        private VendorRMAInvoiceDetailDAL vendorrmainvoicedetailDAL;
        public VendorRMAInvoiceDetailBO()
        {

            vendorrmainvoicedetailDAL = new VendorRMAInvoiceDetailDAL();
        }

        public List<VendorRMAInvoiceDetail> GetList(short branchID,string invoiceNo)
        {
            return vendorrmainvoicedetailDAL.GetList(branchID, invoiceNo);
        }
        public bool Save(VendorRMAInvoiceDetail newItem)
        {

            return vendorrmainvoicedetailDAL.Save(newItem);

        }

        public bool Delete(VendorRMAInvoiceDetail item)
        {
            return vendorrmainvoicedetailDAL.Delete(item);
        }

        public VendorRMAInvoiceDetail GetItem(VendorRMAInvoiceDetail item)
        {
            return (VendorRMAInvoiceDetail)vendorrmainvoicedetailDAL.GetItem<VendorRMAInvoiceDetail>(item);
        }

    }
}
