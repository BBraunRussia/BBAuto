using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Car.Doc
{
  public class CarDocService: ICarDocService
  {
    private readonly IDbContext _dbContext;

    public CarDocService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public CarDocModel Save(CarDocModel carDoc)
    {
      var dbModel = Mapper.Map<DbCarDoc>(carDoc);

      var result = _dbContext.CarDoc.UpsertCarDoc(dbModel);

      return Mapper.Map<CarDocModel>(result);
    }

    public void Delete(int id)
    {
      _dbContext.CarDoc.DeleteCarDoc(id);
    }

    public CarDocModel GetCarDocById(int id)
    {
      var dbModel = _dbContext.CarDoc.GetCarDocById(id);
      return Mapper.Map<CarDocModel>(dbModel);
    }

    public DataTable GetDataTableByCarId(int carId)
    {
      try
      {
        var dbDocs = _dbContext.CarDoc.GetCarDocByCarId(carId);

        if (!dbDocs.Any())
          return null;

        var docs = Mapper.Map<IList<CarDocModel>>(dbDocs).ToList();

        var dt = createTable();

        docs.ForEach(doc => dt.Rows.Add(doc.ToRow()));
        
        return dt;
      }
      catch
      {
        return null;
      }
    }

    private DataTable createTable()
    {
      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Файл");

      return dt;
    }
  }
}
