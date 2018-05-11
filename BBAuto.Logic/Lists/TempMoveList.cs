using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class TempMoveList : MainList
  {
    private List<TempMove> list;
    private static TempMoveList uniqueInstance;

    private TempMoveList()
    {
      list = new List<TempMove>();

      LoadFromSql();
    }

    public static TempMoveList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new TempMoveList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("TempMove");

      foreach (DataRow row in dt.Rows)
      {
        TempMove tempMove = new TempMove(row);
        Add(tempMove);
      }
    }

    public void Add(TempMove tempMove)
    {
      if (list.Exists(item => item == tempMove))
        return;

      list.Add(tempMove);
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("Водитель");
      dt.Columns.Add("Начало", typeof(DateTime));
      dt.Columns.Add("Окончание", typeof(DateTime));

      var tempMoves = list.OrderByDescending(item => item.DateEnd);

      foreach (TempMove tempMove in tempMoves)
        dt.Rows.Add(tempMove.ToRow());

      return dt;
    }

    public TempMove GetItem(int id)
    {
      return list.FirstOrDefault(t => t.Id == id);
    }

    internal Driver GetDriver(int carId, DateTime date)
    {
      var tempMove = list.FirstOrDefault(item => item.IsDriverCar(carId, date));
      return tempMove?.Driver;
    }
  }
}
