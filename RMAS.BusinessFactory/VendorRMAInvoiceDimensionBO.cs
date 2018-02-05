using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class VendorRMAInvoiceDimensionBO
    {


        private VendorRMAInvoiceDimensionDAL vendorrmainvoicedimensionDAL;
        public VendorRMAInvoiceDimensionBO()
        {

            vendorrmainvoicedimensionDAL = new VendorRMAInvoiceDimensionDAL();
        }

        public List<VendorRMAInvoiceDimension> GetList(short branchID,string invoiceNo)
        {
            return vendorrmainvoicedimensionDAL.GetList(branchID, invoiceNo);
        }



        public bool Save(VendorRMAInvoiceDimension newItem)
        {

            return vendorrmainvoicedimensionDAL.Save(newItem);

        }



        public bool Delete(VendorRMAInvoiceDimension item)
        {
            return vendorrmainvoicedimensionDAL.Delete(item);
        }

        public VendorRMAInvoiceDimension GetItem(VendorRMAInvoiceDimension item)
        {
            return (VendorRMAInvoiceDimension)vendorrmainvoicedimensionDAL.GetItem<VendorRMAInvoiceDimension>(item);
        }

    }
}
