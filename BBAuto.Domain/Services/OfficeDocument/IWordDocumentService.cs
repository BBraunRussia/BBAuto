using System;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Services.OfficeDocument
{
  public interface IWordDocumentService
  {
    IDocument CreateProxyOnSto(Driver driver, DateTime dateBegin, DateTime dateEnd);
    IDocument CreateProxyOnSto(Car car, Invoice invoice);
    IDocument CreateActFuelCard(Car car, Invoice invoice);

    IDocument CreateContractOfSale(Car car);
    IDocument CreateTransferCarAct(Car car);
    IDocument CreateTermination(Policy policy);
    IDocument CreateExtraTermination(Policy policy);
  }
}
