using System.Collections.Generic;

namespace BBAuto.Logic.Services.Mark
{
  public interface IMarkService
  {
    IList<MarkModel> GetMarks();
    MarkModel GetMarkById(int markId);
  }
}
