using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Tables;

namespace BBAuto.SendDocumentForms
{
  public partial class SelectRecipientStep2Form : Form
  {
    private readonly SelectRecipients _recipients;

    public SelectRecipientStep2Form(SelectRecipients recipients)
    {
      InitializeComponent();

      _recipients = recipients;
    }
    
    private void SelectRecipientStep2Form_Load(object sender, System.EventArgs e)
    {
      Text = GetHeaderText();

      chbList.DataSource = GetList();
      chbList.DisplayMember = "Name";
      chbList.ValueMember = "ID";
    }

    private object GetList()
    {
      if (_recipients == SelectRecipients.City)
        return RegionList.getInstance().GetList().Where(region => !string.IsNullOrEmpty(region.Name)).ToList();

      return DriverList.getInstance().GetList().Where(driver => driver.ID != Common.Consts.ReserveDriverId).ToList();
    }
    
    private string GetHeaderText()
    {
      return _recipients == SelectRecipients.City ? "Выбор городов" : "Выбор отдельных водителей";
    }

    private void btnNext_Click(object sender, System.EventArgs e)
    {
      if (chbList.CheckedItems.Count == 0)
      {
        MessageBox.Show("Для продолжения выберите хотя бы один элемент", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      IList<Driver> drivers;

      if (_recipients == SelectRecipients.City)
      {
        var regions = chbList.CheckedItems.Cast<Region>().ToList();

        drivers = DriverList.getInstance().GetDriversByRegionList(regions).Where(driver => driver.ID != Common.Consts.ReserveDriverId).ToList();
      }
      else
      {
        drivers = chbList.CheckedItems.Cast<Driver>().ToList();
      }
      
      var selectDocumentsForm = new SelectDocumentsForm(drivers);
      selectDocumentsForm.ShowDialog();
    }
  }
}
