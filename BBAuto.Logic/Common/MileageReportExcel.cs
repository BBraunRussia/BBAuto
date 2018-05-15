using BBAuto.Logic.Services.Documents.Office;

namespace BBAuto.Logic.Common
{
  public class MileageReportExcel
  {
    private readonly MileageReportList _mileageReportList;

    public MileageReportExcel(MileageReportList mileageReportList)
    {
      _mileageReportList = mileageReportList;
    }

    public void Create()
    {
      var doc = new ExcelDoc();

      try
      {
        var i = 1;

        foreach (MileageReport item in _mileageReportList)
        {
          if (item.IsFailed)
          {
            doc.SetValue(i, 1, item.ToString());
            i++;
          }
        }

        doc.SetList(2);

        i = 1;

        foreach (MileageReport item in _mileageReportList)
        {
          if (!item.IsFailed)
          {
            doc.SetValue(i, 1, item.ToString());
            i++;
          }
        }

        doc.Show();
      }

      catch
      {
        doc.Dispose();
      }
    }
  }
}
