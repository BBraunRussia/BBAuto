using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class formPassportList : Form
  {
    Driver driver;
    PassportList passportList;

    public formPassportList(Driver driver)
    {
      InitializeComponent();

      this.driver = driver;
      passportList = PassportList.getInstance();
    }

    private void PassportList_Load(object sender, EventArgs e)
    {
      loadData();

      SetEnable();
    }

    private void SetEnable()
    {
      btnAdd.Enabled = User.IsFullAccess();
      btnDelete.Enabled = User.IsFullAccess();
    }

    private void loadData()
    {
      _dgvPassport.DataSource = passportList.ToDataTable(driver);
      _dgvPassport.Columns[0].Visible = false;

      foreach (DataGridViewRow row in _dgvPassport.Rows)
      {
        int.TryParse(row.Cells[0].Value.ToString(), out int id);

        Passport passport = passportList.getPassport(id);

        if (passport.File != string.Empty)
          row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
      }
    }

    private void add_Click(object sender, EventArgs e)
    {
      Passport passport = driver.createPassport();

      if (openAddEditDialog(passport))
      {
        passportList.Add(passport);
        loadData();
      }
    }

    private void dgvPassport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idPassport = Convert.ToInt32(_dgvPassport.Rows[_dgvPassport.SelectedCells[0].RowIndex].Cells[0].Value);

      Passport passport = passportList.getPassport(idPassport);

      if ((e.ColumnIndex == 1) && (passport.File != string.Empty))
        tryOpenFile(passport.File);
      else
        openAddEditDialog(passport);
    }

    private bool openAddEditDialog(Passport passport)
    {
      Passport_AddEdit passportAE = new Passport_AddEdit(passport);

      return passportAE.ShowDialog() == System.Windows.Forms.DialogResult.OK;
    }

    private void tryOpenFile(string fileName)
    {
      try
      {
        WorkWithFiles.openFile(fileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      int idPassport = 0;
      int.TryParse(_dgvPassport.Rows[_dgvPassport.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idPassport);

      passportList.Delete(idPassport);

      loadData();
    }
  }
}
