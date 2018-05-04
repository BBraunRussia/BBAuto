using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Lists
{
  public class SuppyAddressList : MainList
  {
    private List<SuppyAddress> list;
    private static SuppyAddressList uniqueInstance;

    private SuppyAddressList()
    {
      list = new List<SuppyAddress>();

      LoadFromSql();
    }

    public static SuppyAddressList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new SuppyAddressList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("SuppyAddress");

      foreach (DataRow row in dt.Rows)
      {
        SuppyAddress suppyAddress = new SuppyAddress(row);
        Add(suppyAddress);
      }
    }

    public void Add(SuppyAddress suppyAddress)
    {
      if (list.Exists(item => item == suppyAddress))
        return;

      list.Add(suppyAddress);
    }

    public void Delete(int idSuppyAddress)
    {
      SuppyAddress suppyAddress = getItem(idSuppyAddress);

      list.Remove(suppyAddress);

      suppyAddress.Delete();
    }

    public SuppyAddress getItemByRegion(int idRegion)
    {
      var suppyAddresses = getListByRegion(idRegion);

      return (suppyAddresses.Count() > 0) ? suppyAddresses.First() : null;
    }

    private List<SuppyAddress> getListByRegion(int idRegion)
    {
      var suppyAddresses = list.Where(item => item.IsEqualsRegionID(idRegion));

      return suppyAddresses.ToList();
    }

    public SuppyAddress getItem(int idSuppyAddress)
    {
      var suppyAddresses = getList(idSuppyAddress);

      return (suppyAddresses.Count() > 0) ? suppyAddresses.First() : null;
    }

    private List<SuppyAddress> getList(int id)
    {
      var suppyAddresses = list.Where(item => item.Id == id);

      return suppyAddresses.ToList();
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Адрес");

      foreach (SuppyAddress suppyAddress in list.OrderBy(item => item.Region))
        dt.Rows.Add(suppyAddress.ToRow());

      return dt;
    }
  }
}
