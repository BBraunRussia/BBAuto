using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Services.Document
{
  public interface IWordDocumentService
  {
    IDocument CreateProxyOnSto(Car car, Invoice invoice);
    IDocument CreateActFuelCard(Car car, Invoice invoice);
    IDocument CreateContractOfSale(Car car);
  }
}
