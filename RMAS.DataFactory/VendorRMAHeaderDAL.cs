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
   public class VendorRMAHeaderDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
             #region IDataFactory Members
        public VendorRMAHeaderDAL()
        {
            db = DatabaseFactory.CreateDatabase("RMAS");
        }
        public List<VendorRMAHeader> GetListByBranchID(Int16 branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.VENDORRMAHEADERLISTBYBRANCHID,
                                           MapBuilder<VendorRMAHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.VendorRmaDetails)
                                           .DoNotMap(hd=>hd.VendorRMAHeaderList)
                                           .DoNotMap(hd=>hd.CompanyName)
                                           .DoNotMap(hd=>hd.CustomerAddress)
                                           .Build(), branchID).ToList();
        }
        public List<RMAHeader> GetVendorOutwardList(DataTableObject Obj)
        {
            return db.ExecuteSprocAccessor(DBRoutine.VENDORRMAHEADERLISTBYBRANCHID2,
                MapBuilder<RMAHeader>.BuildAllProperties(),
                Obj.limit, Obj.offset, Obj.sortColumn, Obj.sortType, Obj.BranchID).ToList();
        }
        public bool SaveList<T>(List<T> items) where T : IContract
        {
            var result = true;

            if (items.Count == 0)
                result = true;

            foreach (var item in items)
            {
                result = Save(item);
                if (result == false) break;
            }


            return result;

        }
        //public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
        //{
        //    currentTransaction = parentTransaction;
        //    return Save(item);

        //}
        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var vendorrmaheader = (VendorRMAHeaderList)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMAHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, vendorrmaheader.BranchID);
                db.AddInParameter(savecommand, "DoNo", System.Data.DbType.String, vendorrmaheader.DoNo ?? "");
                db.AddInParameter(savecommand, "ReferenceNo", System.Data.DbType.String, vendorrmaheader.DocumentNo);
                db.AddInParameter(savecommand, "VendorInvoiceNo", System.Data.DbType.String, vendorrmaheader.VendorInvoiceNo);
                db.AddInParameter(savecommand, "VendorName", System.Data.DbType.String, vendorrmaheader.VendorName);
                db.AddInParameter(savecommand, "Status", System.Data.DbType.Boolean, vendorrmaheader.Status);
                db.AddInParameter(savecommand, "SerialNo", System.Data.DbType.String,"");
                db.AddInParameter(savecommand, "WarrantyExpiryDate", System.Data.DbType.DateTime, vendorrmaheader.WarrantyExpiryDate);
                db.AddInParameter(savecommand, "IsValid", System.Data.DbType.Boolean, vendorrmaheader.IsValid);
                db.AddInParameter(savecommand, "RMAIsValid", System.Data.DbType.Boolean, vendorrmaheader.RMAIsValid);
                db.AddInParameter(savecommand, "VendorWarrantyExpiryDate", System.Data.DbType.DateTime, vendorrmaheader.VendorWarrantyExpiryDate);            
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, vendorrmaheader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, vendorrmaheader.ModifiedBy);
                db.AddOutParameter(savecommand, "NewDoNo", System.Data.DbType.String,50);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (result > 0)
                {
                    vendorrmaheader.DoNo = savecommand.Parameters["@NewDoNo"].Value.ToString();
                    List<VendorRMADetails> rmaDetailList = new List<VendorRMADetails>();
                    vendorrmaheader.SerialNo.ForEach(dt =>
                    {
                        var rmaDetail = new VendorRMADetails {
                            DoNo = vendorrmaheader.DoNo,
                            SerialNo = dt,
                            BranchID = vendorrmaheader.BranchID
                        };
                        rmaDetailList.Add(rmaDetail);
                    });


                    VendorRMADetailDAL vendorRMADetails = new VendorRMADetailDAL();

                    result = vendorRMADetails.SaveList(rmaDetailList, transaction) == true ? 1 : 0;
                }

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
        public bool CloseVendorRMAHeader(short branchID, string referenceNo,string documentNo)
        {
            var result = false;


            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            //currentTransaction = parentTransaction;


            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.CLOSEVENDORRMAHEADER);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(deleteCommand, "ReferenceNo", System.Data.DbType.String, referenceNo);
                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, documentNo);

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
        #endregion

    }
}
