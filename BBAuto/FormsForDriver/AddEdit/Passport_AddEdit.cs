using System;
using System.Windows.Forms;
using BBAuto.Domain.ForDriver;

namespace BBAuto
{
  public partial class Passport_AddEdit : Form
  {
    private readonly Passport _passport;

    private WorkWithForm _workWithForm;

    public Passport_AddEdit(Passport passport)
    {
      InitializeComponent();

      _passport = passport;
    }

    private void Passport_AddEdit_Load(object sender, EventArgs e)
    {
      fillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_passport.ID == 0);
    }

    private void fillFields()
    {
      tbFirstName.Text = _passport.FirstName;
      tbLastName.Text = _passport.LastName;
      tbSecondName.Text = _passport.SecondName;
      mtbNumber.Text = _passport.Number;
      mtbGiveDate.Text = _passport.GiveDate.ToShortDateString();
      tbGiveOrg.Text = _passport.GiveOrg;
      tbAddress.Text = _passport.Address;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _passport.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        copyFields();
        _passport.Save();
        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void copyFields()
    {
      _passport.FirstName = tbFirstName.Text;
      _passport.LastName = tbLastName.Text;
      _passport.SecondName = tbSecondName.Text;
      _passport.Number = mtbNumber.Text;
      DateTime date;
      DateTime.TryParse(mtbGiveDate.Text, out date);
      _passport.GiveDate = date;
      _passport.GiveOrg = tbGiveOrg.Text;
      _passport.Address = tbAddress.Text;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      _passport.File = tbFile.Text;
    }
  }
}
