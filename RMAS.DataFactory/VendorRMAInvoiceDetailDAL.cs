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
    public class VendorRMAInvoiceDetailDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public VendorRMAInvoiceDetailDAL()
        {

            db = DatabaseFactory.CreateDatabase("RMAS");

        }

        #region IDataFactory Members

        public List<VendorRMAInvoiceDetail> GetList(short branchID, string invoiceNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTVENDORRMAINVOICEDETAIL, MapBuilder<VendorRMAInvoiceDetail>.MapAllProperties().DoNotMap(x=>x.Description).Build(),branchID,invoiceNo).ToList();
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

            var vendorrmainvoicedetail = (VendorRMAInvoiceDetail)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMAINVOICEDETAIL);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, vendorrmainvoicedetail.BranchID);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, vendorrmainvoicedetail.InvoiceNo);
                db.AddInParameter(savecommand, "RMANumber", System.Data.DbType.String, vendorrmainvoicedetail.RMANumber);
                db.AddInParameter(savecommand, "ModelNo", System.Data.DbType.String, vendorrmainvoicedetail.ModelNo);
                db.AddInParameter(savecommand, "Qty", System.Data.DbType.Int64, vendorrmainvoicedetail.Qty);
                db.AddInParameter(savecommand, "UnitPrice", System.Data.DbType.Decimal, vendorrmainvoicedetail.UnitPrice);
                db.AddInParameter(savecommand, "TotalPrice", System.Data.DbType.Decimal, vendorrmainvoicedetail.TotalPrice);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, vendorrmainvoicedetail.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, vendorrmainvoicedetail.ModifiedBy);

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
            var vendorrmainvoicedetail = (VendorRMAInvoiceDetail)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEINVOICEDETAIL);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, vendorrmainvoicedetail.BranchID);
                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, vendorrmainvoicedetail.InvoiceNo);
                db.AddInParameter(deleteCommand, "ModelNo", System.Data.DbType.String, vendorrmainvoicedetail.ModelNo);
                db.AddInParameter(deleteCommand, "RMANumber", System.Data.DbType.String, vendorrmainvoicedetail.RMANumber);
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

            var item = (VendorRMAInvoiceDetail)lookupItem;

            var vendorrmainvoicedetailItem = db.ExecuteSprocAccessor(DBRoutine.SELECTVENDORRMAINVOICEDETAIL,
                                                    MapBuilder<VendorRMAInvoiceDetail>.BuildAllProperties(),
                                                    item.BranchID,
                                                    item.InvoiceNo
                                                    ).FirstOrDefault();
            return vendorrmainvoicedetailItem;
        }



        #endregion

    }
}




