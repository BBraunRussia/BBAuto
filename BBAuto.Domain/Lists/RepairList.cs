using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
    public class RepairList : MainList
    {
        private List<Repair> list;
        private static RepairList uniqueInstance;

        private RepairList()
        {
            list = new List<Repair>();

            loadFromSql();
        }

        public static RepairList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new RepairList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Repair");

            foreach (DataRow row in dt.Rows)
            {
                Repair repair = new Repair(row);
                Add(repair);
            }
        }

        public void Add(Repair repair)
        {
            if (list.Exists(item => item == repair))
                return;

            list.Add(repair);
        }

        public Repair getItem(int id)
        {
            return list.FirstOrDefault(r => r.ID == id);
        }

        public void Delete(int idRepair)
        {
            Repair repair = getItem(idRepair);

            list.Remove(repair);

            repair.Delete();
        }

        public DataTable ToDataTable()
        {
            DataTable dt = createTable();

            var repairs = from repair in list
                          orderby repair.Date ascending
                          select repair;

            foreach (Repair repair in repairs)
                dt.Rows.Add(repair.getRow());

            return dt;
        }

        public DataTable ToDataTableByCar(Car car)
        {
            DataTable dt = createTable();

            var repairs = from repair in list
                          where repair.Car.ID == car.ID
                          orderby repair.Date ascending
                          select repair;

            foreach (Repair repair in repairs)
                dt.Rows.Add(repair.getRow());

            return dt;
        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("ГРЗ");
            dt.Columns.Add("Вид ремонта");
            dt.Columns.Add("СТО");
            dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
            dt.Columns.Add("Стоимость", Type.GetType("System.Double"));
            dt.Columns.Add("Файл");

            return dt;
        }
    }
}
