using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataLayer;
using BBAuto.Domain.DataBase;

namespace BBAuto.Domain.Common
{
  public abstract class MyDictionary
  {
    protected Dictionary<int, string> dictionary;
    protected abstract void loadFromSql();
    protected IProvider provider;

    protected MyDictionary()
    {
      dictionary = new Dictionary<int, string>();

      provider = Provider.GetProvider();

      loadFromSql();
    }

    public void ReLoad()
    {
      loadFromSql();
    }

    public string getItem(int key)
    {
      return key == 0 ? "(нет данных)" : dictionary[key];
    }

    public int getItem(string value)
    {
      var items = dictionary.Where(item => item.Value == value);

      return items.Count() > 0 ? items.First().Key : 0;
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");

      foreach (var item in dictionary)
        dt.Rows.Add(item.Key, item.Value);

      return dt;
    }

    protected void clearList()
    {
      dictionary.Clear();
    }

    protected void fillList(DataTable dt)
    {
      clearList();

      foreach (DataRow row in dt.Rows)
      {
        dictionary.Add(Convert.ToInt32(row.ItemArray[0]), row.ItemArray[1].ToString());
      }
    }
  }
}
