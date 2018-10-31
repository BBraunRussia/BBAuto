using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Common;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
  public class SuppyAddressList : MainList<SuppyAddress>
  {
    private static SuppyAddressList _uniqueInstance;

    public static SuppyAddressList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new SuppyAddressList());
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

    public void Delete(int idSuppyAddress)
    {
      SuppyAddress suppyAddress = getItem(idSuppyAddress);

      _list.Remove(suppyAddress);

      suppyAddress.Delete();
    }

    public SuppyAddress getItemByRegion(int idRegion)
    {
      var suppyAddresses = getListByRegion(idRegion);

      return (suppyAddresses.Count() > 0) ? suppyAddresses.First() : null;
    }

    private List<SuppyAddress> getListByRegion(int idRegion)
    {
      var suppyAddresses = _list.Where(item => item.IsEqualsRegionID(idRegion));

      return suppyAddresses.ToList();
    }

    public SuppyAddress getItem(int idSuppyAddress)
    {
      var suppyAddresses = getList(idSuppyAddress);

      return (suppyAddresses.Count() > 0) ? suppyAddresses.First() : null;
    }

    private List<SuppyAddress> getList(int id)
    {
      var suppyAddresses = _list.Where(item => item.ID == id);

      return suppyAddresses.ToList();
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Адрес");

      foreach (SuppyAddress suppyAddress in _list.OrderBy(item => item.Region))
        dt.Rows.Add(suppyAddress.getRow());

      return dt;
    }
  }
}
