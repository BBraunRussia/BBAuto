using System.Data;

namespace BBAuto.Domain.Services.Transponder
{
  public interface ITransponderService
  {
    DataTable GetReportTransponderList();
    Transponder Save(Transponder transponder);
    Transponder GetTransponder(int id);
  }
}
