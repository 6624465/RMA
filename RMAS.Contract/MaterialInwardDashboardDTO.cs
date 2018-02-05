using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.Contract
{
    public class MaterialInwardDashboardDTO : IContract
    {
        public MaterialInwardDashboardDTO()
        {

        }
        public int ProductCode { get;set; }
        public Int16 Branch { get; set; }
        public Int16 FromMonth { get; set; }
        public Int16 ToMonth { get; set; }
        public Int16 Year { get; set; }
        public List<MaterialInwardProductQty> MaterialInwardProductQty { get; set; }

        public List<MaterialInwardProductCost> MaterialInwardProductCost { get; set; }

        public List<MaterialInwardInvoices> MaterialInwardInvoices { get; set; }
    }
}
