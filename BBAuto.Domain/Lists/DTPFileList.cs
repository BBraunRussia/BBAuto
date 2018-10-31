using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
  public class DTPFileList : MainList<DTPFile>
  {
    private static DTPFileList _uniqueInstance;
    
    public static DTPFileList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new DTPFileList());
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("DTPFile");

      foreach (DataRow row in dt.Rows)
      {
        DTPFile dtpFile = new DTPFile(row);
        Add(dtpFile);
      }
    }
    
    public DTPFile getItem(int id)
    {
      return _list.FirstOrDefault(f => f.ID == id);
    }

    public void Delete(int idDTPFile)
    {
      DTPFile dtpFile = getItem(idDTPFile);

      _list.Remove(dtpFile);

      dtpFile.Delete();
    }

    public DataTable ToDataTable(DTP dtp)
    {
      return createTable(_list.Where(f => f.DTP.ID == dtp.ID));
    }

    private DataTable createTable(IEnumerable<DTPFile> dtpFiles)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название файла");
      dt.Columns.Add("Просмотр скана");

      foreach (DTPFile dtpFile in dtpFiles)
        dt.Rows.Add(dtpFile.getRow());

      return dt;
    }
  }
}
