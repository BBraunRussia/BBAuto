using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.OfficeDocument;
using Common;

namespace BBAuto.Print
{
  public partial class ProxyOnStoForm : Form
  {
    public ProxyOnStoForm()
    {
      InitializeComponent();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      var drivers = GetDrivers();
      if (!drivers.Any())
      {
        MessageBox.Show("Не найдены водители", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      IWordDocumentService wordDocumentService = new WordDocumentService();

      foreach (var driver in drivers)
      {
        var doc = wordDocumentService.CreateProxyOnSto(driver, dtpBeginDate.Value, dtpEndDate.Value);

        doc.Print();
      }
      DialogResult = DialogResult.OK;
    }

    private void ProxyOnStoForm_Load(object sender, EventArgs e)
    {
      dtpEndDate.Value = new DateTime(DateTime.Today.Year, 12, DateTime.DaysInMonth(DateTime.Today.Year, 12));

      var driverList = DriverList.getInstance().GetList().Where(dr => dr.ID != Consts.ReserveDriverId)
        .OrderBy(dr => dr.Name).Select(d => d.Name).ToArray();
      cbDriver.Items.Add(Consts.ValueAllForCheckBox);
      cbDriver.Items.AddRange(driverList);

      var regionList = RegionList.getInstance().GetList().Select(r => r.Name).ToArray();
      cbRegion.Items.Add(Consts.ValueAllForCheckBox);
      cbRegion.Items.AddRange(regionList);
    }

    private IList<Driver> GetDrivers()
    {
      var actualDrivers = DriverList.getInstance().GetList().Where(dr => dr.ID != Consts.ReserveDriverId).ToList();

      if (rbDriver.Checked)
      {
        return cbDriver.CheckBoxItems.FirstOrDefault(item => item.Checked)?.Text == Consts.ValueAllForCheckBox
          ? actualDrivers
          : (from item in cbDriver.CheckBoxItems
            where item.Checked
            select actualDrivers.First(driver => driver.Name == item.Text)).ToList();
      }

      var drivers = new List<Driver>();
      foreach (var item in cbRegion.CheckBoxItems)
      {
        if (item.Checked)
          drivers.AddRange(actualDrivers.Where(driver => driver.Region.Name == item.Text));
      }

      return drivers;
    }
  }
}
