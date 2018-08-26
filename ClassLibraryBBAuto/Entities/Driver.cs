using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using System;
using System.Data;

namespace BBAuto.Domain.Entities
{
  public class Driver : MainDictionary
  {
    private string _fio;
    private DateTime _dateBirth;
    private string _mobile;
    private int _fired;
    private int _expSince;
    private int _decret;
    private DateTime _dateStopNotification;
    private string _number;
    private int _isDriver;
    private int _from1C;

    public Driver()
    {
      ID = 0;
      _isDriver = 0;
      _mobile = string.Empty;
      SuppyAddress = string.Empty;
    }

    public Driver(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      ID = id;

      _fio = row.ItemArray[1].ToString();

      int.TryParse(row.ItemArray[2].ToString(), out int idRegion);
      Region = RegionList.getInstance().getItem(idRegion);

      DateTime.TryParse(row.ItemArray[3].ToString(), out _dateBirth);
      _mobile = row.ItemArray[4].ToString();
      Email = row.ItemArray[5].ToString();
      int.TryParse(row.ItemArray[6].ToString(), out _fired);
      int.TryParse(row.ItemArray[7].ToString(), out _expSince);

      int.TryParse(row.ItemArray[8].ToString(), out int idPosition);
      PositionID = idPosition;

      int.TryParse(row.ItemArray[9].ToString(), out int idDept);
      DeptID = idDept;

      Login = row.ItemArray[10].ToString();

      int.TryParse(row.ItemArray[11].ToString(), out int idOwner);
      OwnerID = idOwner;

      SuppyAddress = row.ItemArray[12].ToString();

      int.TryParse(row.ItemArray[13].ToString(), out int idSex);
      SexIndex = idSex;

      int.TryParse(row.ItemArray[14].ToString(), out _decret);
      DateTime.TryParse(row.ItemArray[15].ToString(), out _dateStopNotification);
      _number = row.ItemArray[16].ToString();
      int.TryParse(row.ItemArray[17].ToString(), out _isDriver);
      int.TryParse(row.ItemArray[18].ToString(), out _from1C);
    }

    public string Name => GetName(NameType.Short);

    public string Email { get; set; }
    public string SuppyAddress { get; set; }

    public string Fio
    {
      set => _fio = value.Trim();
    }

    public string DateBirth
    {
      get => _dateBirth.Year == 1 ? string.Empty : _dateBirth.ToShortDateString();
      set
      {
        DateTime.TryParse(value, out DateTime date);
        _dateBirth = date;
      }
    }

    public string Mobile
    {
      get
      {
        if ((_mobile == null) || (_mobile == string.Empty))
          return "(нет данных)";
        else
        {
          if (_mobile.Length == 10)
            return string.Concat("+7 (", _mobile.Substring(0, 3), ") ", _mobile.Substring(3, 3), "-",
              _mobile.Substring(6, 2), "-", _mobile.Substring(8, 2));
          else
            return _mobile;
        }
      }
      set { _mobile = value.Replace("+7", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""); }
    }

    public bool Fired
    {
      get { return Convert.ToBoolean(_fired); }
      set { _fired = Convert.ToInt32(value); }
    }

    public bool Decret
    {
      get { return Convert.ToBoolean(_decret); }
      set { _decret = Convert.ToInt32(value); }
    }

    public string ExpSince
    {
      get { return _expSince == 0 ? string.Empty : _expSince.ToString(); }
      set { int.TryParse(value, out _expSince); }
    }

    public int PositionID { get; set; }

    public string Position
    {
      get
      {
        Positions positions = Positions.getInstance();
        return positions.getItem(PositionID);
      }
      set
      {
        Positions positions = Positions.getInstance();
        PositionID = positions.getItem(value);

        if (PositionID == 0)
        {
          OneStringDictionary.save("Position", 0, value);
          positions.ReLoad();
          PositionID = positions.getItem(value);
        }
      }
    }

    public int DeptID { get; set; }

    public string Dept
    {
      get
      {
        Depts depts = Depts.getInstance();
        return depts.getItem(DeptID);
      }
      set
      {
        Depts depts = Depts.getInstance();
        DeptID = depts.getItem(value);

        if (DeptID == 0)
        {
          OneStringDictionary.save("Dept", 0, value);
          depts.ReLoad();
          DeptID = depts.getItem(value);
        }
      }
    }

    public int OwnerID { get; set; }

    public string CompanyName
    {
      get
      {
        Owners owners = Owners.getInstance();
        return owners.getItem(OwnerID);
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          Owners owners = Owners.getInstance();
          OwnerID = owners.getItem(value);

          if (OwnerID == 0)
          {
            OneStringDictionary.save("Owner", 0, value);
            owners.ReLoad();
            OwnerID = owners.getItem(value);
          }
        }
      }
    }

