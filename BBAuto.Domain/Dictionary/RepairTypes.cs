using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
    public class RepairTypes : MyDictionary
    {
        private static RepairTypes uniqueInstance;

        public static RepairTypes getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new RepairTypes();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("RepairType");

            fillList(dt);
        }
    }
}
