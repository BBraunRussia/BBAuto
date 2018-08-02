using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class DriverList : MainList
  {
    private readonly List<Driver> _list;
    private static DriverList _uniqueInstance;

    private DriverList()
    {
      _list = new List<Driver>();

      loadFromSql();
    }

    public static DriverList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new DriverList());
    }

    protected override void loadFromSql()
    {
      _list.Clear();

      var dt = _provider.Select("Driver");

      foreach (DataRow row in dt.Rows)
      {
        _list.Add(new Driver(row));
      }
    }

    public void Add(Driver driver)
    {
      if (_list.Exists(item => item == driver))
        return;

      _list.Add(driver);
    }

    public DataTable ToDataTable(bool all = false)
    {
      var tempList = (all) ? _list.ToList() : _list.Where(item => item.IsDriver);

      return CreateDataTable(tempList);
    }

    public DataTable ToDataTableNotDriver(int idOwner)
    {
      var tempList = _list.Where(item => !item.IsDriver && item.OwnerID == idOwner);

      return CreateDataTable(tempList);
    }

    public IList<Driver> GetDriversByRegionList(IList<Region> regions)
    {
      var regionIds = regions.Select(r => r.ID);

      return _list.Where(item => regionIds.Contains(item.Region.ID) && item.IsDriver).ToList();
    }

    public DataTable ToDataTableByRegion(Region region, bool all = false)
    {
      var tempList = (all)
        ? _list.Where(item => item.Region == region || item.ID == 1).ToList()
        : _list.Where(item => (item.Region == region || item.ID == 1) && item.IsDriver);

      return CreateDataTable(tempList);
    }

    private static DataTable CreateDataTable(IEnumerable<Driver> drivers)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("ФИО");
      dt.Columns.Add("Скан водительского удостоверения");
      dt.Columns.Add("Скан медицинской справки");
      dt.Columns.Add("Автомобиль");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Компания");
      dt.Columns.Add("Статус");

      foreach (var driver in drivers)
        dt.Rows.Add(driver.getRow());

      return dt;
    }

    public Driver getItem(int id)
    {
      return _list.FirstOrDefault(d => d.ID == id);
    }

    public Driver getItem(string login)
    {
      List<Driver> drivers = _list.Where(item => item.Login == login).OrderBy(item => item.ID).ToList();

      return drivers.FirstOrDefault();
    }

    public Driver getItemByNumber(string number)
    {
      List<Driver> drivers = _list.Where(item => item.Number == number.Trim()).ToList();

      return drivers.FirstOrDefault();
    }

    public Driver getItemByFIO(string fio)
    {
      List<Driver> drivers = _list.Where(item =>
        item.GetName(NameType.Short).Replace(" ", "") == fio.Replace(" ", "") && item.IsDriver).ToList();

      return drivers.FirstOrDefault();
    }

    public Driver getItemByFullFIO(string fio)
    {
      List<Driver> drivers = _list.Where(item =>
        item.GetName(NameType.Full).Replace(" ", "") == fio.Replace(" ", "") && item.IsDriver).ToList();

      return drivers.FirstOrDefault();
    }


    public List<Driver> GetDriverListByRole(RolesList role)
    {
      var userAccessList = UserAccessList.getInstance();
      var userAccesses = userAccessList.ToList(role);

      if (userAccesses != null)
      {
        List<Driver> drivers = new List<Driver>();

        foreach (UserAccess userAccess in userAccesses)
        {
          drivers.Add(getItem(userAccess.Driver.ID));
        }

        return drivers;
      }

      return null;
    }

    public IList<Driver> GetList()
    {
      return _list.Where(item => item.IsDriver && !item.Decret && !item.Fired).ToList();
    }

    internal int CountDriversInRegion(Region region)
    {
      return _list.Count(item => item.Region == region && !item.Fired);
    }

    public bool IsUniqueNumber(string number)
    {
      return _list.All(item => item.Number != number);
    }
  }
}
