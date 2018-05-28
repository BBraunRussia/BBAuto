using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Logic.Common;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Driver.DriverCar;
using BBAuto.Logic.Static;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Violation
{
  public class ViolationService : IViolationService
  {
    private readonly IDbContext _dbContext;
    private readonly IDriverCarService _driverCarService;

    public ViolationService(
      IDbContext dbContext,
      IDriverCarService driverCarService)
    {
      _dbContext = dbContext;
      _driverCarService = driverCarService;
    }

    public ViolationModel Save(ViolationModel violation)
    {
      var dbModel = Mapper.Map<DbViolation>(violation);

      var result = _dbContext.Violation.UpsertViolation(dbModel);

      return Mapper.Map<ViolationModel>(result);
    }

    public void Agree(ViolationModel violation, CarModel car)
    {
      var driver = _driverCarService.GetDriver(violation.CarId, violation.Date);
      var driverName = driver?.GetName(NameType.Full);

      var email = new EMail();
      
      email.SendMailAccountViolation(driverName, violation.File, car);

      violation.Agreed = true;

      Save(violation);
    }

    public ViolationModel GetById(int id)
    {
      if (id == 0)
        return null;

      var dbModel = _dbContext.Violation.GetViolationById(id);

      return Mapper.Map<ViolationModel>(dbModel);
    }

    public void Delete(int id)
    {
      _dbContext.Violation.DeleteViolationById(id);
    }

    public DataTable GetDataTable(ICarService carService)
    {
      var dbList = _dbContext.Violation.GetViolations();
      var list = Mapper.Map<IList<ViolationModel>>(dbList);

      var violations = list.OrderByDescending(v => v.Date);

      return CreateTable(violations, null, carService);
    }

    public DataTable GetDataTableByCar(CarModel car)
    {
      var dbList = _dbContext.Violation.GetViolationsByCarId(car.Id);
      var list = Mapper.Map<IList<ViolationModel>>(dbList);

      var violations = list.OrderByDescending(v => v.Date);

      return CreateTable(violations, car, null);
    }

    private static DataTable CreateTable(IEnumerable<ViolationModel> violations, CarModel car, ICarService carService)
    {
      var violationList = violations.OrderByDescending(v => v.Date).ToList();

      var dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("Бортовой номер");
      dt.Columns.Add("Регистрационный знак");
      dt.Columns.Add("Регион");
      dt.Columns.Add("Дата", typeof(DateTime));
      dt.Columns.Add("Водитель");
      dt.Columns.Add("№ постановления");
      dt.Columns.Add("Дата оплаты", typeof(DateTime));
      dt.Columns.Add("Тип нарушения");
      dt.Columns.Add("Сумма штрафа", typeof(int));

      violationList.ForEach(v => dt.Rows.Add(v.GetRow(car ?? carService?.GetCarById(v.CarId))));

      return dt;
    }
  }
}
