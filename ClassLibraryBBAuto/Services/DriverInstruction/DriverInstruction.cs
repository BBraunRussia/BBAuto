using System;
using System.ComponentModel;
using BBAuto.Domain.Services.Documents;

namespace BBAuto.Domain.Services.DriverInstruction
{
  public class DriverInstruction
  {
    private DriverInstruction() { }

    public DriverInstruction(int driverId)
    {
      DriverId = driverId;
      Date = DateTime.Today;
    }

    [Browsable(false)]
    public int Id { get; set; }
    [Browsable(false)]
    public int DocumentId { get; set; }
    [Browsable(false)]
    public int DriverId { get; set; }

    [DisplayName("��������")]
    public string DocumentName
    {
      get
      {
        IDocumentsService documentsService = new DocumentsService();
        return documentsService.GetDocumentById(DocumentId).Name;
      }
    }

    [DisplayName("����")]
    public DateTime Date { get; set; }
  }
}
