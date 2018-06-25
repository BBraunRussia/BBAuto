using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Services.Document
{
  public interface IWordDocumentService
  {
    WordDoc CreateProxyOnSto(Car car, Invoice invoice);
    WordDoc CreateActFuelCard(Car car, Invoice invoice);
  }
}
