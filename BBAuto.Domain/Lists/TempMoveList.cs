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
    public class TempMoveList : MainList
    {
        private List<TempMove> list;
        private static TempMoveList uniqueInstance;

        private TempMoveList()
        {
            list = new List<TempMove>();

            loadFromSql();
        }

        public static TempMoveList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new TempMoveList();

            return uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("TempMove");

            foreach (DataRow row in dt.Rows)
            {
                TempMove tempMove = new TempMove(row);
                Add(tempMove);
            }
        }

        public void Add(TempMove tempMove)
        {
            if (list.Exists(item => item == tempMove))
                return;

            list.Add(tempMove);
        }

        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("Водитель");
            dt.Columns.Add("Начало", Type.GetType("System.DateTime"));
            dt.Columns.Add("Окончание", Type.GetType("System.DateTime"));

            var tempMoves = list.OrderByDescending(item => item.DateEnd);

            foreach (TempMove tempMove in tempMoves)
                dt.Rows.Add(tempMove.getRow());

            return dt;
        }

        public TempMove getItem(int id)
        {
            return list.FirstOrDefault(t => t.ID == id);
        }

        internal Driver getDriver(Car car, DateTime date)
        {
            var tempMoves = list.Where(item => item.IsDriverCar(car, date));

            if (tempMoves.Count() > 0)
            {
                TempMove tempMove = tempMoves.First() as TempMove;
                return tempMove.Driver;
            }
            else
                return null;
        }
    }
}
