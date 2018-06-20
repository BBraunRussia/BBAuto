using System.Collections.Generic;

namespace BBAuto.Domain.Services.Comp
{
  public interface ICompService
  {
    Comp GetCompById(int id);
    IList<Comp> GetCompList();
    Comp SaveComp(Comp comp);
    void DeleteComp(int id);
  }
}
