using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Account
{
  public class AccountModel
  {
    public int Id { get; set; }
    public string Number { get; set; }
    public bool Agreed { get; set; }
    public int PolicyTypeId { get; set; }
    public int OwnerId { get; set; }
    public int PaymentNumber { get; set; }
    public bool BusinessTrip { get; set; }
    public string File { get; set; }

    public PolicyType PolicyType => (PolicyType) PolicyTypeId;

    public bool IsPolicyKaskoAndPayment2()
    {
      return PolicyType == PolicyType. ¿— Œ && PaymentNumber == 2;
    }
  }
}
