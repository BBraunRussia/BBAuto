using System.Collections.Generic;
using BBAuto.Repository.Models;
using Insight.Database;

namespace BBAuto.Repository.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbCarSale
  {
    void DeleteCarSale(int id);

    DbCarSale GetCarSaleByCarId(int carId);

    IList<DbCarSale> GetCarSaleList();

    DbCarSale UpsertCarSale(DbCarSale carSale);
  }
}