    public string Login { get; set; }

    public RolesList UserRole
    {
      get
      {
        UserAccessList userAccessList = UserAccessList.getInstance();
        UserAccess userAccess = userAccessList.getItem(ID);

        return (RolesList) userAccess.RoleID;
      }
    }

    public bool IsOne
    {
      get
      {
        DriverList driverList = DriverList.getInstance();
        return driverList.CountDriversInRegion(Region) == 1;
      }
    }

    public int SexIndex { get; set; }

    public string Sex
    {
      get { return SexIndex == 0 ? "мужской" : "женский"; }
      set { SexIndex = (value == "Мужской") ? 0 : 1; }
    }

    public Region Region { get; set; }

    public bool IsDriver
    {
      get { return Convert.ToBoolean(_isDriver); }
      set { _isDriver = Convert.ToInt32(value); }
    }

    public bool From1C
    {
      get { return Convert.ToBoolean(_from1C); }
      set { _from1C = Convert.ToInt32(value); }
    }

    private string Status
    {
      get
      {
        return (Fired)
          ? "Уволенный"
          : (Decret)
            ? "В декрете"
            : ((OwnerID < 3) && string.IsNullOrEmpty(_number))
              ? "нет табельного"
              : "";
      }
    }

    /*TODO: проверять включена ли рассылка */
    public bool NotificationStop => _dateStopNotification.Year != 1 && _dateStopNotification > DateTime.Today;

    public DateTime DateStopNotification
    {
      get => _dateStopNotification;
      set => _dateStopNotification = value;
    }

    public string Number
    {
      get => _number;
      set
      {
        _number = value;
        if ((!string.IsNullOrEmpty(value)) && (OwnerID < 3))
          From1C = true;
      }
    }
    
    public override void Save()
    {
      DriverList driverList = DriverList.getInstance();

      string dateBirthSql = string.Empty;
      if (DateBirth != string.Empty)
        dateBirthSql = string.Concat(_dateBirth.Year.ToString(), "-", _dateBirth.Month.ToString(), "-",
          _dateBirth.Day.ToString());

      string dateStopNotificationSql = string.Empty;
      if (DateStopNotification.Year != 1)
        dateStopNotificationSql = string.Concat(DateStopNotification.Year.ToString(), "-",
          DateStopNotification.Month.ToString(), "-", DateStopNotification.Day.ToString());

      int id;
      int.TryParse(_provider.Insert("Driver", ID, GetName(NameType.Full), Region.ID, dateBirthSql, _mobile, Email,
        _fired, _expSince, PositionID,
        DeptID, Login, OwnerID, SuppyAddress, SexIndex, _decret,
        dateStopNotificationSql, _number, _isDriver, _from1C), out id);
      ID = id;

      driverList.ReLoad();
    }

    public MedicalCert createMedicalCert()
    {
      return new MedicalCert(this);
    }

    public Passport createPassport()
    {
      return new Passport(this);
    }

    public DriverLicense createDriverLicense()
    {
      return new DriverLicense(this);
    }

    public ColumnSize CreateColumnSize(Status status)
    {
      return new ColumnSize(ID, status);
    }

    internal override object[] getRow()
    {
      MedicalCertList medicalCertList = MedicalCertList.getInstance();
      MedicalCert medicalCert = medicalCertList.getItem(this);
      string medicalCertStatus = ((medicalCert == null) || (!medicalCert.IsActual())) ? "нет" : "есть";

      LicenseList licenseList = LicenseList.getInstance();
      DriverLicense license = licenseList.getItem(this);
      string licenseStatus = ((license == null) || (!license.IsActual())) ? "нет" : "есть";

      DriverCarList driverCarList = DriverCarList.getInstance();
      Car car = driverCarList.GetCar(this);

      return new object[]
      {
        ID,
        0,
        GetName(NameType.Full),
        licenseStatus,
        medicalCertStatus,
        car?.ToString() ?? "нет автомобиля",
        Region.Name,
        CompanyName,
        Status
      };
    }

    public string GetName(NameType nameType)
    {
      if (string.IsNullOrEmpty(_fio))
        return "(нет водителя)";

      switch (nameType)
      {
        case NameType.Short:
          return NameHelper.GetNameShort(_fio);
        case NameType.Genetive:
          return NameHelper.GetNameGenetive(_fio, Sex);
        case NameType.Full:
          return _fio;
        default:
          throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
      }
    }
  }
}
