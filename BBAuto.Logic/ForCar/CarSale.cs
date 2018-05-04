using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class CarSale : MainDictionary
  {
    private DateTime _date;
    public string comm;

    public DateTime DateForSort
    {
      get { return _date.Year == 1 ? DateTime.Today : _date; }
    }

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
      get { return CarList.getInstance().getItem(Id); }
      private set { Id = value.Id; }
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
      Id = id;

      Date = row.ItemArray[1].ToString();
      comm = row.ItemArray[2].ToString();
    }

    public override void Save()
    {
      string Sqldate = string.Empty;
      if (Date != string.Empty)
        Sqldate = string.Concat(_date.Year.ToString(), "-", _date.Month.ToString(), "-", _date.Day.ToString());

      Provider.Insert("CarSale", Id, comm, Sqldate);

      CarSaleList carSaleList = CarSaleList.getInstance();
      carSaleList.Add(this);
    }

    internal override object[] ToRow()
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
      string regionName = (invoice == null)
        ? regions.getItem(idRegion)
        : regions.getItem(Convert.ToInt32(invoice.RegionToID));

      return new object[]
        {Id, Id, Car.BBNumber, Car.Grz, regionName, _date, comm, pts.Number, sts.Number, Car.GetStatus()};
    }

    internal override void Delete()
    {
      Provider.Delete("CarSale", Id);
    }
  }
}
