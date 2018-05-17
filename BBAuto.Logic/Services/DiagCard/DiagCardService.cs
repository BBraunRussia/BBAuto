using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Logic.Services.Car;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.DiagCard
{
  public class DiagCardService : IDiagCardService
  {
    private readonly IDbContext _dbContext;

    public DiagCardService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public DiagCardModel GetByCarId(int id)
    {
      var dbModel = _dbContext.DiagCard.GetDiagCardById(id);

      return Mapper.Map<IList<DiagCardModel>>(dbModel).OrderByDescending(m => m.DateEnd).FirstOrDefault();
    }

    public void Save(DiagCardModel diagCard)
    {
      var dbModel = Mapper.Map<DbDiagCard>(diagCard);

      _dbContext.DiagCard.UpsertDiagCard(dbModel);
    }

    public void Delete(int id)
    {
      _dbContext.DiagCard.DeleteDiagCard(id);
    }

    public DataTable GetDataTable(ICarService carService)
    {
      var dbDiagCards = _dbContext.DiagCard.GetDiagCardsByDate(DateTime.Today.AddYears(-1));

      if (!dbDiagCards.Any())
        return null;

      var diagCards = Mapper.Map<IList<DiagCardModel>>(dbDiagCards)
        .OrderByDescending(item => item.DateEnd).ToList();

      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("№ ДК");
      dt.Columns.Add("Срок действия до", typeof(DateTime));

      diagCards.ForEach(diagCard => dt.Rows.Add(diagCard.ToRow(carService)));
      
      return dt;
    }

    public IList<DiagCardModel> GetDiagCardsForSend()
    {
      var dbDiagCards = _dbContext.DiagCard.GetDiagCardsForSend(DateTime.Today.AddMonths(1));

      return Mapper.Map<IList<DiagCardModel>>(dbDiagCards);
    }
  }
}
