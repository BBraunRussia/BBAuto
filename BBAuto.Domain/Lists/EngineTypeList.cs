using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;

namespace BBAuto.Domain.Lists
{
    public class EngineTypeList : MainList
    {
        private static EngineTypeList uniqueInstance;
        private List<EngineType> list;

        private EngineTypeList()
        {
            list = new List<EngineType>();

            loadFromSql();
        }

        public static EngineTypeList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new EngineTypeList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("EngineType");

            foreach (DataRow row in dt.Rows)
            {
                EngineType engineType = new EngineType(row);
                Add(engineType);
            }
        }

        public void Add(EngineType engineType)
        {
            if (list.Exists(et => et.ID == engineType.ID))
                return;

            list.Add(engineType);
        }

        public EngineType getItem(int id)
        {
            return list.FirstOrDefault(et => et.ID == id);
        }
    }
}
