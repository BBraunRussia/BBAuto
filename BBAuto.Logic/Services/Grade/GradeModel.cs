using System;
using System.Data;
using Common.Resources;

namespace BBAuto.Logic.Services.Grade
{
  public class GradeModel
  {
    private GradeModel() { }

    public GradeModel(int modelId)
    {
      if (modelId == 0)
        throw new NullReferenceException(Messages.GradeCannotBeCreated);

      ModelId = modelId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Epower { get; set; }
    public int Evol { get; set; }
    public int MaxLoad { get; set; }
    public int NoLoad { get; set; }
    public int EngineTypeId { get; set; }
    public int ModelId { get; private set; }

    public DataTable ToDataTable()
    {
      var dt = new DataTable();

      dt.Columns.Add("��������");
      dt.Columns.Add("��������");

      dt.Rows.Add("�������� ���������", Epower);
      dt.Rows.Add("����� ���������", Evol);
      dt.Rows.Add("����������� ������������ �����", MaxLoad);
      dt.Rows.Add("����� ��� ��������", NoLoad);

      return dt;
    }

    public object[] ToRow()
    {
      return new object[] { Id, Name };
    }
  }
}
