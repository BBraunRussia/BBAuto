using System;
using System.Drawing;
using System.Windows.Forms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForDriver;

namespace BBAuto
{
  public partial class formDriversList : Form
  {
    DriverList driverList;

    private SearchInDgv _searcher;
    private MyFilter _myFilter;
    private MyStatusStrip _myStatusStrip;

    public formDriversList()
    {
      InitializeComponent();

      driverList = DriverList.getInstance();

      btnAdd.Visible = User.IsFullAccess();
      btnDelete.Visible = User.IsFullAccess();

      _searcher = new SearchInDgv(_dgvDrivers);

      _myStatusStrip = new MyStatusStrip(_dgvDrivers, statusStrip1);

      _myFilter = MyFilter.GetInstanceDrivers();
      _myFilter.Fill(_dgvDrivers, _myStatusStrip, this);
    }

    private void DriversList_Load(object sender, EventArgs e)
    {
      _dgvDrivers.DataSource = driverList.ToDataTable();
      formatDGV();

      _myFilter.tryCreateComboBox();
    }

    private void loadDrivers()
    {
      _dgvDrivers.DataSource = driverList.ToDataTable();
      formatDGV();

      _searcher.Find(tbSearch.Text);

      _myFilter.ApplyFilter();
    }

    private void formatDGV()
    {
      _dgvDrivers.Columns[0].Visible = false;
      ResizeDGV();

      foreach (DataGridViewRow row in _dgvDrivers.Rows)
      {
        int idDriver = 0;
        int.TryParse(row.Cells[0].Value.ToString(), out idDriver);

        Driver driver = driverList.getItem(idDriver);

        LicenseList licenseList = LicenseList.getInstance();
        DriverLicense license = licenseList.getItem(driver);

        MedicalCertList medicalCertList = MedicalCertList.getInstance();
        MedicalCert medicalCert = medicalCertList.getItem(driver);

        if (!license.IsActual() || !medicalCert.IsActual())
          row.DefaultCellStyle.BackColor = Color.LightYellow;

        if (driver.Fired)
          row.DefaultCellStyle.ForeColor = Color.Red;

        if (((driver.OwnerID < 3) && (string.IsNullOrEmpty(driver.Number))) || (driver.Decret))
          row.DefaultCellStyle.ForeColor = Color.Blue;

        if (driver.OwnerID > 2)
          row.DefaultCellStyle.ForeColor = BBColors.bbGreen1;
      }
    }

    private void dgvDrivers_Resize(object sender, EventArgs e)
    {
      ResizeDGV();
    }

    private void ResizeDGV()
    {
      _dgvDrivers.Columns[1].Width = Convert.ToInt32(_dgvDrivers.Width * 0.2);
      _dgvDrivers.Columns[2].Width = Convert.ToInt32(_dgvDrivers.Width * 0.1);
      _dgvDrivers.Columns[3].Width = Convert.ToInt32(_dgvDrivers.Width * 0.15);
      _dgvDrivers.Columns[4].Width = Convert.ToInt32(_dgvDrivers.Width * 0.1);
      _dgvDrivers.Columns[5].Width = Convert.ToInt32(_dgvDrivers.Width * 0.2);
      _dgvDrivers.Columns[6].Width = Convert.ToInt32(_dgvDrivers.Width * 0.15);
      _dgvDrivers.Columns[7].Width = Convert.ToInt32(_dgvDrivers.Width * 0.1);
    }

    private void add_Click(object sender, EventArgs e)
    {
      AddNewDriver addNewDriver = new AddNewDriver();
      if (addNewDriver.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadDrivers();
    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }

    private void dgvDrivers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (isCellNoHeader(e.RowIndex))
      {
        int driverID = Convert.ToInt32(_dgvDrivers.Rows[e.RowIndex].Cells[0].Value);

        Driver_AddEdit dAE = new Driver_AddEdit(driverList.getItem(driverID));
        dAE.ShowDialog();
      }
    }

    private void tbSearch_TextChanged(object sender, EventArgs e)
    {
      _searcher.Find(tbSearch.Text);
    }

    private void tbSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.SuppressKeyPress = true;
        _searcher.Find(tbSearch.Text);
      }
    }

    private void formDriversList_Activated(object sender, EventArgs e)
    {
      formatDGV();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Вы действительно хотите удалить водителя из списка?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
      {
        int idDriver;
        int.TryParse(_dgvDrivers.Rows[_dgvDrivers.CurrentCell.RowIndex].Cells[0].Value.ToString(), out idDriver);
        Driver driver = driverList.getItem(idDriver);
        DriverCarList driverCarList = DriverCarList.GetInstance();
        if (driverCarList.IsDriverHaveCar(driver))
        {
          MessageBox.Show("За водителем закреплён автомобиль, удаление невозможно", "Удаление", MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        }
        else
        {
          driver.IsDriver = false;
          driver.Save();
          loadDrivers();
        }
      }
    }

    private void _dgvDrivers_CellContextMenuStripNeeded(object sender,
      DataGridViewCellContextMenuStripNeededEventArgs e)
    {
      if ((e.RowIndex < 0) || (e.ColumnIndex < 0))
        return;

      _dgvDrivers.CurrentCell = _dgvDrivers.Rows[e.RowIndex].Cells[e.ColumnIndex];
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      _myFilter.ApplyFilter();
    }

    private void btnClearFilter_Click(object sender, EventArgs e)
    {
      _myFilter.clearComboList();
      loadDrivers();
    }
  }
}
