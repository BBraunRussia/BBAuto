using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;

namespace BBAuto.Domain.Lists
{
    public class MarkList : MainList
    {
        private static MarkList uniqueInstance;
        private List<Mark> list;

        private MarkList()
        {
            list = new List<Mark>();

            loadFromSql();
        }

        public static MarkList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new MarkList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Mark");

            foreach (DataRow row in dt.Rows)
            {
                Mark mark = new Mark(row);
                Add(mark);
            }
        }

        public void Add(Mark mark)
        {
            if (list.Exists(item => item.ID == mark.ID))
                return;

            list.Add(mark);
        }
        
        public Mark getItem(int id)
        {
            return list.FirstOrDefault(m => m.ID == id);
        }
    }
}
