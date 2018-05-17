using System;

namespace BBAuto.Repositories.Entities
{
  public class DbCar
  {
    public int Id { get; set; }
    public string BbNumber { get; set; }
    public string Grz { get; set; }
    public string Vin { get; set; }
    public int? Year { get; set; }
    public string Enumber { get; set; }
    public string BodyNumber { get; set; }
    public int? ColorId { get; set; }
    public int? GradeId { get; set; }
    public int? MarkId { get; set; }
    public int? ModelId { get; set; }
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
  }
}
