﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class UserBranchBO
    {
        private UserBranchDAL userBranchDAL;
        public UserBranchBO()
        {
            userBranchDAL = new UserBranchDAL();
        }

        public List<UserBranch> GetList(string UserID)
        {
            return userBranchDAL.GetList(UserID);
        }

        public List<UserBranchSelectedList> GetUserBranchSelectedList(string UserID, string CompanyCode)
        {
            return userBranchDAL.GetUserBranchSelectedList(UserID, CompanyCode);
        }
    }
}
