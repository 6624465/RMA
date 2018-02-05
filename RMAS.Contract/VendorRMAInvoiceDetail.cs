using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace EZY.RMAS.Contract
{
	public class VendorRMAInvoiceDetail: IContract
	{
		// Constructor 
		public VendorRMAInvoiceDetail() { }

		// Public Members 

		public Int16  BranchID { get; set; }

		public string InvoiceNo { get; set; }

		public string  RMANumber { get; set; }

		public string ModelNo { get; set; }

        public string Description { get; set; }

		public Int64  Qty { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TotalPrice { get; set; }

		public string  CreatedBy { get; set; }

		public DateTime  CreatedOn { get; set; }

		public string  ModifiedBy { get; set; }

		public DateTime  ModifiedOn { get; set; }


	}
}



