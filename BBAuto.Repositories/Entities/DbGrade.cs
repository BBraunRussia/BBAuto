namespace BBAuto.Repositories.Entities
{
  public class DbGrade
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Epower { get; set; }
    public int Evol { get; set; }
    public int MaxLoad { get; set; }
    public int NoLoad { get; set; }
    public int EngineTypeId { get; set; }
    public int ModelId { get; set; }
  }
}
