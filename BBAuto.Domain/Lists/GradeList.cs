using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
  public class GradeList : MainList<Grade>
  {
    private static GradeList _uniqueInstance;
    
    public static GradeList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new GradeList();

      return _uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Grade");
      
      foreach (DataRow row in dt.Rows)
      {
        Grade grade = new Grade(row);
        Add(grade);
      }
    }
    
    public Grade getItem(int id)
    {
      return _list.FirstOrDefault(g => g.ID == id);
    }

    public void Delete(int idGrade)
    {
      Grade grade = getItem(idGrade);

      _list.Remove(grade);

      grade.Delete();
    }

    public DataTable ToDataTable(int idModel)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");

      foreach (Grade grade in _list.Where(g => g.Model.ID == idModel))
        dt.Rows.Add(grade.getRow());

      return dt;
    }
  }
}
