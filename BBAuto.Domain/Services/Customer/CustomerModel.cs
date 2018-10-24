using System;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Services.Customer
{
  public class CustomerModel
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

    public string FullName => $"{LastName} {FirstName} {SecondName}";

    public string ShortName => NameHelper.GetNameShort(FullName);
  }
}
