using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Services.OfficeDocument
{
  public interface IWordDocumentService
  {
    IDocument CreateProxyOnSto(Car car, Invoice invoice);
    IDocument CreateActFuelCard(Car car, Invoice invoice);

    IDocument CreateContractOfSale(Car car);
    IDocument CreateTransferCarAct(Car car);
    IDocument CreateTermination(Policy policy);
    IDocument CreateExtraTermination(Policy policy);
  }
}
