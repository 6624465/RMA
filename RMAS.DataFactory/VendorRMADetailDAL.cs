using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
       public  class VendorRMADetailDAL : IDataFactory
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public VendorRMADetailDAL()
        {
            db = DatabaseFactory.CreateDatabase("RMAS");
        }

        #region IDataFactory Members
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

            var rmadetail = (VendorRMADetails)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMADETAIL);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.String, rmadetail.BranchID);
                db.AddInParameter(savecommand, "DoNo", System.Data.DbType.String, rmadetail.DoNo);
                db.AddInParameter(savecommand, "SerialNo", System.Data.DbType.String, rmadetail.SerialNo);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (currentTransaction == null)
                    transaction.Commit();

            }
            catch (Exception ex)
            {
                if (currentTransaction == null)
                    transaction.Rollback();

                throw ex;
            }

            return (result > 0 ? true : false);

        }

        public bool Delete<T>(T item) where T : IContract
        {
            var result = false;
            var vendorrmadetails = (VendorRMADetails)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICEDETAIL);

                db.AddInParameter(deleteCommand, "DoNo", System.Data.DbType.String,vendorrmadetails.DoNo);

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

            //var item = (VendorRMADetails)lookupItem;

            //var vendorinvoicedetailItem = db.ExecuteSprocAccessor(DBRoutine.select,
            //                                        MapBuilder<VendorRMADetails>.BuildAllProperties(),
            //                                        item.DocumentNo).FirstOrDefault();
            //return vendorinvoicedetailItem;

            throw new NotImplementedException();
        }
        #endregion
    }
}
