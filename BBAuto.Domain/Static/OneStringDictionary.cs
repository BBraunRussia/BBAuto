using System.Data;
using BBAuto.Domain.DataBase;

namespace BBAuto.Domain.Static
{
  public static class OneStringDictionary
  {
    private static IProvider _provider;

    static OneStringDictionary()
    {
      _provider = Provider.GetProvider();
    }

    public static string save(string dicName, int id, string Name)
    {
      return _provider.Insert(dicName, id, Name);
    }

    public static DataTable getDataTable(string dicName)
    {
      return _provider.Select(dicName);
    }

    public static void delete(string dicName, int id)
    {
      _provider.Delete(dicName, id);
    }
  }
}
