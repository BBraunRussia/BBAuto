namespace BBAuto.Repository.Models
{
  public class DbReportTransponder
  {
    public int Id { get; set; }
    public int DriverId { get; set; }
    public string Number { get; set; }
    public string RegionName { get; set; }
    public string DriverFio { get; set; }
    public bool Lost { get; set; }
  }
}
