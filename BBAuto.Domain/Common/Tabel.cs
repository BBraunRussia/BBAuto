using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Lists;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Common
{
    public class Tabel
    {
        private IProvider provider;

        public Driver Driver { get; private set; }
        public DateTime Date { get; private set; }
        public string Comment { get; set; }
        
        public Tabel(string number, DateTime date)
        {
            Driver = DriverList.getInstance().getItemByNumber(number);
            Date = date;
            Comment = string.Empty;

            provider = Provider.GetProvider();
        }

        public Tabel(string comm, Driver driver, DateTime date)
        {
            Driver = driver;
            Date = date;
            Comment = comm;

            provider = Provider.GetProvider();
        }
        

        public Tabel(DataRow row)
        {
            int idDriver;
            int.TryParse(row[0].ToString(), out idDriver);

            Driver = DriverList.getInstance().getItem(idDriver);

            DateTime date;
            DateTime.TryParse(row[1].ToString(), out date);
            Date = date;

            if (row[2] != null)
                Comment = row[2].ToString();
            else
                Comment = string.Empty;
        }

        public void Save()
        {
            provider.Insert("Tabel", Driver.ID, Date, Comment);
        }

        public void Save2()
        {
            provider.Insert("Tabel_test", Driver.ID, Date, Comment);
        }
    }
}
