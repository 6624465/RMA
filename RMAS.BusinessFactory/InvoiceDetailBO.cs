using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class InvoiceDetailBO
    {
        private InvoiceDetailDAL vendorinvoicedetailDAL;
        public InvoiceDetailBO()
        {

            vendorinvoicedetailDAL = new InvoiceDetailDAL();
        }

        public List<InvoiceDetail> GetList()
        {
            return vendorinvoicedetailDAL.GetList();
        }


        public bool SaveVendorInvoiceDetail(InvoiceDetail newItem)
        {

            return vendorinvoicedetailDAL.Save(newItem);

        }

        public bool DeleteVendorInvoiceDetail(InvoiceDetail item)
        {
            return vendorinvoicedetailDAL.Delete(item);
        }

        public InvoiceDetail GetVendorInvoiceDetail(InvoiceDetail item)
        {
            return (InvoiceDetail)vendorinvoicedetailDAL.GetItem<InvoiceDetail>(item);
        }

        public RMAInwardDTO GetVendorInvoiceDetailBySerialNo(string serialNo,string CountryCode, Int16 branchID, Int16 invoiceType)
        {
            return vendorinvoicedetailDAL.GetVendorInvoiceDetailBySerialNo(serialNo, CountryCode, branchID, invoiceType);
        }
        public List<VendorRMAInwardDTO> GetRMADetailByRMANo(Int16 branchID, string DocumentNo)
        {
            return vendorinvoicedetailDAL.GetRMADetailByRMANo(branchID, DocumentNo);
        }
        public MaterialsEnquiryDetails GetDetailBySerialNo(string serialNo, Int16 branchID)
        {
            return vendorinvoicedetailDAL.GetDetailBySerialNo(serialNo, branchID);
        }
    }
}
