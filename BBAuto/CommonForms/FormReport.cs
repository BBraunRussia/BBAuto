using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Services.OfficeDocument;

namespace BBAuto
{
  public partial class FormReport : Form
  {
    private readonly IList<MileageReport> _mileageReportList;

    public FormReport(IList<MileageReport> mileageReportList)
    {
      InitializeComponent();

      _mileageReportList = mileageReportList;
    }

    private void FormReport_Load(object sender, EventArgs e)
    {
      var countFailed = _mileageReportList.Count(item => item.IsFailed);
      var countSuccess = _mileageReportList.Count - countFailed;

      tbReport.Text = $"Всего обработано файлов: {_mileageReportList.Count}. Из них пробеги удалось считать из {countSuccess}. Не удалось считать из {countFailed}";
    }

    private void btnShowReport_Click(object sender, EventArgs e)
    {
      IExcelDocumentService excelDocumentService = new ExcelDocumentService();
      var doc = excelDocumentService.CreateReportLoadMileage(_mileageReportList);
      doc.Show();
    }
  }
}
