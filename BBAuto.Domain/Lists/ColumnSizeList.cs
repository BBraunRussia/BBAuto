using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Static;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
  public class ColumnSizeList : MainList<ColumnSize>
  {
    private static ColumnSizeList _uniqueInstance;
    
    public static ColumnSizeList GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new ColumnSizeList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("ColumnSize");

      foreach (DataRow row in dt.Rows)
      {
        ColumnSize columnSize = new ColumnSize(row);
        Add(columnSize);
      }
    }
    
    public ColumnSize getItem(Driver driver, Status status)
    {
      var columnSizes = from columnSize in _list
        where columnSize.IsEqualsIDs(driver, status)
        select columnSize;

      if (columnSizes.Count() > 0)
        return columnSizes.First() as ColumnSize;
      else
        return driver.CreateColumnSize(status);
    }

    public DataTable ToDataTable()
    {
      return CreateTable(_list);
    }

    private DataTable CreateTable(List<ColumnSize> columnSizes)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Номер счёта");
      dt.Columns.Add("Тип полиса");
      dt.Columns.Add("Собственник");
      dt.Columns.Add("Сумма");
      dt.Columns.Add("Согласование");
      dt.Columns.Add("Файл");

      foreach (ColumnSize columnSize in columnSizes)
        dt.Rows.Add(columnSize.getRow());

      return dt;
    }
  }
}
