using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EZY.RMAS.Contract;
using System.Data.Common;

namespace EZY.RMAS.DataFactory
{
    public class DeclarantDAL
	{
		private Database db;
		private DbTransaction currentTransaction = null;
		private DbConnection connection = null;

		/// <summary>
		/// Constructor
		/// </summary>
		public DeclarantDAL()
		{

			db = DatabaseFactory.CreateDatabase("RMAS");

		}

		#region IDataFactory Members

		public List<Declarant> GetList(short branchID)
		{

			return db.ExecuteSprocAccessor(DBRoutine.LISTDECLARANT, MapBuilder<Declarant>.BuildAllProperties(), branchID).ToList();
		}



		public bool Save<T>(T item, DbTransaction parentTransaction) where T : IContract
		{
			currentTransaction = parentTransaction;
			return Save(item);

		}




		public bool Save<T>(T item) where T : IContract
		{
			var result = 0;

			var declarant = (Declarant)(object)item;

			if (currentTransaction == null)
			{
				connection = db.CreateConnection();
				connection.Open();
			}

			var transaction = (currentTransaction == null ? connection.BeginTransaction() : currentTransaction);


			try
			{
				var savecommand = db.GetStoredProcCommand(DBRoutine.SAVEDECLARANT);

				db.AddInParameter(savecommand, "BranchID", System.Data.DbType.Int16, declarant.BranchID);
				db.AddInParameter(savecommand, "DeclarantName", System.Data.DbType.String, declarant.DeclarantName);
				db.AddInParameter(savecommand, "Designation", System.Data.DbType.String, declarant.Designation);
				db.AddInParameter(savecommand, "IsActive", System.Data.DbType.Boolean, declarant.IsActive);


				result = db.ExecuteNonQuery(savecommand, transaction);

				if (result > 0)
					transaction.Commit();
				else
					transaction.Rollback();

			}
			catch (Exception)
			{
				transaction.Rollback();

				throw;
			}

			return (result > 0 ? true : false);

		}

		public bool Delete<T>(T item) where T : IContract
		{
			var result = false;
			var declarant = (Declarant)(object)item;

			var connnection = db.CreateConnection();
			connnection.Open();

			var transaction = connnection.BeginTransaction();

			try
			{
				var deleteCommand = db.GetStoredProcCommand(DBRoutine.DELETEDECLARANT);

				db.AddInParameter(deleteCommand, "BranchID", System.Data.DbType.Int16, declarant.BranchID);
				db.AddInParameter(deleteCommand, "DeclarantName", System.Data.DbType.String, declarant.DeclarantName);

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

		public Declarant GetItem<T>(IContract lookupItem) where T : IContract
		{
			var item = ((Declarant)lookupItem);

			var declarationItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDECLARANT,
													MapBuilder<Declarant>
													.BuildAllProperties(), item.BranchID, item.DeclarantName).FirstOrDefault();



			return declarationItem;

		}

		#endregion






	}
}
