
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System.Collections.Generic;

namespace EZY.RMAS.BusinessFactory
{
    public class COODetailBO
    {
        private COODetailDAL coodetailDAL;
        public COODetailBO()
        {

            coodetailDAL = new COODetailDAL();
        }

        public List<COODetail> GetList(short branchID, string documentNo)
        {
            return coodetailDAL.GetList(branchID, documentNo);
        }


        public bool SaveCOODetail(COODetail newItem)
        {

            return coodetailDAL.Save(newItem);

        }

        public bool DeleteCOODetail(COODetail item)
        {
            return coodetailDAL.Delete(item);
        }

        public COODetail GetCOODetail(COODetail item)
        {
            return (COODetail)coodetailDAL.GetItem<COODetail>(item);
        }

    }
}

