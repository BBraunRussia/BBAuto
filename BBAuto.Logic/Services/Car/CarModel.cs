using System;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Services.Car
{
  public class CarModel
  {
    public int Id { get; private set; }
    public string Grz { get; private set; }
    public int MarkId { get; set; }
    public int ModelId { get; set; }
    public int RegionUsingId { get; set; }
    public string BbNumber { get; set; }
    public string Vin { get; set; }
    public int Year { get; set; }
    public int OwnerId { get; set; }
    public DateTime DateGet { get; set; }
    public bool IsGet { get; set; }

    public override string ToString()
    {
      var mark = MarkList.getInstance().getItem(MarkId);
      var model = ModelList.getInstance().getItem(ModelId);

      return Id == 0
        ? "нет данных"
        : string.Concat(mark.Name, " ", model.Name, " ", Grz);
    }
  }
}
