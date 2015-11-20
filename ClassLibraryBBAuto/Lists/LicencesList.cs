﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class LicenseList : MainList, INotificationList
    {
        private List<DriverLicense> _list;
        private static LicenseList _uniqueInstance;

        private LicenseList()
        {
            _list = new List<DriverLicense>();

            loadFromSql();
        }

        public static LicenseList getInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new LicenseList();

            return _uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("DriverLicense");

            foreach (DataRow row in dt.Rows)
            {
                DriverLicense driverLicense = new DriverLicense(row);
                Add(driverLicense);
            }
        }

        public void Add(DriverLicense driverLicense)
        {
            if (_list.Exists(item => item == driverLicense))
                return;

            _list.Add(driverLicense);
        }

        public DataTable ToDataTable(Driver driver)
        {
            var driverLicenses = _list.Where(item => item.idEqualDriverID(driver)).ToList();

            driverLicenses.Sort(Compare);

            return createTable(driverLicenses);
        }

        private DataTable createTable(List<DriverLicense> driverLicenses)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Номер");
            dt.Columns.Add("Дата окончания действия");

            foreach (DriverLicense driverLicense in driverLicenses)
                dt.Rows.Add(driverLicense.getRow());

            return dt;
        }

        public DriverLicense getItem(int id)
        {
            var driverLicenses = _list.Where(item => item.IsEqualsID(id));

            return (driverLicenses.Count() > 0) ? driverLicenses.First() : null;
        }

        public DriverLicense getItem(Driver driver)
        {
            var driverLicenses = _list.Where(item => item.idEqualDriverID(driver)).ToList();

            driverLicenses.Sort(Compare);

            return (driverLicenses.Count() > 0) ? driverLicenses.First() : driver.createDriverLicense();
        }

        public List<INotification> ToList()
        {
            DriverList driverList = DriverList.getInstance();
            List<DriverLicense> listNew = _list.Where(item => !driverList.getItem(item.DriverID).Fired && !driverList.getItem(item.DriverID).Decret && driverList.getItem(item.DriverID).IsDriver).ToList();

            List<INotification> listNotification = new List<INotification>();
            foreach (INotification item in listNew)
                listNotification.Add(item);

            return listNotification;
        }

        public void Delete(int idLicence)
        {
            DriverLicense licence = getItem(idLicence);

            _list.Remove(licence);

            licence.Delete();
        }

        private int Compare(DriverLicense license1, DriverLicense license2)
        {
            if (license1.DateEnd == license2.DateEnd)
                return DateTime.Compare(license1.DateBegin, license2.DateBegin) * -1;

            return DateTime.Compare(license1.DateEnd, license2.DateEnd) * -1;
        }
    }
}
