using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
    public class Depts : MyDictionary
    {
        private static Depts uniqueInstance;

        public static Depts getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Depts();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Dept");

            fillList(dt);
        }
    }
}
