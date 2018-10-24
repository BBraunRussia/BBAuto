using BBAuto.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Dictionary
{
    public class StatusAfterDTPs : MyDictionary
    {
        private static StatusAfterDTPs uniqueInstance;

        public static StatusAfterDTPs getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new StatusAfterDTPs();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("StatusAfterDTP");

            fillList(dt);
        }
    }
}
