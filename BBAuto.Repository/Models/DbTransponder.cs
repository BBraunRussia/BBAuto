namespace BBAuto.Repository.Models
{
  public class DbTransponder
  {
    public int Id { get; set; }
    public string Number { get; set; }
    public int RegionId { get; set; }
    public string Comment { get; set; }
  }
}
