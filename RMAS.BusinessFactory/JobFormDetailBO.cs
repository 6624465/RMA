
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System.Collections.Generic;

namespace EZY.RMAS.BusinessFactory
{
    public class JobFormDetailBO
    {
        private JobFormDetailDAL jobformdetailDAL;
        public JobFormDetailBO()
        {

            jobformdetailDAL = new JobFormDetailDAL();
        }

        public List<JobFormDetail> GetList(long jobID)
        {
            return jobformdetailDAL.GetList(jobID);
        }


        public bool SaveCOODetail(JobFormDetail newItem)
        {

            return jobformdetailDAL.Save(newItem);

        }

        public bool DeleteCOODetail(JobFormDetail item)
        {
            return jobformdetailDAL.Delete(item);
        }

        public JobFormDetail GetCOODetail(JobFormDetail item)
        {
            return (JobFormDetail)jobformdetailDAL.GetItem<JobFormDetail>(item);
        }

    }
}

