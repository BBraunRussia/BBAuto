using System.Collections.Generic;

namespace BBAuto.Domain.Services.Comp
{
  public interface ICompService
  {
    Comp GetCompById(int id);
    IList<Comp> GetCompList();
  }
}
