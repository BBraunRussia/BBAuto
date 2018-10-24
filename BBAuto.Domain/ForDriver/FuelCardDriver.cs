using System;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Entities;
using Common;

namespace BBAuto.Domain.ForDriver
{
  public class FuelCardDriver : MainDictionary
  {
    public FuelCard FuelCard { get; private set; }
    public DateTime DateBegin { get; set; }
    public DateTime? DateEnd { get; set; }
    public Driver Driver { get; set; }

    public bool IsNotUse
    {
      get => DateEnd != null;
      set
      {
        if (!value) DateEnd = null;
      }
    }

    public FuelCardDriver(FuelCard fuelCard)
    {
      FuelCard = fuelCard;
      DateBegin = DateTime.Today;
      Driver = DriverList.getInstance().getItem(Consts.ReserveDriverId);
      IsNotUse = false;
    }

    public FuelCardDriver(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);

      int.TryParse(row.ItemArray[1].ToString(), out int idFuelCard);
      FuelCard = FuelCardList.getInstance().getItem(idFuelCard);

      int.TryParse(row.ItemArray[2].ToString(), out int idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      DateTime.TryParse(row.ItemArray[3].ToString(), out DateTime dateBegin);
      DateBegin = dateBegin;

      if (DateTime.TryParse(row.ItemArray[4].ToString(), out DateTime dateEnd))
      {
        DateEnd = dateEnd;
      }
    }

    public override void Save()
    {
      var dateBeginSql = string.Concat(DateBegin.Year.ToString(), "-", DateBegin.Month.ToString(), "-",
        DateBegin.Day.ToString());

      var dateEndSql = string.Empty;
      if (DateEnd != null)
      {
        dateEndSql = string.Concat(DateEnd.Value.Year.ToString(), "-", DateEnd.Value.Month.ToString(), "-",
          DateEnd.Value.Day.ToString());
      }

      ID = Convert.ToInt32(_provider.Insert("FuelCardDriver", ID, FuelCard?.ID ?? 0, Driver.ID,
        dateBeginSql, dateEndSql));

      FuelCardDriverList.getInstance().Add(this);
    }

    internal override void Delete()
    {
      _provider.Delete("FuelCardDriver", ID);
    }

    internal override object[] getRow()
    {
      return new object[]
      {
        ID, FuelCard.ID, FuelCard.Number, Driver.FullName, FuelCard.Region, FuelCard.DateEnd,
        FuelCard.FuelCardType,
        DateBegin, DateEnd ?? new DateTime(1, 1, 1)
      };
    }

    public override string ToString()
    {
      return FuelCard == null ? string.Empty : FuelCard.Number + " " + FuelCard.FuelCardType;
    }
  }
}
