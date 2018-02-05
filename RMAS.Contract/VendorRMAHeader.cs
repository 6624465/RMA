using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
  public  class VendorRMAHeader : IContract
    {
        // Constructor 
        public VendorRMAHeader() {
        }

        // Public Members 

        public Int16 BranchID { get; set; }

        public string DoNo { get; set; }

        public string DocumentNo { get; set; }

        //New add start
        public string CompanyName { get; set; }

        public string CustomerAddress { get; set; }

        //New add end

        public string VendorInvoiceNo { get; set; }

        public string VendorName { get; set; }

        public string SerialNo { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }

        public bool IsValid { get; set; }

        public bool RMAIsValid { get; set; }

        public DateTime? VendorWarrantyExpiryDate { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<VendorRMADetails> VendorRmaDetails { get; set; }
        public List<VendorRMAHeaderList> VendorRMAHeaderList { get; set; }
        public bool IsCompleted { get; set; }
    }
    public class VendorRMAHeaderList :IContract
    {
        public VendorRMAHeaderList() { }

        public Int16 BranchID { get; set; }
        public string DoNo { get; set; }

        public string DocumentNo { get; set; }

        public string VendorInvoiceNo { get; set; }

        public string VendorName { get; set; }

        public bool Status { get; set; }

        public List<string> SerialNo { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }

        public bool IsValid { get; set; }

        public bool RMAIsValid { get; set; }

        public DateTime? VendorWarrantyExpiryDate { get; set; }

        public bool IsCompleted { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
