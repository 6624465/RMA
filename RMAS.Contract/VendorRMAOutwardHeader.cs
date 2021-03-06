﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
    public class VendorRMAOutwardHeader:IContract
    {
        public VendorRMAOutwardHeader() { }

        public Int16 BranchID { get; set; }

        public string DocumentNo { get; set; }

        public DateTime DocumentDate { get; set; }

        public string ReferenceNo { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<VendorRMAOutwardDetail> VendorRmaDetails { get; set; }
    }
}
