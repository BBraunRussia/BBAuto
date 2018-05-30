using BBAuto.Domain.Abstract;
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
    public class CarSale : MainDictionary
    {
        private DateTime _date;
        public string comm;

        public DateTime DateForSort { get { return _date.Year == 1 ? DateTime.Today : _date; } }

        public string Date
        {
            get { return _date.Year == 1 ? string.Empty : _date.ToShortDateString(); }
            set { DateTime.TryParse(value, out _date); }
        }
        
        internal CarSale(DataRow row)
        {
            fillFields(row);
        }

        public Car Car
        {
            get { return CarList.GetInstance().getItem(ID); }
            private set { ID = value.ID; }
        }

        public CarSale(Car car)
        {
            Car = car;
            comm = string.Empty;
        }

        private void fillFields(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            ID = id;

            Date = row.ItemArray[1].ToString();
            comm = row.ItemArray[2].ToString();
        }

        public override void Save()
        {
            string Sqldate = string.Empty;
            if (Date != string.Empty)
                Sqldate = string.Concat(_date.Year.ToString(), "-", _date.Month.ToString(), "-", _date.Day.ToString());

            _provider.Insert("CarSale", ID, comm, Sqldate);

            CarSaleList carSaleList = CarSaleList.getInstance();
            carSaleList.Add(this);
        }

        internal override object[] getRow()
        {
            InvoiceList invoiceList = InvoiceList.getInstance();
            Invoice invoice = invoiceList.getItem(Car);

            PTSList ptsList = PTSList.getInstance();
            PTS pts = ptsList.getItem(Car);

            STSList stsList = STSList.getInstance();
            STS sts = stsList.getItem(Car);

            int idRegion = 0;
            int.TryParse(Car.regionUsingID.ToString(), out idRegion);

            Regions regions = Regions.getInstance();
            string regionName = (invoice == null) ? regions.getItem(idRegion) : regions.getItem(Convert.ToInt32(invoice.RegionToID));

            return new object[] { ID, ID, Car.BBNumber, Car.Grz, regionName, _date, comm, pts.Number, sts.Number, Car.GetStatus() };
        }

        internal override void Delete()
        {
            _provider.Delete("CarSale", ID);
        }
    }
}
