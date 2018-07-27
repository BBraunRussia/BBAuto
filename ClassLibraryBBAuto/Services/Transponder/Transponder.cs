namespace BBAuto.Domain.Services.Transponder
{
  public class Transponder
  {
    public int Id { get; set; }
    public string Number { get; set; }
    public int RegionId { get; set; }
    public bool Lost { get; set; }
    public string Comment { get; set; }
  }
}
