﻿using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
    public class STS : MainDictionary
    {
        private string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value.ToUpper(); }
        }

        public string GiveOrg { get; set; }
        public DateTime Date { get; set; }
        public string File { get; set; }

        public Car Car { get; private set; }
        
        internal STS(Car car)
        {
            Car = car;

            Date = DateTime.Today;
            Number = string.Empty;
        }

        public STS(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            Car = CarList.getInstance().getItem(id);

            Number = row.ItemArray[1].ToString();
            Date = Convert.ToDateTime(row.ItemArray[2]);
            GiveOrg = row.ItemArray[3].ToString();
            File = row.ItemArray[4].ToString();
            _fileBegin = File;
        }

        public override void Save()
        {
            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "cars", ID, "", "STS");

            _provider.Insert("STS", ID, Number, Date, GiveOrg, File);

            STSList stsList = STSList.getInstance();
            stsList.Add(this);
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }

        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("STS", ID);
        }
    }
}
