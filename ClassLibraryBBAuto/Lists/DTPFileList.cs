using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class DTPFileList : MainList
  {
    private static DTPFileList uniqueInstance;
    private List<DTPFile> list;

    private DTPFileList()
    {
      list = new List<DTPFile>();

      LoadFromSql();
    }

    public static DTPFileList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DTPFileList();

      return uniqueInstance;
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

    public void Add(DTPFile dtpFile)
    {
      if (list.Exists(item => item.Id == dtpFile.Id))
        return;

      list.Add(dtpFile);
    }

    public DTPFile getItem(int id)
    {
      return list.FirstOrDefault(f => f.Id == id);
    }

    public void Delete(int idDTPFile)
    {
      DTPFile dtpFile = getItem(idDTPFile);

      list.Remove(dtpFile);

      dtpFile.Delete();
    }

    public DataTable ToDataTable(DTP dtp)
    {
      return createTable(list.Where(f => f.DTP.Id == dtp.Id));
    }

    private DataTable createTable(IEnumerable<DTPFile> dtpFiles)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название файла");
      dt.Columns.Add("Просмотр скана");

      foreach (DTPFile dtpFile in dtpFiles)
        dt.Rows.Add(dtpFile.GetRow());

      return dt;
    }
  }
}
