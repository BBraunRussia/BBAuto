using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Tables
{
    public class EngineType : MainDictionary
    {
        public EngineType(DataRow row)
        {
            ID = Convert.ToInt32(row[0]);
            Name = row[1].ToString();
            ShortName = row[2].ToString();
        }
        
        public string Name { get; private set; }
        public string ShortName { get; private set; }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }
    }
}
