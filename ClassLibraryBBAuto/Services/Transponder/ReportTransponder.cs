using System.ComponentModel;

namespace BBAuto.Domain.Services.Transponder
{
  public class ReportTransponder
  {
    public int Id { get; set; }
    public int DriverId { get; set; }

    [DisplayName("Номер транспондера")]
    public string Number { get; set; }

    [DisplayName("Регион")]
    public string RegionName { get; set; }

    [DisplayName("Водитель")]
    public string DriverFio { get; set; }

    public bool Lost { get; set; }
  }
}
