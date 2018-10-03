using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EZY.RMAS.Contract
{
    public class JobFormHeader : IContract
    {// Constructor 
        public JobFormHeader() { }

		// Public Members 

		public Int64 JobID { get; set; }

        public Int16 BranchID { get; set; }

        public string DocumentNo { get; set; }

        public string CustomerCode { get; set; }

        public string Address1 { get; set; }

        public string EmailID { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime DateReceived { get; set; }

        public DateTime DateReturn { get; set; }

        public string AssignedEngineer { get; set; }

        public Int16 ProductCategory { get; set; }

        public Int16 ServiceType { get; set; }

        public Int16 ServiceStatus { get; set; }

        public string ServiceStatusDescription { get; set; }

        public string SerialNo { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<JobFormDetail> JobFormDetails { get; set; }

        public string CustomerName { get; set; }
        public IEnumerable<SelectListItem> EngineerList { get; set; }

        public IEnumerable<SelectListItem> ProductCategoryList { get; set; }

        public IEnumerable<SelectListItem> ServiceTypeList { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }

    }
}
