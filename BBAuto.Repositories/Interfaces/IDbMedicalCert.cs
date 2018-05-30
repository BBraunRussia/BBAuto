using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbMedicalCert
  {
    void DeleteMedicalCert(int id);
    IList<DbMedicalCert> GetMedicalCerts();
    IList<DbMedicalCert> GetMedicalCertForNotification();
    DbMedicalCert UpsertMedicalCert(DbMedicalCert medicalCert);
  }
}
