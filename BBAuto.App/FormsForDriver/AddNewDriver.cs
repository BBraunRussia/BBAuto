using System;
using System.Windows.Forms;
using BBAuto.App.FormsForDriver.AddEdit;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.App.FormsForDriver
{
  public partial class AddNewDriver : Form
  {
    public AddNewDriver()
    {
      InitializeComponent();
    }

    private void AddNewDriver_Load(object sender, EventArgs e)
    {
      LoadDictionary();
    }

    private void LoadDictionary()
    {
      if (((rbBraun.Checked) || (rbGematek.Checked)) && !chbEmployeeIn1C.Checked)
      {
        DriverList driverList = DriverList.getInstance();
        cbFio.DataSource = driverList.ToDataTableNotDriver(rbBraun.Checked ? 1 : 2);
        cbFio.DisplayMember = "ФИО";
        cbFio.ValueMember = "id";
      }
      else
        cbFio.DataSource = null;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      Driver driver;

      if (cbFio.DataSource != null)
      {
        DriverList driverList = DriverList.getInstance();
        driver = driverList.getItem(Convert.ToInt32(cbFio.SelectedValue));
      }
      else
      {
        driver = new Driver();
        driver.From1C = false;
      }

      driver.OwnerID = (rbBraun.Checked) ? 1 : (rbGematek.Checked) ? 2 : 3;

      DriverForm dAE = new DriverForm(driver);
      dAE.ShowDialog();
    }

    private void CheckedChanged(object sender, EventArgs e)
    {
      chbEmployeeIn1C.Enabled = ((rbBraun.Checked) || (rbGematek.Checked));
      cbFio.Enabled = (!chbEmployeeIn1C.Checked && ((rbBraun.Checked) || (rbGematek.Checked)));
      if (rbOther.Checked)
        chbEmployeeIn1C.Checked = false;
      LoadDictionary();
    }
  }
}
