using BBAuto.Repository.Interfaces;

namespace BBAuto.Repository
{
  public interface IDbContext
  {
    IDbCustomer Customer { get; }
  }
}
