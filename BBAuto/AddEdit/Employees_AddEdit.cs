using System;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.Static;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Lists;

namespace BBAuto
{
  public partial class Employees_AddEdit : Form
  {
    private readonly Employees _employees;

    private WorkWithForm _workWithForm;

    public Employees_AddEdit(Employees employees)
    {
      InitializeComponent();

      _employees = employees;
    }

    private void aeEmployees_Load(object sender, EventArgs e)
    {
      LoadDictionaries();

      LoadData();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += EnableIfAccountWayBill;
      _workWithForm.SetEditMode(_employees.ID == 0);
    }

    private void EnableIfAccountWayBill(Object sender, EditModeEventArgs e)
    {
      if (!User.IsFullAccess())
        _workWithForm.SetEnableValue(btnSave, User.GetRole() == RolesList.AccountantWayBill);
    }

    private void LoadDictionaries()
    {
      cbRegion.DataSource = Regions.getInstance().ToDataTable();
      cbRegion.ValueMember = "id";
      cbRegion.DisplayMember = "Название";

      cbEmployeesName.DataSource = EmployeesNames.getInstance().ToDataTable();
      cbEmployeesName.ValueMember = "id";
      cbEmployeesName.DisplayMember = "Название";

      cbDriver.DataSource = DriverList.getInstance().ToDataTable(true);
      cbDriver.ValueMember = "id";
      cbDriver.DisplayMember = "ФИО";
    }

    private void LoadData()
    {
      cbRegion.SelectedValue = _employees.Region.ID;
      cbEmployeesName.SelectedValue = _employees.EmployeesNameId;
      cbDriver.SelectedValue = _employees.DriverId;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        int.TryParse(cbRegion.SelectedValue.ToString(), out int regionId);
        _employees.Region = RegionList.getInstance().getItem(regionId);

        if (int.TryParse(cbEmployeesName.SelectedValue.ToString(), out int employeesNameId))
          _employees.EmployeesNameId = employeesNameId;

        if (int.TryParse(cbDriver.SelectedValue.ToString(), out int driverId))
          _employees.DriverId = driverId;

        _employees.Save();

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
