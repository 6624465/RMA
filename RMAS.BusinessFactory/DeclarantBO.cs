using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{

	public class DeclarantBO
	{
		private DeclarantDAL declarantDAL;
		public DeclarantBO()
		{

			declarantDAL = new DeclarantDAL();
		}

		public List<Declarant> GetList(short branchID)
		{
			return declarantDAL.GetList(branchID);
		}


		public bool SaveDeclarant(Declarant newItem)
		{

			return declarantDAL.Save(newItem);

		}

		public bool DeleteDeclarant(Declarant item)
		{
			return declarantDAL.Delete(item);
		}

		public Declarant GetDeclarant(Declarant item)
		{
			return (Declarant)declarantDAL.GetItem<Declarant>(item);
		}

	}
}
