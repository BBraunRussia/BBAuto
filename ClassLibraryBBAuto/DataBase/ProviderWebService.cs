using System;
using System.Data;

namespace BBAuto.Domain.DataBase
{
    public class ProviderWebService : IProvider
    {
        public DataTable Select(string tableName)
        {
            BBAutoWebService.BBAutoServiceSoapClient service = new BBAutoWebService.BBAutoServiceSoapClient();
            DataSet ds = service.GetTable(tableName);
            if ((ds.Tables.Count > 0) && (ds.Tables[0] != null))
                return ds.Tables[0];
            else
                throw new Exception("Данные не получены");
        }
        
        public string SelectOne(string tableName)
        {
            DataTable dt = Select(tableName);

            if (dt.Rows.Count > 0)
                return dt.Rows[0].ItemArray[0].ToString();
            else
                throw new Exception("Пустое значение");
        }

        public string Insert(string tableName, params object[] Params)
        {
            throw new NotImplementedException();
        }


        public void Delete(string tableName, int id)
        {
            throw new NotImplementedException();
        }


        public DataTable DoOther(string sql, params object[] Params)
        {
            throw new NotImplementedException();
        }
    }
}
