using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.ForCar
{
  public class Grade : MainDictionary
  {
    public string EPower { get; set; }
    public string EVol { get; set; }
    public string MaxLoad { get; set; }
    public string NoLoad { get; set; }
    public Model Model { get; set; }
    public string Name { get; set; }
    public EngineType EngineType { get; set; }

    public Grade(Model model)
    {
      Id = 0;
      Name = string.Empty;
      EPower = string.Empty;
      EVol = string.Empty;
      MaxLoad = string.Empty;
      NoLoad = string.Empty;
      Model = model;

      EngineType = EngineTypeList.getInstance().getItem(1);
    }

    public Grade(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      Id = Convert.ToInt32(row.ItemArray[0]);
      Name = row.ItemArray[1].ToString();
      EPower = row.ItemArray[2].ToString();
      EVol = row.ItemArray[3].ToString();
      MaxLoad = row.ItemArray[4].ToString();
      NoLoad = row.ItemArray[5].ToString();

      int idEngineType;
      int.TryParse(row.ItemArray[6].ToString(), out idEngineType);
      EngineType = EngineTypeList.getInstance().getItem(idEngineType);

      int idModel;
      int.TryParse(row.ItemArray[7].ToString(), out idModel);
      Model = ModelList.getInstance().getItem(idModel);
    }

    internal override void Delete()
    {
      Provider.Delete("Grade", Id);
    }

    public override void Save()
    {
      Id = Convert.ToInt32(Provider.Insert("Grade", Id, Name, EPower, EVol, MaxLoad, NoLoad, EngineType.Id, Model.Id));

      GradeList gradeList = GradeList.getInstance();
      gradeList.Add(this);
    }

    internal override object[] ToRow()
    {
      return new object[] {Id, Name};
    }

    public DataTable ToDataTable()
    {
      DataTable dt = new DataTable();

      dt.Columns.Add("Название");
      dt.Columns.Add("Значение");

      dt.Rows.Add("Мощность двигателя", EPower);
      dt.Rows.Add("Объем двигателя", EVol);
      dt.Rows.Add("Разрешенная максимальная масса", MaxLoad);
      dt.Rows.Add("Масса без нагрузки", NoLoad);

      return dt;
    }
  }
}
