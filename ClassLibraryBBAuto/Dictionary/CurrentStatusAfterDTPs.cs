﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class CurrentStatusAfterDTPs : MyDictionary
    {
        private static CurrentStatusAfterDTPs uniqueInstance;

        public static CurrentStatusAfterDTPs getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CurrentStatusAfterDTPs();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("CurrentStatusAfterDTP");

            fillList(dt);
        }
    }
}
