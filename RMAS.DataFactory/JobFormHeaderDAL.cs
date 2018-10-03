using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
    public class JobFormHeaderDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public JobFormHeaderDAL()
        {

            db = DatabaseFactory.CreateDatabase("RMAS");

        }

        #region IDataFactory Members

        public List<JobFormHeader> GetList(short branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTJOBFORMHEADER, MapBuilder<JobFormHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.JobFormDetails)
                                           .Build(), branchID).ToList();
        }


        

        public List<JobFormHeader> SearchList(short branchID, string vesselName, string consigneeName, DateTime dateFrom, DateTime dateTo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.JOBFORMHEADERAUTOCOMPLETESEARCH, MapBuilder<JobFormHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.JobFormDetails)
                                           .Build(), branchID, consigneeName, vesselName, dateFrom, dateTo).ToList();
        }

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var jobformheader = (JobFormHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                if (jobformheader.JobID <=0)
                {
                    jobformheader.ServiceStatus = 2000;
                }


                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEJOBFORMHEADER);

                db.AddInParameter(savecommand, "JobID", System.Data.DbType.Int64, jobformheader.JobID);
                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, jobformheader.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, jobformheader.DocumentNo);
                db.AddInParameter(savecommand, "CustomerCode", System.Data.DbType.String, jobformheader.CustomerCode);
                db.AddInParameter(savecommand, "Address1", System.Data.DbType.String, jobformheader.Address1);
                db.AddInParameter(savecommand, "EmailID", System.Data.DbType.String, jobformheader.EmailID);
                db.AddInParameter(savecommand, "PhoneNumber", System.Data.DbType.String, jobformheader.PhoneNumber);
                db.AddInParameter(savecommand, "PurchaseDate", System.Data.DbType.DateTime, jobformheader.PurchaseDate);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, jobformheader.InvoiceNo);
                db.AddInParameter(savecommand, "DateReceived", System.Data.DbType.DateTime, jobformheader.DateReceived);
                db.AddInParameter(savecommand, "DateReturn", System.Data.DbType.DateTime, jobformheader.DateReturn);
                db.AddInParameter(savecommand, "AssignedEngineer", System.Data.DbType.String, jobformheader.AssignedEngineer);
                db.AddInParameter(savecommand, "ProductCategory", System.Data.DbType.Int16, jobformheader.ProductCategory);
                db.AddInParameter(savecommand, "ServiceType", System.Data.DbType.Int16, jobformheader.ServiceType);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, jobformheader.CreatedBy);
                db.AddInParameter(savecommand, "ServiceStatus", System.Data.DbType.String, jobformheader.ServiceStatus);
                db.AddInParameter(savecommand, "SerialNo", System.Data.DbType.String, jobformheader.SerialNo);
                db.AddOutParameter(savecommand, "NewJobID", System.Data.DbType.Int64,0);




                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    jobformheader.JobID = Convert.ToInt64(savecommand.Parameters["@NewJobID"].Value);
                    short itr = 1;
                    jobformheader.JobFormDetails.ForEach(dt =>
                    {
                        dt.JobID= jobformheader.JobID;
                        dt.ItemID= itr++;
                    });


                    JobFormDetailDAL jobformdetailDAL = new JobFormDetailDAL();

                    result = jobformdetailDAL.DeleteAll( jobformheader.JobID  ,transaction) == true ? 1 : 0;

                    result = jobformdetailDAL.SaveList(jobformheader.JobFormDetails, transaction) == true ? 1 : 0;

                }

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var jobformheader = (JobFormHeader)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEJOBFORMHEADER);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, jobformheader.BranchID);
                db.AddInParameter(deleteCommand, "JobID", System.Data.DbType.Int64, jobformheader.JobID);

                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return result;
        }

        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((JobFormHeader)lookupItem);

            var jobformheaderItem = db.ExecuteSprocAccessor(DBRoutine.SELECTJOBFORMHEADER,
                                                    MapBuilder<JobFormHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.JobFormDetails)
                                           .Build(),
                                                    item.JobID,item.BranchID ).FirstOrDefault();

            if (jobformheaderItem == null) return null;


            jobformheaderItem.JobFormDetails = new JobFormDetailDAL().GetList(jobformheaderItem.JobID);



            return jobformheaderItem;
        }

        #endregion

        public IContract GetItem<T>(short branchID, string serialNo) where T : IContract
        {
             

            var jobformheaderItem = db.ExecuteSprocAccessor(DBRoutine.GETPRODUCTDETAILSBYSERIALNO,
                                                    MapBuilder<JobFormHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.JobFormDetails)
                                           .Build(),branchID,serialNo).FirstOrDefault();

            if (jobformheaderItem == null) return null;


            jobformheaderItem.JobFormDetails = new List<JobFormDetail>();



            return jobformheaderItem;
        }

        public bool DeleteJobID(short BranchId,short JobID)
        {
            var result = false;
            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEJOBFORMHEADER);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, BranchId);
                db.AddInParameter(deleteCommand, "JobID", System.Data.DbType.Int64, JobID);

                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return result;
        }

    }
}


