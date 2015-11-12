﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Colors : MyDictionary
    {
        private static Colors uniqueInstance;
        
        public static Colors getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Colors();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Color");

            fillList(dt);
        }
    }
}
