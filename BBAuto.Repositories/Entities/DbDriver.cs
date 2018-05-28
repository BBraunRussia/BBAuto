using System;

namespace BBAuto.Repositories.Entities
{
  public class DbDriver
  {
    public int Id { get; set; }
    public string Fio { get; set; }
    public int RegionId { get; set; }
    public DateTime? DateBirth { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public bool? Fired { get; set; }
    public int? ExpSince { get; set; }
    public int PositionId { get; set; }
    public int? DeptId { get; set; }
    public string Login { get; set; }
    public int? OwnerId { get; set; }
    public string SuppyAddress { get; set; }
    public bool? Sex { get; set; }
    public bool? Decret { get; set; }
    public DateTime? DateStopNotification { get; set; }
    public string Number { get; set; }
    public bool? IsDriver { get; set; }
    public bool? From1C { get; set; }
  }
}
