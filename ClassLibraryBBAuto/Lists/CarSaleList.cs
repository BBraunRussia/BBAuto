﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
    public class CarSaleList : MainList
    {
        private static CarSaleList uniqueInstance;
        private List<CarSale> list;

        private CarSaleList()
        {
            list = new List<CarSale>();

            loadFromSql();
        }

        public static CarSaleList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CarSaleList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("CarSale");

            foreach (DataRow row in dt.Rows)
            {
                CarSale carSale = new CarSale(row);
                Add(carSale);
            }
        }

        public void Add(CarSale carSale)
        {
            if (list.Exists(item => item == carSale))
                return;
            
            list.Add(carSale);
        }
                
        public DataTable ToDataTable()
        {
            List<CarSale> carSales = list.OrderByDescending(item => item.DateForSort).ToList();

            return createTable(carSales);
        }

        private DataTable createTable(List<CarSale> carSales)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Дата продажи", Type.GetType("System.DateTime"));
            dt.Columns.Add("Комментарий");
            dt.Columns.Add("№ ПТС");
            dt.Columns.Add("№ СТС");
            dt.Columns.Add("Статус");

            foreach (CarSale carSale in carSales)
                dt.Rows.Add(carSale.getRow());

            return dt;
        }

        public CarSale getItem(int id)
        {
            return list.FirstOrDefault(item => item.Car.ID == id);
        }
        
        public void Delete(int idCarSale)
        {
            CarSale carSale = getItem(idCarSale);

            list.Remove(carSale);

            carSale.Delete();
            
            ReLoad();
        }
    }
}
