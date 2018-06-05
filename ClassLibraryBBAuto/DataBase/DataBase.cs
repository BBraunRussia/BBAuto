using System;
using DataLayer;

namespace BBAuto.Domain.DataBase
{
  public static class DataBase
  {
    private static IDataBase _database;

    public static void InitDataBase()
    {
      _database = new SQL();
    }

    public static void InitMockDataBase()
    {
      _database = new MockDataBase();
    }

    public static IDataBase GetDataBase()
    {
      if (_database == null)
        throw new NullReferenceException("База данных не инициализирована");

      return _database;
    }
  }

  public static class Provider
  {
    private static IProvider _provider;

    public static void InitWebServiceProvider()
    {
      _provider = new ProviderWebService();
    }

    public static void InitSQLProvider()
    {
      _provider = new ProviderSQL();
    }

    public static void InitMockProvider()
    {
      _provider = new MockProvider();
    }

    public static IProvider GetProvider()
    {
      if (_provider == null)
        throw new NullReferenceException("Провайдер не инициализирован");
      else
        return _provider;
    }
  }
}
