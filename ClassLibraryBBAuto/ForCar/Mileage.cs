using BBAuto.Domain.Abstract;
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
    public class Mileage : MainDictionary
    {
        private int _count;

        public string Count
        {
            get { return _count == 0 ? string.Empty : _count.ToString(); }
        }

        public DateTime Date { get; set; }
        public Car Car { get; private set; }

        internal Mileage(Car car)
        {
            Car = car;
            Date = DateTime.Today.Date;
            ID = 0;
        }

        public Mileage(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            ID = Convert.ToInt32(row.ItemArray[0]);

            int idCar;
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            Car = CarList.GetInstance().getItem(idCar);

            DateTime date;
            DateTime.TryParse(row.ItemArray[2].ToString(), out date);
            Date = date;

            int.TryParse(row.ItemArray[3].ToString(), out _count);
        }

        public override void Save()
        {
            ID = Convert.ToInt32(_provider.Insert("Mileage", ID, Car.ID, Date, _count));
            
            MileageList mileageList = MileageList.getInstance();
            mileageList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[3] { ID, Date, _count };
        }
        
        internal override void Delete()
        {
            _provider.Delete("Mileage", ID);
        }

        internal DateTime MonthToString()
        {
            MyDateTime myDate = new MyDateTime(Date.ToShortDateString());

            return (_count == 0) ? new DateTime(DateTime.Today.Year, 1, 31) : Date;
        }

        public void SetCount(string value)
        {
            Mileage mileage = GetPrev();

            int count;

            if (!int.TryParse(value.Replace(" ", ""), out count))
                throw new InvalidCastException();

            int prevCount = 0;
            if (mileage != null)
            {
                int.TryParse(mileage.Count, out prevCount);

                if ((count < prevCount) && (Date > mileage.Date))
                    throw new InvalidConstraintException();
            }

            if (count >= 1000000)
                throw new OverflowException();

            _count = count;
        }

        public string PrevToString()
        {
            Mileage mileage = GetPrev();

            return (mileage == null) ? "0" : mileage.ToString();
        }

        private Mileage GetPrev()
        {
            return MileageList.getInstance().getItem(Car, this);
        }

        public override string ToString()
        {
            return (Count == string.Empty) ? "(нет данных)" : string.Concat(MyString.GetFormatedDigitInteger(Count), " км от ", Date.ToShortDateString());
        }
    }
}
