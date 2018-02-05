﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class CountryBO
    {
        private CountryDAL countryDAL;
        public CountryBO()
        {

            countryDAL = new CountryDAL();
        }

        public List<Country> GetList()
        {
            return countryDAL.GetList();
        }


        public bool SaveCountry(Country newItem)
        {

            return countryDAL.Save(newItem);

        }

        public bool DeleteCountry(Country item)
        {
            return countryDAL.Delete(item);
        }

        public Country GetCountry(Country item)
        {
            return (Country)countryDAL.GetItem<Country>(item);
        }

    }
}
