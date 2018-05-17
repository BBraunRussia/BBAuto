using System;
using System.Data;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Services.Car
{
  public class CarModel
  {
    public int Id { get; private set; }
    public string BbNumber { get; set; }
    public string Grz { get; set; }
    public string Vin { get; set; }
    public int? Year { get; set; }
    public string ENumber { get; set; }
    public string BodyNumber { get; set; }
    public int? MarkId { get; set; }
    public int? ModelId { get; set; }
    public int? GradeId { get; set; }
    public int? ColorId { get; set; }
    public DateTime? LisingDate { get; set; }
    public string InvertoryNumber { get; set; }

    public int? OwnerId { get; set; }
    public int? RegionIdBuy { get; set; }
    public int? RegionIdUsing { get; set; }
    public int? DriverId { get; set; }
    public DateTime? DateOrder { get; set; }
    public bool IsGet { get; set; }
    public DateTime? DateGet { get; set; }
    public decimal? Cost { get; set; }
    public string Dop { get; set; }
    public string Events { get; set; }
    public int? DealerId { get; set; }

    public override string ToString()
    {
      if (!MarkId.HasValue || !ModelId.HasValue)
        return string.Empty;

      var mark = MarkList.getInstance().getItem(MarkId.Value);
      var model = ModelList.getInstance().getItem(ModelId.Value);

      return Id == 0
        ? "нет данных"
        : string.Concat(mark.Name, " ", model.Name, " ", Grz);
    }

    public DataTable ToDataTableInfo()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("Название");
      dt.Columns.Add("Значение");

      if (!MarkId.HasValue)
        return dt;

      var mark = MarkList.getInstance().getItem(MarkId.Value);
      var model = ModelList.getInstance().getItem(ModelId.Value);
      var color = Colors.GetInstance().getItem(ColorId.Value);
      var owner = Owners.getInstance().getItem(OwnerId.Value);
      var pts = PTSList.getInstance().getItem(Id);
      var sts = STSList.getInstance().getItem(Id);

      dt.Rows.Add("Марка", mark.Name);
      dt.Rows.Add("Модель", model.Name);
      dt.Rows.Add("Год выпуска", Year);
      dt.Rows.Add("Цвет", color);
      dt.Rows.Add("Собственник", owner);
      dt.Rows.Add("Дата покупки", DateGet.Value.ToShortDateString());
      dt.Rows.Add("Модель № двигателя", ENumber);
      dt.Rows.Add("№ кузова", BodyNumber);
      dt.Rows.Add("Дата выдачи ПТС:", pts.Date.ToShortDateString());
      dt.Rows.Add("Дата выдачи СТС:", sts.Date.ToShortDateString());

      return dt;
    }
  }
}
