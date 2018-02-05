using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class EduProductBO
    {
        private EduProductDAL eduProductDAL;
        public EduProductBO()
        {
            eduProductDAL = new EduProductDAL();
        }

        public List<EduProduct> GetList()
        {
            return eduProductDAL.GetList();
        }

        public EduProduct GetEduProduct(EduProduct item)
        {
            return (EduProduct)eduProductDAL.GetItem<EduProduct>(item);
        }

        public bool SaveEduProduct(EduProduct newItem)
        {
            return eduProductDAL.Save(newItem);
        }
    }
}
