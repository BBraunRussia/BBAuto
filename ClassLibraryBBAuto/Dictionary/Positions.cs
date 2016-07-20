﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class Positions : MyDictionary
    {
        private static Positions uniqueInstance;

        public static Positions getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Positions();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Position");

            fillList(dt);
        }
    }
}
