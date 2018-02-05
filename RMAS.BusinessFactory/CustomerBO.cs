using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class CustomerBO
    {
        private CustomerDAL customerDAL;
        public CustomerBO()
        {

            customerDAL = new CustomerDAL();
        }

        public List<Customer> GetList(short branchID)
        {
            return customerDAL.GetList(branchID);
        }

        public List<Customer> GetCustomerTableDataList(DataTableObject Obj)
        {
            return customerDAL.GetCustomerTableDataList(Obj);
        }


        public bool SaveCustomer(Customer newItem)
        {

            return customerDAL.Save(newItem);

        }

        public bool DeleteCustomer(Customer item)
        {
            return customerDAL.Delete(item);
        }

        public Customer GetCustomer(Customer item)
        {
            return (Customer)customerDAL.GetItem<Customer>(item);
        }

    }
}
