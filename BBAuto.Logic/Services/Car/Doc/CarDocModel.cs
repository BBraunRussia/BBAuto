namespace BBAuto.Logic.Services.Car.Doc
{
  public class CarDocModel
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public string Name { get; set; }
    public string File { get; set; }

    public object[] ToRow()
    {
      return new object[] { Id, Name, File == string.Empty ? string.Empty : "Показать" };
    }
  }
}
