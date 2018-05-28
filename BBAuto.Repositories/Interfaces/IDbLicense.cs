using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbLicense
  {
    void DeleteLicense(int id);
    DbLicense GetLicenseById(int id);
    IList<DbLicense> GetLicenses();
    IList<DbLicense> GetLicensesByDriverId(int driverId);
    DbLicense UpsertLicense(DbLicense license);
  }
}
