using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
	public class Declarant : IContract
	{
		public Declarant() { }

		public short BranchID { get; set; }

		public string DeclarantName { get; set; }

		public string Designation { get; set; }

		public bool IsActive { get; set; }

	}
}
