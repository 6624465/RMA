using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
    public class JobFormDetailDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public JobFormDetailDAL()
        {

            db = DatabaseFactory.CreateDatabase("RMAS");

        }

        #region IDataFactory Members

        public List<JobFormDetail> GetList(Int64 jobID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTJOBFORMDETAIL, MapBuilder<JobFormDetail>.MapAllProperties()
                                           .Build(), jobID).ToList();
        }
        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }
        public bool SaveList<T>(List<T> items, DbTransaction parentTransaction) where T : IContract
        {
            var result = true;

            if (items.Count == 0)
                result = true;

            foreach (var item in items)
            {
                result = Save(item, parentTransaction);
                if (result == false) break;
            }


            return result;

        }
         
        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var jobformdetail = (JobFormDetail)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.SAVEJOBFORMDETAIL);

                db.AddInParameter(deleteCommand, "ItemID", System.Data.DbType.Int16, jobformdetail.ItemID);
                db.AddInParameter(deleteCommand, "JobID", System.Data.DbType.Int64, jobformdetail.JobID);
                db.AddInParameter(deleteCommand, "ProductDescription", System.Data.DbType.String, jobformdetail.ProductDescription);
                db.AddInParameter(deleteCommand, "ModelNo", System.Data.DbType.String, jobformdetail.ModelNo);
                db.AddInParameter(deleteCommand, "SerialNo", System.Data.DbType.String, jobformdetail.SerialNo);
                db.AddInParameter(deleteCommand, "Remarks", System.Data.DbType.String, jobformdetail.Remarks);
                db.AddInParameter(deleteCommand, "FaultDescription", System.Data.DbType.String, jobformdetail.FaultDescription);

                result = db.ExecuteNonQuery(deleteCommand, transaction);


                 

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
            var jobformdetail = (JobFormDetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEJOBFORMDETAIL);

                db.AddInParameter(deleteCommand, "ItemID", System.Data.DbType.Int16, jobformdetail.ItemID);
                db.AddInParameter(deleteCommand, "JobID", System.Data.DbType.Int64, jobformdetail.JobID);

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


        public bool DeleteAll(Int64 jobID, DbTransaction parentTransaction)
        {
            var result = false;

            currentTransaction = parentTransaction;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEJOBFORMDETAILALL);

                db.AddInParameter(deleteCommand, "JobID", System.Data.DbType.Int64, jobID);

                result = Convert.ToBoolean(db.ExecuteNonQuery(deleteCommand, transaction));

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();
                throw ex;
            }

            return result;
        }


        public IContract GetItem<T>(IContract lookupItem) where T : IContract
        {
            var item = ((JobFormDetail)lookupItem);

            var JobFormDetailItem = db.ExecuteSprocAccessor(DBRoutine.SELECTJOBFORMDETAIL,
                                                    MapBuilder<JobFormDetail>.MapAllProperties()
                                                    .Build(),
                                                    ((JobFormDetail)lookupItem).ItemID, item.JobID).FirstOrDefault();

             

            return JobFormDetailItem;
        }

        #endregion

    }
}


