using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repositories;

namespace BBAuto.Logic.Services.MedicalCert
{
  public class MedicalCertService : IMedicalCertService
  {
    private readonly IDbContext _dbContext;

    public MedicalCertService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IList<MedicalCertModel> GetMedicalCertForNotification()
    {
      var list = _dbContext.MedicalCert.GetMedicalCertForNotification();

      return Mapper.Map<IList<MedicalCertModel>>(list);
    }
  }
}
