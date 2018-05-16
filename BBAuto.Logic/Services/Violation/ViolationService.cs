using AutoMapper;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Static;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Violation
{
  public class ViolationService : IViolationService
  {
    private readonly IDbContext _dbContext;

    public ViolationService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public ViolationModel Save(ViolationModel violation)
    {
      var dbModel = Mapper.Map<DbViolation>(violation);

      var result = _dbContext.Violation.UpsertViolation(dbModel);

      return Mapper.Map<ViolationModel>(result);
    }

    public void Agree(ViolationModel violation, CarModel car)
    {
      var driverName = GetDriver(violation).GetName(NameType.Full);

      var email = new EMail();
      
      email.SendMailAccountViolation(driverName, violation.File, car);

      violation.Agreed = true;

      Save(violation);
    }

    public ViolationModel GetById(int id)
    {
      var dbModel = _dbContext.Violation.GetViolationById(id);

      return Mapper.Map<ViolationModel>(dbModel);
    }

    public Driver GetDriver(ViolationModel violation)
    {
      var driverCarList = DriverCarList.getInstance();
      var driver = driverCarList.GetDriver(violation.CarId, violation.Date);

      return driver ?? new Driver();
    }
  }
}
