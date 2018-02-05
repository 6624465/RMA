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
    public class VendorRMAInvoiceDimensionDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public VendorRMAInvoiceDimensionDAL()
        {

            db = DatabaseFactory.CreateDatabase("RMAS");

        }

        #region IDataFactory Members

        public List<VendorRMAInvoiceDimension> GetList(short branchID, string invoiceNo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTVENDORRMAINVOICEDIMENSION, MapBuilder<VendorRMAInvoiceDimension>.BuildAllProperties(),branchID,invoiceNo).ToList();
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

            var vendorrmainvoicedimension = (VendorRMAInvoiceDimension)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMAINVOICEDIMENSION);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, vendorrmainvoicedimension.BranchID);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, vendorrmainvoicedimension.InvoiceNo);
                db.AddInParameter(savecommand, "CartonNo", System.Data.DbType.String, vendorrmainvoicedimension.CartonNo==null? " ": vendorrmainvoicedimension.CartonNo);
                db.AddInParameter(savecommand, "Dimension", System.Data.DbType.String, vendorrmainvoicedimension.Dimension==null?" ": vendorrmainvoicedimension.Dimension);
                db.AddInParameter(savecommand, "Weight", System.Data.DbType.Decimal, vendorrmainvoicedimension.Weight);

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
            var vendorrmainvoicedimension = (VendorRMAInvoiceDimension)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEVENDORRMAINVOICEDIMENSION);


                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, vendorrmainvoicedimension.BranchID);
                db.AddInParameter(deleteCommand, "CartonNo", System.Data.DbType.String, vendorrmainvoicedimension.CartonNo);
                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, vendorrmainvoicedimension.InvoiceNo);
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

            var item = (VendorRMAInvoiceDimension)lookupItem;

            var vendorrmainvoicedetailItem = db.ExecuteSprocAccessor(DBRoutine.SELECTVENDORRMAINVOICEDIMENSION,
                                                    MapBuilder<VendorRMAInvoiceDimension>.BuildAllProperties(),
                                                    item.BranchID,
                                                    item.InvoiceNo
                                                    ).FirstOrDefault();
            return vendorrmainvoicedetailItem;
        }



        #endregion

    }

}
