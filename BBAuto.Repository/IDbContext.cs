using BBAuto.Repository.Interfaces;

namespace BBAuto.Repository
{
  public interface IDbContext
  {
    IDbCarSale CarSale { get; }
    IDbComp Comp { get; }
    IDbCustomer Customer { get; }
    IDbTransponder Transponder { get; }
  }
}
