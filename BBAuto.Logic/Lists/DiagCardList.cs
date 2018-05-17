using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class DiagCardList2 : MainList
  {
    private static DiagCardList2 uniqueInstance;
    private List<DiagCard> list;

    private DiagCardList2()
    {
      list = new List<DiagCard>();

      LoadFromSql();
    }

    public static DiagCardList2 getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DiagCardList2();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("DiagCard");

      foreach (DataRow row in dt.Rows)
      {
        DiagCard diagCard = new DiagCard(row);
        Add(diagCard);
      }
    }

    public void Add(DiagCard diagCard)
    {
      if (list.Exists(item => item == diagCard))
        return;

      list.Add(diagCard);
    }

    public DataTable ToDataTable()
    {
      var diagCards = list.Where(item => item.Date >= (DateTime.Today.AddYears(-1)) && !item.Car.info.IsSale)
        .OrderByDescending(item => item.Date);

      return createTable(diagCards.ToList());
    }

    public DataTable ToDataTable(Car car)
    {
      var diagCards = list.Where(item => item.Car.Id == car.Id).OrderByDescending(item => item.Date);

      return createTable(diagCards.ToList());
    }

    private DataTable createTable(List<DiagCard> diagCards)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ ДК");
      dt.Columns.Add("Срок действия до", Type.GetType("System.DateTime"));

      foreach (DiagCard diagCard in diagCards)
        dt.Rows.Add(diagCard.ToRow());

      return dt;
    }

    public DiagCard getItem(int id)
    {
      return list.FirstOrDefault(item => item.Id == id);
    }

    public DiagCard getItem(Car car)
    {
      return list.Where(item => item.Car.Id == car.Id).OrderByDescending(item => item.Date).FirstOrDefault();
    }

    public void Delete(int idDiagCard)
    {
      DiagCard diagCard = getItem(idDiagCard);

      list.Remove(diagCard);

      diagCard.Delete();
    }

    public List<DiagCard> GetDiagCardList(DateTime date)
    {
      return list.Where(item => (item.Date.Year == date.Year && item.Date.Month == date.Month)).ToList();
    }

    internal IEnumerable<DiagCard> GetDiagCardEnds()
    {
      IEnumerable<DiagCard> list = GetDiagCardList(DateTime.Today.AddMonths(1));

      return list.Where(item => !item.IsNotificationSent && !item.Car.info.IsSale).ToList();
    }

    internal IEnumerable<Car> GetCarListFromDiagCardList(List<DiagCard> list)
    {
      return list.Select(diagCard => diagCard.Car);
    }
  }
}
