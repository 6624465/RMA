﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
     
    public class RMAOutwardHeaderBO
    {
        private RMAOutwardHeaderDAL rmaoutwardheaderDAL;
        public RMAOutwardHeaderBO()
        {
            rmaoutwardheaderDAL = new RMAOutwardHeaderDAL();
        }

        public List<RMAOutwardHeader> GetList(short branchID)
        {
            return rmaoutwardheaderDAL.GetList(branchID);
        }


        public bool SaveRMAOutwardHeader(RMAOutwardHeader newItem)
        {

            return rmaoutwardheaderDAL.Save(newItem);

        }
        


        public bool DeleteRMAOutwardHeader(RMAOutwardHeader item)
        {
            return rmaoutwardheaderDAL.Delete(item);
        }

        public RMAOutwardHeader GetRMAOutwardHeader(RMAOutwardHeader item)
        {
            return (RMAOutwardHeader)rmaoutwardheaderDAL.GetItem<RMAOutwardHeader>(item);
        }

    }
}
