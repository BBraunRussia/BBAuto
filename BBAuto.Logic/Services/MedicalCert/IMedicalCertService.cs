using System.Collections.Generic;

namespace BBAuto.Logic.Services.MedicalCert
{
  public interface IMedicalCertService
  {
    IList<MedicalCertModel> GetMedicalCertForNotification();
  }
}
