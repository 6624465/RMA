using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System.Collections.Generic;

namespace EZY.RMAS.BusinessFactory
{
    public class JobFormHeaderBO
    {
        private JobFormHeaderDAL jobformheaderDAL;
        public JobFormHeaderBO()
        {

            jobformheaderDAL = new JobFormHeaderDAL();
        }

        public List<JobFormHeader> GetList(short branchID)
        {
            return jobformheaderDAL.GetList(branchID);
        }


        public bool SaveJobFormHeader(JobFormHeader newItem)
        {

            return jobformheaderDAL.Save(newItem);

        }

        public bool DeleteJobFormHeader(JobFormHeader item)
        {
            return jobformheaderDAL.Delete(item);
        }

        public JobFormHeader GetJobFormHeader(JobFormHeader item)
        {
            return (JobFormHeader)jobformheaderDAL.GetItem<JobFormHeader>(item);
        }


        public JobFormHeader GetJobFormHeader(short branchID, string serialNo)
        {
            return (JobFormHeader)jobformheaderDAL.GetItem<JobFormHeader>(branchID, serialNo);
        }
    }
}

