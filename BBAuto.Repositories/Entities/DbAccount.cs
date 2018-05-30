namespace BBAuto.Repositories.Entities
{
  public class DbAccount
  {
    public int Id { get; set; }
    public string Number { get; set; }
    public bool Agreed { get; set; }
    public int PolicyTypeId { get; set; }
    public int OwnerId { get; set; }
    public int PaymentNumber { get; set; }
    public bool BusinessTrip { get; set; }
    public string File { get; set; }
  }
}
