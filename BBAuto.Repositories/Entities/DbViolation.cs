using System;

namespace BBAuto.Repositories.Entities
{
  public class DbViolation
  {
    public int Id { get; set; }
    public int CarId { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public string File { get; set; }
    public DateTime? DatePay { get; set; }
    public string FilePay { get; set; }
    public int ViolationTypeId { get; set; }
    public int Sum { get; set; }
    public bool Sent { get; set; }
    public bool NoDeduction { get; set; }
    public bool Agreed { get; set; }
    public DateTime DateCreate { get; set; }
  }
}
