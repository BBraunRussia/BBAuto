using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
    public class Repair : MainDictionary
    {
        private int _idRepairType;
        private int _idServiceStantion;
        private double _cost;

        public Car Car { get; private set; }
        public DateTime Date { get; set; }
        public string File { get; set; }

        public string RepairTypeID
        {
            get { return _idRepairType.ToString(); }
            set { int.TryParse(value, out _idRepairType); }
        }

        public string ServiceStantionID
        {
            get { return _idServiceStantion.ToString(); }
            set { int.TryParse(value, out _idServiceStantion); }
        }

        public string Cost
        {
            get { return _cost.ToString(); }
            set { double.TryParse(value, out _cost); }
        }
        
        public Repair(Car car)
        {
            ID = 0;
            Car = car;
            Date = DateTime.Today;
        }

        public Repair(Car car, DataRow row)
        {
            Car = car;

            fillFields(row);
        }

        public Repair(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            ID = id;

            int idCar;
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            Car = CarList.GetInstance().getItem(idCar);

            int.TryParse(row.ItemArray[2].ToString(), out _idRepairType);
            int.TryParse(row.ItemArray[3].ToString(), out _idServiceStantion);

            DateTime date;
            DateTime.TryParse(row.ItemArray[4].ToString(), out date);
            Date = date;

            Cost = row.ItemArray[5].ToString();
            File = row.ItemArray[6].ToString();
            _fileBegin = File;
        }

        internal override object[] getRow()
        {
            string show = "";
            if (!string.IsNullOrEmpty(File))
                show = "Показать";

            RepairTypes repairTypes = RepairTypes.getInstance();
            ServiceStantions serviceStantions = ServiceStantions.getInstance();

            return new object[] { ID, Car.ID, Car.BBNumber, Car.Grz, repairTypes.getItem(_idRepairType), serviceStantions.getItem(_idServiceStantion),
                Date, _cost, show };
        }

        public override void Save()
        {
            int id;

            if (ID == 0)
            {
                int.TryParse(_provider.Insert("Repair", ID, Car.ID, _idRepairType, _idServiceStantion, Date, _cost, File), out id);
                ID = id;
            }

            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Repair", ID.ToString());
            int.TryParse(_provider.Insert("Repair", ID, Car.ID, _idRepairType, _idServiceStantion, Date, _cost, File), out id);
            ID = id;
        }

        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("Repair", ID);
        }
    }
}
