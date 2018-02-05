using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System.Collections.Generic;

namespace EZY.RMAS.BusinessFactory
{
    public class BranchBO
    {
        private BranchDAL branchDAL;
        public BranchBO()
        {

            branchDAL = new BranchDAL();
        }

        public List<Branch> GetList()
        {
            return branchDAL.GetList();
        }

        public List<Branch> GetListByCompanyCode(string CompanyCode)
        {
            return branchDAL.GetListByCompanyCode(CompanyCode);
        }


        public bool SaveBranch(Branch newItem)
        {

            return branchDAL.Save(newItem);

        }

        public bool DeleteBranch(Branch item)
        {
            return branchDAL.Delete(item);
        }

        public Branch GetBranch(Branch item)
        {
            return (Branch)branchDAL.GetItem<Branch>(item);
        }

    }
}
