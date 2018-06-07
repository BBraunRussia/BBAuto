using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace BBAuto.Repository
{
  public class SqlDatabase : IDataBase
  {
    private const int Timeout = 600;
    private const string ProviderName = "System.Data.SqlClient";
    private const string Server = @"localhost";
    private const string Database = "BBAuto";
    private const bool WinAuth = true;

    private readonly string _userId;
    private readonly string _password;
    
    private SqlConnection _con;

    public SqlDatabase()
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
    
    private void Init()
    {
      var connectionStringSettings = GetConnectionStringSettings();

      try
      {
        _con = new SqlConnection(connectionStringSettings.ConnectionString);
        _con.Open();
        
      }
      catch (Exception)
      {
        // ignored
      }
    }

    public ConnectionStringSettings GetConnectionStringSettings()
    {
      SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder
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

      return new ConnectionStringSettings(Consts.Config.ConnectionName, csb.ConnectionString, ProviderName);
    }

    private String Disconnect()
    {
      try
      {
        _con.Close();
        return String.Empty;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      finally
      {
        if (_con.State != ConnectionState.Closed) _con.Close();
      }
    }

    public DataTable GetRecords(String SQL, params Object[] Params)
    {
      if (isOpenedConnection())
        return tryToGetRecords(SQL, Params);

      return null;
    }

    public string GetRecordsOne(String SQL, params Object[] Params)
    {
      if (isOpenedConnection())
        return tryGetRecordsOne(SQL, Params);

      return string.Empty;
    }
    
    private bool isOpenedConnection()
    {
      if ((_con == null) || (_con.State != ConnectionState.Open))
        _con.Open();

      return (_con != null) && (_con.State == ConnectionState.Open);
    }

    private string tryGetRecordsOne(String SQL, params Object[] Params)
    {
      DataTable dt1 = tryToGetRecords(SQL, Params);

      if ((dt1 != null) && (dt1.Rows.Count > 0))
        return dt1.Rows[0].ItemArray[0].ToString();

      return string.Empty;
    }

    private DataTable tryToGetRecords(String SQL, params Object[] Params)
    {
      DataTable Out = new DataTable();

      SqlDataAdapter sqlDataAdapter = CreateSqlDataAdapter(SQL, Params);
      sqlDataAdapter.Fill(Out);
      Disconnect();
      return Out;
    }

    private SqlDataAdapter CreateSqlDataAdapter(String SQL, params Object[] Params)
    {
      SqlCommand sqlCommand = CreateSqlCommand(SQL, Params);
      return new SqlDataAdapter(sqlCommand);
    }

    private SqlCommand CreateSqlCommand(String SQL, params Object[] Params)
    {
      SqlCommand sqlCommand = new SqlCommand(SQL, _con) {CommandTimeout = Timeout};

      for (int i = 0; i < Params.Length; i++)
        sqlCommand.Parameters.Add(GetParam(i, Params));

      return sqlCommand;
    }

    private SqlParameter GetParam(int paramIndex, params Object[] Params)
    {
      return new SqlParameter($"p{paramIndex + 1}", Params[paramIndex]);
    }
  }
}
