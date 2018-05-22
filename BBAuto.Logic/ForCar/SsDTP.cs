using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.ForCar
{
  public class SsDTP : MainDictionary
  {
    private int idServiceStantion;

    public string IDServiceStantion
    {
      get { return idServiceStantion.ToString(); }
      set { int.TryParse(value, out idServiceStantion); }
    }

    public string ServiceStantion
    {
      get { return ServiceStantions.getInstance().getItem(idServiceStantion); }
      set { int.TryParse(value, out idServiceStantion); }
    }

    public int MarkId { get; set; }

    public SsDTP()
    {
      idServiceStantion = 0;
    }

    public SsDTP(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int markId);
      MarkId = markId;

      int.TryParse(row.ItemArray[1].ToString(), out idServiceStantion);
    }

    public override void Save()
    {
      Provider.Insert("ssDTP", MarkId, idServiceStantion);
    }

    internal override void Delete()
    {
      Provider.Delete("ssDTP", MarkId);
    }

    internal override object[] ToRow()
    {
      
      return new object[] {MarkId, "Mark.Name", ServiceStantion};
    }
  }
}
