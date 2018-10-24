using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
    public class Statuses : MyDictionary
    {
        private static Statuses uniqueInstance;

        public static Statuses getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Statuses();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Status");

            fillList(dt);
        }
    }
}
