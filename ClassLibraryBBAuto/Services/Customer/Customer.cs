using System;

namespace BBAuto.Domain.Services.Customer
{
  public class Customer
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SecondName { get; set; }
    public string PassportNumber { get; set; }
    public string PassportGiveOrg { get; set; }
    public DateTime PassportGiveDate { get; set; }
    public string Address { get; set; }
    public string Inn { get; set; }
  }
}
