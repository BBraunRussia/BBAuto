using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
    public class EngineTypes : MyDictionary
    {
        private static EngineTypes uniqueInstance;

        public static EngineTypes getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new EngineTypes();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("EngineType");

            fillList(dt);
        }
    }
}
