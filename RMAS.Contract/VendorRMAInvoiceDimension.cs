using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace EZY.RMAS.Contract
{
	public class VendorRMAInvoiceDimension: IContract
	{
		// Constructor 
		public VendorRMAInvoiceDimension() { }

		// Public Members 

		public Int16  BranchID { get; set; }

		public string InvoiceNo { get; set; }

		public string CartonNo { get; set; }

		public string Dimension { get; set; }

		public decimal Weight { get; set; }


	}
}



