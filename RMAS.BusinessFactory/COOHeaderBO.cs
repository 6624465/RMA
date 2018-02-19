using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System.Collections.Generic;
using System;

namespace EZY.RMAS.BusinessFactory
{
    public class COOHeaderBO
    {
        private COOHeaderDAL cooheaderDAL;
        public COOHeaderBO()
        {

            cooheaderDAL = new COOHeaderDAL();
        }

        public List<COOHeader> GetList(short branchID)
        {
            return cooheaderDAL.GetList(branchID);
        }
		public List<COOHeader> SearchList(short branchID, string vesselName, string consigneeName, DateTime dateFrom, DateTime dateTo)
		{
			return cooheaderDAL.SearchList(branchID, vesselName, consigneeName, dateFrom, dateTo);

		}

		public bool SaveCOOHeader(COOHeader newItem)
        {

            return cooheaderDAL.Save(newItem);

        }

        public bool DeleteCOOHeader(COOHeader item)
        {
            return cooheaderDAL.Delete(item);
        }

        public COOHeader GetCOOHeader(COOHeader item)
        {
            return (COOHeader)cooheaderDAL.GetItem<COOHeader>(item);
        }

    }
}
