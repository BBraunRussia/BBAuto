using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Lists;

namespace BBAuto.SendDocumentForms
{
  public partial class SelectRecipientStep1Form : Form
  {
    public SelectRecipientStep1Form()
    {
      InitializeComponent();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (rbAll.Checked)
      {
        var drivers = DriverList.getInstance().GetList().Where(driver => driver.ID != Common.Consts.ReserveDriverId).ToList();
        var selectDocumentForm = new SelectDocumentsForm(drivers);
        selectDocumentForm.ShowDialog();
      }
      else
      {
        ShowStep2(rbCity.Checked ? SelectRecipients.City : SelectRecipients.Drivers);
      }
    }

    private void ShowStep2(SelectRecipients recipients)
    {
      var formStep2 = new SelectRecipientStep2Form(recipients);

      formStep2.ShowDialog();
    }
  }
}
