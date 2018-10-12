using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Dictionary;

namespace BBAuto.Domain.ForCar
{
  public class SsDTP : MainDictionary
  {
    public int ServiceStantionId { get; set; }
    
    public int MarkId { get; set; }

    public SsDTP() { }

    public SsDTP(DataRow row)
    {
      if (int.TryParse(row.ItemArray[0].ToString(), out int idMark))
        MarkId = idMark;

      if (int.TryParse(row.ItemArray[1].ToString(), out int serviceStantionId))
        ServiceStantionId = serviceStantionId;
    }

    public override void Save()
    {
      _provider.Insert("ssDTP", MarkId, ServiceStantionId);
    }

    internal override void Delete()
    {
      _provider.Delete("ssDTP", MarkId);
    }

    internal override object[] getRow()
    {
      var mark = MarkList.getInstance().getItem(MarkId);
      var serviceStantion = ServiceStantions.getInstance().getItem(ServiceStantionId);

      return new object[] {MarkId, mark?.Name, serviceStantion };
    }
  }
}
