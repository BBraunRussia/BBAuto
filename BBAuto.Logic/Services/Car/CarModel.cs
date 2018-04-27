using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Services.Car
{
  public class CarModel
  {
    public int Id { get; private set; }
    public string Grz { get; private set; }
    private int MarkId { get; set; }
    private int ModelId { get; set; }



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
