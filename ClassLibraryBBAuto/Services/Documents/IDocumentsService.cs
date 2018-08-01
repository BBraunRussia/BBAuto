using System.Collections.Generic;

namespace BBAuto.Domain.Services.Documents
{
  public interface IDocumentsService
  {
    IList<Document> GetList();
    void Save(Document document);
    void DeleteDocument(Document document);
  }
}
