using System;
using BBAuto.Repository;

namespace BBAuto.Domain.DataBase
{
  public static class DataBase
  {
    private static IDataBase _database;

    public static void InitDataBase()
    {
      _database = new SqlDatabase();
    }
    
    public static IDataBase GetDataBase()
    {
      if (_database == null)
        throw new NullReferenceException("База данных не инициализирована");

      return _database;
    }
  }
}
