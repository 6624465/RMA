﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class UsersBO
    {
        private UsersDAL usersDAL;
        public UsersBO()
        {

            usersDAL = new UsersDAL();
        }

        public List<Users> GetList()
        {
            return usersDAL.GetList();
        }


        public bool SaveUsers(Users newItem, string CompanyId)
        {

            return usersDAL.Save(newItem, CompanyId);

        }

        public bool DeleteUsers(Users item)
        {
            return usersDAL.Delete(item);
        }

        public Users GetUsers(Users item)
        {
            return (Users)usersDAL.GetItem<Users>(item);
        }




    }
}
