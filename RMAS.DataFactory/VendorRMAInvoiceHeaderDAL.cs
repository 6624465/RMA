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
    public class VendorRMAInvoiceHeaderDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
             #region IDataFactory Members
        public VendorRMAInvoiceHeaderDAL()
        {
            db = DatabaseFactory.CreateDatabase("RMAS");
        }
        public List<VendorRMAInvoiceHeader> GetList(Int16 branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTVENDORRMAINVOICEHEADER,
                                           MapBuilder<VendorRMAInvoiceHeader>.MapAllProperties()
                                           .DoNotMap(x => x.CompanyName)
                                           .DoNotMap(x => x.CustomerAddress)
                                           .DoNotMap(hd => hd.VendorRMAInvoiceDetails)
                                           .DoNotMap(hd => hd.VendorRMAInvoiceDimensions)
                                           .Build(), branchID).ToList();
        }


        public bool Save<T>(T item) where T : IContract
        {
            var result = 0;

            var vendorrmainvoiceheader = (VendorRMAInvoiceHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);

            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEVENDORRMAINVOICEHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, vendorrmainvoiceheader.BranchID);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, vendorrmainvoiceheader.InvoiceNo);
                db.AddInParameter(savecommand, "InvoiceDate", System.Data.DbType.DateTime,DateTime.Now);
                db.AddInParameter(savecommand, "ReferenceNo", System.Data.DbType.String, vendorrmainvoiceheader.ReferenceNo);
                db.AddInParameter(savecommand, "TotalQty", System.Data.DbType.Int64, vendorrmainvoiceheader.TotalQty);
                db.AddInParameter(savecommand, "TotalAmount", System.Data.DbType.Decimal, vendorrmainvoiceheader.TotalAmount);
                db.AddInParameter(savecommand, "TotalWeight", System.Data.DbType.Decimal, vendorrmainvoiceheader.TotalWeight);
                db.AddInParameter(savecommand, "PaymentTerm", System.Data.DbType.String, vendorrmainvoiceheader.PaymentTerm);
                db.AddInParameter(savecommand, "LoadingPort", System.Data.DbType.String, vendorrmainvoiceheader.LoadingPort);
                db.AddInParameter(savecommand, "DischargePort", System.Data.DbType.String, vendorrmainvoiceheader.DischargePort);
                db.AddInParameter(savecommand, "ShipmentTerms", System.Data.DbType.String, vendorrmainvoiceheader.ShipmentTerms);
                db.AddInParameter(savecommand, "OriginCountry", System.Data.DbType.String, vendorrmainvoiceheader.OriginCountry);
                db.AddInParameter(savecommand, "AWB", System.Data.DbType.String, vendorrmainvoiceheader.AWB);
                db.AddInParameter(savecommand, "Remarks", System.Data.DbType.String, vendorrmainvoiceheader.Remarks);
                db.AddInParameter(savecommand, "Dimension", System.Data.DbType.String, vendorrmainvoiceheader.Dimension);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, vendorrmainvoiceheader.CreatedBy);
                db.AddInParameter(savecommand, "ModifiedBy", System.Data.DbType.String, vendorrmainvoiceheader.ModifiedBy);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);

                result = db.ExecuteNonQuery(savecommand, transaction);

                if (result > 0)
                {
                    vendorrmainvoiceheader.InvoiceNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    vendorrmainvoiceheader.VendorRMAInvoiceDetails.ForEach(dt =>
                    {
                        dt.BranchID = vendorrmainvoiceheader.BranchID;
                        dt.InvoiceNo = vendorrmainvoiceheader.InvoiceNo;
                        dt.CreatedBy = vendorrmainvoiceheader.CreatedBy;
                        dt.ModifiedBy = vendorrmainvoiceheader.ModifiedBy;
                    });
                    VendorRMAInvoiceDetailDAL vendorRMAinvoiceDetailDAL = new VendorRMAInvoiceDetailDAL();

                    result = vendorRMAinvoiceDetailDAL.SaveList(vendorrmainvoiceheader.VendorRMAInvoiceDetails, transaction) == true ? 1 : 0;


                    if (vendorrmainvoiceheader.VendorRMAInvoiceDimensions != null)
                    {
                        List<VendorRMAInvoiceDimension> rmaDimensionList = new List<VendorRMAInvoiceDimension>();
                        vendorrmainvoiceheader.VendorRMAInvoiceDimensions.ForEach(dt =>
                        {
                            dt.BranchID = vendorrmainvoiceheader.BranchID;
                            dt.InvoiceNo = vendorrmainvoiceheader.InvoiceNo;
                        });

                        VendorRMAInvoiceDimensionDAL vendorRMAinvoiceDimensionsDAL = new VendorRMAInvoiceDimensionDAL();

                        result = vendorRMAinvoiceDimensionsDAL.SaveList(vendorrmainvoiceheader.VendorRMAInvoiceDimensions, transaction) == true ? 1 : 0;
                    }

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
        public bool Delete(short branchID, string invoiceNo)
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
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEVENDORRMAINVOICEHEADER);
                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, branchID);
                db.AddInParameter(deleteCommand, "InvoiceNo", System.Data.DbType.String, invoiceNo);


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

            var item = (VendorRMAInvoiceHeader)lookupItem;

            var vendorrmaheaderItem = db.ExecuteSprocAccessor(DBRoutine.SELECTVENDORRMAINVOICEHEADER,
                                                    MapBuilder<VendorRMAInvoiceHeader>
                                                    .MapAllProperties()
                                                    .DoNotMap(x=>x.CompanyName)
                                                    .DoNotMap(x => x.CustomerAddress)
                                                    .DoNotMap(hd => hd.VendorRMAInvoiceDetails)
                                                    .DoNotMap(hd => hd.VendorRMAInvoiceDimensions)
                                                    .Build(),
                                                    item.BranchID,
                                                    item.InvoiceNo
                                                   ).FirstOrDefault();

            if (vendorrmaheaderItem != null)
            {
                vendorrmaheaderItem.VendorRMAInvoiceDetails = new VendorRMAInvoiceDetailDAL().GetList(item.BranchID,item.InvoiceNo);

                vendorrmaheaderItem.VendorRMAInvoiceDimensions = new VendorRMAInvoiceDimensionDAL().GetList(item.BranchID,item.InvoiceNo);
            }

            return vendorrmaheaderItem;
        }

        #endregion

    }
}
