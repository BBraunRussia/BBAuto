﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Roles : MyDictionary
    {
        private static Roles uniqueInstance;

        public static Roles getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Roles();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Role");

            fillList(dt);
        }
    }
}
