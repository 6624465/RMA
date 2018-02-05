using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class AddressBO
    {
        private AddressDAL addressDAL;
        public AddressBO()
        {

            addressDAL = new AddressDAL();
        }

        public List<Address> GetList()
        {
            return addressDAL.GetList();
        }


        public bool SaveAddress(Address newItem)
        {

            return addressDAL.Save(newItem);

        }

        public bool DeleteAddress(Address item)
        {
            return addressDAL.Delete(item);
        }

        public Address GetAddress(Address item)
        {
            return (Address)addressDAL.GetItem<Address>(item);
        }

    }
}
