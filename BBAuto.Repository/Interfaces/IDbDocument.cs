using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDocument
  {
    IList<DbDocument> GetDocumentList();
    DbDocument GetDocumentById(int id);
    void UpsertDocument(DbDocument dbDocument);
    void DeleteDocument(int id);
  }
}
