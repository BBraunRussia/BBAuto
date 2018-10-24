using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
    public class ViolationTypes : MyDictionary
    {
        private static ViolationTypes uniqueInstance;

        public static ViolationTypes getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ViolationTypes();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("ViolationType");

            fillList(dt);
        }
    }
}
