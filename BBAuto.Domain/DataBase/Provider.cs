using System;

namespace BBAuto.Domain.DataBase
{
  public static class Provider
  {
    private static IProvider _provider;

    public static void InitWebServiceProvider()
    {
      _provider = new ProviderWebService();
    }

    public static void InitSqlProvider()
    {
      _provider = new ProviderSql();
    }

    public static IProvider GetProvider()
    {
      if (_provider == null)
        throw new NullReferenceException("Провайдер не инициализирован");

      return _provider;
    }
  }
}
