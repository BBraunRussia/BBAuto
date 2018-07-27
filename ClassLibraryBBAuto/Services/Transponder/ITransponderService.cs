using System.Collections.Generic;

namespace BBAuto.Domain.Services.Transponder
{
  public interface ITransponderService
  {
    IList<ReportTransponder> GetReportTransponderList();
  }
}
