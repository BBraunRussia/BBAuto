using System;
using System.Windows.Forms;
using BBAuto.Domain.Common;

namespace BBAuto
{
  public partial class FormReport : Form
  {
    private readonly MileageReportList _mileageReportList;

    public FormReport(MileageReportList mileageReportList)
    {
      InitializeComponent();

      _mileageReportList = mileageReportList;
    }

    private void FormReport_Load(object sender, EventArgs e)
    {
      tbReport.Text = _mileageReportList.GetReportMessage();
    }

    private void btnShowReport_Click(object sender, EventArgs e)
    {
      MileageReportExcel mileageReportExcel = new MileageReportExcel(_mileageReportList);
      mileageReportExcel.Create();
    }
  }
}
