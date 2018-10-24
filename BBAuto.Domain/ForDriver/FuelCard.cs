using System;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Static;
using BBAuto.Domain.Lists;

namespace BBAuto.Domain.ForDriver
{
  public class FuelCard : MainDictionary
  {
    private int _idFuelCardType;
    private DateTime _dateEnd;
    private int _idRegion;
    private string _pin;
    private int _lost;
    private string _number;

    public string Number
    {
      get
      {
        try
        {
          return string.IsNullOrEmpty(_number)
            ? string.Empty
            : _idFuelCardType == 1
              ? _number.Insert(1, " ").Insert(5, " ").Insert(9, " ")
              : _number.Insert(6, " ").Insert(14, " ");
        }
        catch (ArgumentOutOfRangeException)
        {
          return _number;
        }
      }
      set => _number = value.Replace(" ", "");
    }

    public string FuelCardTypeID
    {
      get { return _idFuelCardType.ToString(); }
      set { int.TryParse(value, out _idFuelCardType); }
    }

    public bool IsNoEnd
    {
      get { return _dateEnd.Year == 1; }
      set
      {
        if (value) _dateEnd = new DateTime(1, 1, 1);
      }
    }

    public DateTime DateEnd
    {
      get { return _dateEnd; }
      set
      {
        _dateEnd = new DateTime(value.Year, value.Month, 1);
        _dateEnd = _dateEnd.AddMonths(1);
        _dateEnd = _dateEnd.AddDays(-1);
      }
    }

    public string RegionID
    {
      get { return _idRegion.ToString(); }
      set { int.TryParse(value, out _idRegion); }
    }

    public string Region
    {
      get
      {
        Regions regions = Regions.getInstance();
        return regions.getItem(_idRegion);
      }
    }

    public string FuelCardType
    {
      get
      {
        FuelCardTypes fuelCardTypes = FuelCardTypes.getInstance();
        return fuelCardTypes.getItem(_idFuelCardType);
      }
    }

    public string Pin
    {
      get { return (User.IsFullAccess()) ? _pin : string.Empty; }
      set { _pin = value; }
    }

    public bool IsLost
    {
      get { return Convert.ToBoolean(_lost); }
      set { _lost = Convert.ToInt32(value); }
    }

    public bool IsVoid => (!IsNoEnd && (_dateEnd < DateTime.Today.AddMonths(1))) || IsLost;

    public string Comment { get; set; }

    public FuelCard()
    {
      ID = 0;
      _idRegion = 0;
      _idFuelCardType = 0;
    }

    public FuelCard(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);
      int.TryParse(row.ItemArray[1].ToString(), out _idFuelCardType);
      _number = row.ItemArray[2].ToString();
      DateTime.TryParse(row.ItemArray[3].ToString(), out _dateEnd);
      int.TryParse(row.ItemArray[4].ToString(), out _idRegion);
      _pin = row.ItemArray[5].ToString();
      int.TryParse(row.ItemArray[6].ToString(), out _lost);
      Comment = row.ItemArray[7].ToString();
    }

    public override void Save()
    {
      var dateEndSql = string.Empty;
      if (_dateEnd.Year != 1)
        dateEndSql = string.Concat(_dateEnd.Year.ToString(), "-", _dateEnd.Month.ToString(), "-",
          _dateEnd.Day.ToString());

      ID = Convert.ToInt32(_provider.Insert("FuelCard", ID, _idFuelCardType, _number, dateEndSql, _idRegion, _pin,
        _lost, Comment));

      FuelCardList.getInstance().Add(this);
    }

    public void AddEmptyDriver()
    {
      FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();
      if (fuelCardDriverList.getItem(this) == null)
      {
        FuelCardDriver fuelCardDriver = CreateFuelCardDriver();
        fuelCardDriver.Save();
      }
    }

    internal override void Delete()
    {
      _provider.Delete("FuelCard", ID);
    }

    internal override object[] getRow()
    {
      FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();
      FuelCardDriver fuelCardDriver = fuelCardDriverList.getItem(this);

      return fuelCardDriver.getRow();
    }

    public FuelCardDriver CreateFuelCardDriver()
    {
      if (ID == 0)
        throw new NullReferenceException();

      return new FuelCardDriver(this);
    }
  }
}
