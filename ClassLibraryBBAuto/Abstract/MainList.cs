﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataLayer;
using BBAuto.Domain.DataBase;

namespace BBAuto.Domain.Abstract
{
    public abstract class MainList
    {
        protected IProvider _provider;
        
        protected abstract void loadFromSql();

        protected MainList()
        {
            _provider = Provider.GetProvider();
        }
        
        public void ReLoad()
        {
            loadFromSql();
        }
    }
}
