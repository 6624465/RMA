﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class InvoiceHeaderBO
    {
        private InvoiceHeaderDAL invoiceheaderDAL;
        public InvoiceHeaderBO()
        {

            invoiceheaderDAL = new InvoiceHeaderDAL();
        }
        public List<InvoiceHeader> GetList(short branchID)
        {
            return invoiceheaderDAL.GetList(branchID);
        }

        public List<InvoiceHeaderData> GetVendorInvoiceDataTableList(DataTableObject Obj, Int16 invoiceType)
        {
            return invoiceheaderDAL.GetVendorInvoiceDataTableList(Obj, invoiceType);
        }


        public bool SaveInvoiceHeader(InvoiceHeader newItem)
        {

            return invoiceheaderDAL.Save(newItem);

        }
        public List<string> CheckSerailNumberForVendor(Int16 branchID,int ProductCategory,string ProductCode, List<string> serialNumbers)
        {
            return invoiceheaderDAL.CheckSerailNumberForVendor(branchID, ProductCategory, ProductCode, serialNumbers);
        }
        public bool DeleteInvoiceHeader(InvoiceHeader item)
        {
            return invoiceheaderDAL.Delete(item);
        }
        public bool DeleteSerialNo(string serialNo)
        {
            return invoiceheaderDAL.DeleteSerialNo(serialNo);
        }
        public InvoiceHeader GetInvoiceHeader(InvoiceHeader item)
        {
            return (InvoiceHeader)invoiceheaderDAL.GetItem<InvoiceHeader>(item);
        }
        public System.Data.IDataReader SerialNoInquiry(string modelNo, string serialNo)
        {
            return invoiceheaderDAL.SerialNoInquiry(modelNo, serialNo);
        }

        public List<string> CheckSerailNumber(Int16 branchID,List<string> serialNumbers)
        {
            return invoiceheaderDAL.CheckSerailNumber(branchID,serialNumbers);
        }
    }
}
