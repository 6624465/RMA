﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using System.Web;
using EZY.RMAS.Contract;
using System.Web.Mvc;

namespace EZY.RMAS.Contract
{
	public class Company: IContract
	{
		// Constructor 
		public Company() { }

		// Public Members 

		[DisplayName("CompanyCode")] 
		public string  CompanyCode { get; set; }

		[DisplayName("CompanyName")] 
		public string CompanyName { get; set; }

		[DisplayName("RegNo")] 
		public string  RegNo { get; set; }

		[DisplayName("Logo")] 
		public object Logo { get; set; }

		[DisplayName("IsActive")] 
		public bool  IsActive { get; set; }

		[DisplayName("AddressID")] 
		public string  AddressID { get; set; }

		[DisplayName("CreatedBy")] 
		public string  CreatedBy { get; set; }

		[DisplayName("CreatedOn")] 
		public DateTime  CreatedOn { get; set; }

		[DisplayName("ModifiedBy")] 
		public string  ModifiedBy { get; set; }

		[DisplayName("ModifiedOn")] 
		public DateTime  ModifiedOn { get; set; }

        public Address CompanyAddress { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }

        public List<Branch> BranchList { get; set; }
	}
}



