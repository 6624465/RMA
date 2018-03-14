using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
    public class COOHeader : IContract
    {
        // Constructor 
        public COOHeader() { }

        // Public Members 

        public Int16 BranchID { get; set; }

        public string DocumentNo { get; set; }

        public string ExporterName { get; set; }

        public string ExporterAddress { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeAddress { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DestinationPort { get; set; }

        public string VesselName { get; set; }

        public string DestinationCountry { get; set; }

        public string DeclarantName { get; set; }
        public string Designation { get; set; }
        public DateTime DeclarationDate { get; set; }


        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public bool IsInvoiceConfirm { get; set; }

        public bool IsCertified { get; set; }

        public string Country1 { get; set; }
        public string Country2 { get; set; }
        public string Country3 { get; set; }
        public string Country4 { get; set; }
        public string Country5 { get; set; }
        public string Country6 { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<COODetail> COODetails { get; set; }
    }
}
