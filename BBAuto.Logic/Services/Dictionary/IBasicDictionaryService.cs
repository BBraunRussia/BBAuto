using System.Collections.Generic;

namespace BBAuto.Logic.Services.Dictionary
{
  public interface IBasicDictionaryService
  {
    Dictionary<int, string> GetItems();
    KeyValuePair<int, string> GetItemById(int id);
    void Delete(int id);
    void Save(int id, string name);
  }
}
