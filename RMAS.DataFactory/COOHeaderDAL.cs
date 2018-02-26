using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
    public class COOHeaderDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public COOHeaderDAL()
        {

            db = DatabaseFactory.CreateDatabase("RMAS");

        }

        #region IDataFactory Members

        public List<COOHeader> GetList(short branchID)
        {
            return db.ExecuteSprocAccessor(DBRoutine.LISTCOOHEADER, MapBuilder<COOHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.COODetails)
                                           .Build(), branchID).ToList();
        }
        public List<COOHeader> SearchList(short branchID, string vesselName, string consigneeName, DateTime dateFrom, DateTime dateTo)
        {
            return db.ExecuteSprocAccessor(DBRoutine.SEARCHCOOHEADER, MapBuilder<COOHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.COODetails)
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

            var cooheader = (COOHeader)(object)item;

            if (currentTransaction == null)
            {
                connection = db.CreateConnection();
                connection.Open();
            }

            var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


            try
            {
                var savecommand = db.GetStoredProcCommand(DBRoutine.SAVECOOHEADER);

                db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, cooheader.BranchID);
                db.AddInParameter(savecommand, "DocumentNo", System.Data.DbType.String, cooheader.DocumentNo);
                db.AddInParameter(savecommand, "ExporterName", System.Data.DbType.String, cooheader.ExporterName);
                db.AddInParameter(savecommand, "ExporterAddress", System.Data.DbType.String, cooheader.ExporterAddress);
                db.AddInParameter(savecommand, "ConsigneeName", System.Data.DbType.String, cooheader.ConsigneeName);
                db.AddInParameter(savecommand, "ConsigneeAddress", System.Data.DbType.String, cooheader.ConsigneeAddress);
                db.AddInParameter(savecommand, "DepartureDate", System.Data.DbType.DateTime, cooheader.DepartureDate);
                db.AddInParameter(savecommand, "DestinationPort", System.Data.DbType.String, cooheader.DestinationPort);
                db.AddInParameter(savecommand, "VesselName", System.Data.DbType.String, cooheader.VesselName);
                db.AddInParameter(savecommand, "DestinationCountry", System.Data.DbType.String, cooheader.DestinationCountry);
                db.AddInParameter(savecommand, "DeclarantName", System.Data.DbType.String, cooheader.DeclarantName);
                db.AddInParameter(savecommand, "InvoiceNo", System.Data.DbType.String, cooheader.InvoiceNo);
                db.AddInParameter(savecommand, "InvoiceDate", System.Data.DbType.Date, cooheader.InvoiceDate);
                db.AddInParameter(savecommand, "IsInvoiceConfirm", System.Data.DbType.Boolean, cooheader.IsInvoiceConfirm);
                db.AddInParameter(savecommand, "IsCertified", System.Data.DbType.Boolean, cooheader.IsInvoiceConfirm);
                db.AddOutParameter(savecommand, "NewDocumentNo", System.Data.DbType.String, 50);
                db.AddInParameter(savecommand, "Country1", System.Data.DbType.String, cooheader.Country1);
                db.AddInParameter(savecommand, "Country2", System.Data.DbType.String, cooheader.Country2);
                db.AddInParameter(savecommand, "Country3", System.Data.DbType.String, cooheader.Country3);
                db.AddInParameter(savecommand, "Country4", System.Data.DbType.String, cooheader.Country4);
                db.AddInParameter(savecommand, "Country5", System.Data.DbType.String, cooheader.Country5);
                db.AddInParameter(savecommand, "Country6", System.Data.DbType.String, cooheader.Country6);
                db.AddInParameter(savecommand, "CreatedBy", System.Data.DbType.String, cooheader.CreatedBy);             



                result = db.ExecuteNonQuery(savecommand, transaction);


                if (result > 0)
                {
                    cooheader.DocumentNo = savecommand.Parameters["@NewDocumentNo"].Value.ToString();
                    cooheader.COODetails.ForEach(dt =>
                    {
                        dt.DocumentNo = cooheader.DocumentNo;
                        dt.BranchID = cooheader.BranchID;
                    });


                    COODetailDAL coodetailDAL = new COODetailDAL();

                    result = coodetailDAL.SaveList(cooheader.COODetails, transaction) == true ? 1 : 0;

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
            var cooheader = (COOHeader)(object)item;

            var connnection = db.CreateConnection();
            connnection.Open();

            var transaction = connnection.BeginTransaction();

            try
            {
                var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETECOOHEADER);

                db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, cooheader.BranchID);
                db.AddInParameter(deleteCommand, "DocumentNo", System.Data.DbType.String, cooheader.DocumentNo);

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
            var item = ((COOHeader)lookupItem);

            var cooheaderItem = db.ExecuteSprocAccessor(DBRoutine.SELECTCOOHEADER,
                                                    MapBuilder<COOHeader>.MapAllProperties()
                                           .DoNotMap(hd => hd.COODetails)
                                           .Build(),
                                                    ((COOHeader)lookupItem).BranchID, item.DocumentNo).FirstOrDefault();

            if (cooheaderItem == null) return null;


            cooheaderItem.COODetails = new COODetailDAL().GetList(cooheaderItem.BranchID, cooheaderItem.DocumentNo);



            return cooheaderItem;
        }

        #endregion

    }
}

