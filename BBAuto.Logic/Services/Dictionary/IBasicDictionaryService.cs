using System.Collections.Generic;

namespace BBAuto.Logic.Services.Dictionary
{
  public interface IBasicDictionaryService
  {
    IList<DictionaryModel> GetItems();
    DictionaryModel GetItemById(int id);
    void Delete(int id);
    void Save(DictionaryModel model);
  }
}
