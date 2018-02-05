using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace EZY.RMAS.Contract
{
	public class VendorRMAInvoiceHeader : IContract
    {
		// Constructor 
		public VendorRMAInvoiceHeader() { }

		// Public Members 

		public Int16  BranchID { get; set; }

		public string InvoiceNo { get; set; }

		public DateTime  InvoiceDate { get; set; }

		public string  ReferenceNo { get; set; }

		public Int64  TotalQty { get; set; }

		public decimal TotalAmount { get; set; }

		public decimal TotalWeight { get; set; }

		public string PaymentTerm { get; set; }

		public string LoadingPort { get; set; }

		public string DischargePort { get; set; }

		public string ShipmentTerms { get; set; }

		public string OriginCountry { get; set; }

		public string AWB { get; set; }

		public string Remarks { get; set; }

		public string Dimension { get; set; }

		public string  CreatedBy { get; set; }

		public DateTime  CreatedOn { get; set; }

		public string  ModifiedBy { get; set; }

		public DateTime  ModifiedOn { get; set; }

        public string CompanyName { get; set; }

        public string CustomerAddress { get; set; }

        public List<VendorRMAInvoiceDetail> VendorRMAInvoiceDetails { get; set; }

        public List<VendorRMAInvoiceDimension> VendorRMAInvoiceDimensions { get; set; }
        public List<VendorRMAHeader> VendorRMAHeader { get; set; }
    }
}