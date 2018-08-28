using BBAuto.Domain.ForCar;
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
  public partial class Mileage_AddEdit : Form
  {
    private Mileage _mileage;

    private WorkWithForm _workWithForm;

    public Mileage_AddEdit(Mileage mileage)
    {
      InitializeComponent();

      _mileage = mileage;
    }

    private void Mileage_AddEdit_Load(object sender, EventArgs e)
    {
      fillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_mileage.ID == 0);
    }

    private void fillFields()
    {
      dtpDate.Value = _mileage.Date;
      tbCount.Text = _mileage.CountString;

      lbPrevMileage.Text = _mileage.PrevToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _mileage.Date = dtpDate.Value.Date;

        if (trySetCount())
          _mileage.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private bool trySetCount()
    {
      try
      {
        _mileage.SetCount(tbCount.Text);
        return true;
      }
      catch (InvalidCastException)
      {
        MessageBox.Show("Значение поля пробег не является числом", "Ошибка", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }
      catch (InvalidConstraintException)
      {
        MessageBox.Show("Новое значение поля пробег, должно быть больше предыдущего", "Ошибка", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }
      catch (OverflowException)
      {
        MessageBox.Show("Значение поля пробег не должно превышать 1000000 км", "Ошибка", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }

      return false;
    }

    private void tbCount_TextChanged(object sender, EventArgs e)
    {
      //tbCount.Text = MyString.GetFormatedDigitInteger(tbCount.Text);
      //tbCount.SelectionStart = tbCount.Text.Length;
    }
  }
}
