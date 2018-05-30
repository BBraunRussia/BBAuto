using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
  public class Regions : MyDictionary
  {
    private static Regions uniqueInstance;

    public static Regions getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Regions();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Region");

      fillList(dt);
    }
  }
}
