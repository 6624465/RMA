using System.ComponentModel;

namespace EZY.RMAS.Contract
{
    public class RoleRights : IContract
    {
        // Constructor 
        public RoleRights() { }

        // Public Members 

        [DisplayName("RoleCode")]
        public string RoleCode { get; set; }

        [DisplayName("SecurableItem")]
        public string SecurableItem { get; set; }


    }
}
