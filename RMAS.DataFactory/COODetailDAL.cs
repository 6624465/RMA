using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
    public class COODetailDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public COODetailDAL()
        {

            db = DatabaseFactory.CreateDatabase("RMAS");

        }

        #region IDataFactory Members

        public List<COODetail> GetList(short branchID, string documentNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTCOODETAIL, MapBuilder<COODetail>.BuildAllProperties(),branchID, documentNo).ToList();
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

        public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        {
            currentTransaction = parentTransaction;
            return Save(item);

        }

        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var coodetail = (COODetail)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECOODETAIL);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, coodetail.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, coodetail.DocumentNo);
                db.AddInParameter(savecommand, "ItemNo", System.Data.DbType.Int16, coodetail.ItemNo);
                db.AddInParameter(savecommand, "ModelNo", System.Data.DbType.String, coodetail.ModelNo);
                db.AddInParameter(savecommand, "Description", System.Data.DbType.String, coodetail.Description);
                db.AddInParameter(savecommand, "Qty", System.Data.DbType.Int16, coodetail.Qty);
                db.AddInParameter(savecommand, "Origin", System.Data.DbType.String, coodetail.Origin);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception)
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
            var coodetail = (COODetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETECOODETAIL);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, coodetail.BranchID);
                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, coodetail.DocumentNo);

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
            var item = ((COODetail)lookupItem);

            var Item = db.ExecuteSprocAccessor(DBRoutine.SELECTCOODETAIL,
                                                    MapBuilder<COODetail>.BuildAllProperties(),
                                                    ((COODetail)lookupItem).BranchID, ((COODetail)lookupItem).DocumentNo, ((COODetail)lookupItem).ModelNo).FirstOrDefault();

            if (Item == null) return null;

            return Item;
        }

        #endregion

    }
}


