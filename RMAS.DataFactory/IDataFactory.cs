﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
    interface IDataFactory
    {
        bool Save<T>(T item) where T : IContract;

        bool Delete<T>(T item) where T : IContract;

        IContract GetItem<T>(IContract item) where T : IContract;

        

    }
}
