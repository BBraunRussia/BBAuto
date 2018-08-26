using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repository;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.Documents
{
  public class DocumentsService : IDocumentsService
  {
    private readonly IDbContext _dbContext;

    public DocumentsService()
    {
      _dbContext = new DbContext();
    }

    public IList<Document> GetList()
    {
      var dbDocuments = _dbContext.Document.GetDocumentList();

      return Mapper.Map<IList<Document>>(dbDocuments);
    }

    public void Save(Document document)
    {
      var dbDocument = Mapper.Map<DbDocument>(document);

      _dbContext.Document.UpsertDocument(dbDocument);
    }

    public void DeleteDocument(Document document)
    {
      document.Path = null;

      _dbContext.Document.DeleteDocument(document.Id);
    }

    public Document GetDocumentById(int id)
    {
      var dbDocument = _dbContext.Document.GetDocumentById(id);

      return Mapper.Map<Document>(dbDocument);
    }
  }
}
