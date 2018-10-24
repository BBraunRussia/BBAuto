using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
    public class DilerList : MainList
    {
        private static DilerList uniqueInstance;
        private List<Diler> list;

        private DilerList()
        {
            list = new List<Diler>();

            loadFromSql();
        }

        public static DilerList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DilerList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Diller");

            foreach (DataRow row in dt.Rows)
            {
                Diler diller = new Diler(row);
                Add(diller);
            }
        }

        internal void Add(Diler diller)
        {
            if (list.Exists(item => item == diller))
                return;

            list.Add(diller);
        }

        public void Delete(int idDiller)
        {
            Diler diller = getItem(idDiller);

            list.Remove(diller);

            diller.Delete();
        }

        public Diler getItem(int id)
        {
            return list.FirstOrDefault(d => d.ID == id);
        }
        
        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");
            dt.Columns.Add("Контакты");

            return dt;
        }

        public DataTable ToDataTable()
        {
            DataTable dt = createTable();

            foreach (Diler diller in list)
                dt.Rows.Add(diller.getRow());

            return dt;
        }
    }
}
