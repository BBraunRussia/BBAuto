using System;
using System.Data;
using System.Data.SqlClient;

namespace BBAuto.DataLayer
{
  public class Sql : IDataBase
  {
    private const int Timeout = 600;

    private const string Server = @"localhost";
    private const string Database = "BBAuto";
    //private const string Server = @"bbmru07";
    //private const string Database = "BBAuto_04042018";
    private const bool WinAuth = true;
    private readonly string _userId;
    private readonly string _password;

    private SqlConnection _con;

    public Sql()
    {
      if (Server == @"bbmru09")
      {
        _userId = "sa";
        _password = "gfdtk";
      }
      else
      {
        _userId = "RegionalR_user";
        _password = "regionalr78";
      }

      Init();
    }

    private string Init()
    {
      var csb = new SqlConnectionStringBuilder
      {
        DataSource = Server,
        InitialCatalog = Database,
        IntegratedSecurity = WinAuth
      };

      if (!WinAuth)
      {
        csb.UserID = _userId;
        csb.Password = _password;
      }
      try
      {
        _con = new SqlConnection(csb.ConnectionString);
        _con.Open();
        return string.Empty;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }

    private void Disconnect()
    {
      try
      {
        _con.Close();
      }
      finally
      {
        if (_con.State != ConnectionState.Closed) _con.Close();
      }
    }

    public DataTable GetRecords(string sql, params object[] Params)
    {
      return IsOpenedConnection() ? TryToGetRecords(sql, Params) : null;
    }

    public string GetRecordsOne(string sql, params object[] Params)
    {
      return IsOpenedConnection() ? TryGetRecordsOne(sql, Params) : string.Empty;
    }

    private bool IsOpenedConnection()
    {
      if (_con == null || _con.State != ConnectionState.Open)
        _con.Open();

      return _con != null && _con.State == ConnectionState.Open;
    }

    private string TryGetRecordsOne(string sql, params object[] Params)
    {
      var dt1 = TryToGetRecords(sql, Params);

      if (dt1 != null && dt1.Rows.Count > 0)
        return dt1.Rows[0].ItemArray[0].ToString();

      return string.Empty;
    }

    private DataTable TryToGetRecords(string sql, params object[] Params)
    {
      var Out = new DataTable();

      var sqlDataAdapter = CreateSqlDataAdapter(sql, Params);
      sqlDataAdapter.Fill(Out);
      Disconnect();
      return Out;
    }

    private SqlDataAdapter CreateSqlDataAdapter(string sql, params object[] Params)
    {
      var sqlCommand = CreateSqlCommand(sql, Params);
      return new SqlDataAdapter(sqlCommand);
    }

    private SqlCommand CreateSqlCommand(string sql, params object[] Params)
    {
      var sqlCommand = new SqlCommand(sql, _con)
      {
        CommandTimeout = Timeout
      };

      for (var i = 0; i < Params.Length; i++)
        sqlCommand.Parameters.Add(GetParam(i, Params));

      return sqlCommand;
    }

    private static SqlParameter GetParam(int paramIndex, params object[] Params)
    {
      return new SqlParameter($"p{paramIndex + 1}", Params[paramIndex]);
    }
  }
}
