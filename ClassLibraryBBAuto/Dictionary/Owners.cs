﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class Owners : MyDictionary
    {
        private static Owners uniqueInstance;

        public static Owners getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Owners();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Owner");

            fillList(dt);
        }
    }
}
