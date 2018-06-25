using BBAuto.Domain.Services.Document;

namespace BBAuto.Domain.Common
{
  public class MileageReportExcel
  {
    private MileageReportList _mileageReportList;

    public MileageReportExcel(MileageReportList mileageReportList)
    {
      _mileageReportList = mileageReportList;
    }

    public void Create()
    {
      ExcelDoc doc = new ExcelDoc();

      try
      {
        int i = 1;

        foreach (MileageReport item in _mileageReportList)
        {
          if (item.IsFailed)
          {
            doc.setValue(i, 1, item.ToString());
            i++;
          }
        }

        doc.SetList(2);

        i = 1;

        foreach (MileageReport item in _mileageReportList)
        {
          if (!(item.IsFailed))
          {
            doc.setValue(i, 1, item.ToString());
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
