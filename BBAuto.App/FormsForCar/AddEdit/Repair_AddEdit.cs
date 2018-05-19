using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class Repair_AddEdit : Form
  {
    private readonly Repair _repair;

    private WorkWithForm _workWithForm;

    public Repair_AddEdit(Repair repair)
    {
      InitializeComponent();

      _repair = repair;
    }

    private void Repair_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_repair.Id == 0);
    }

    private void FillFields()
    {
      loadDictionary();

      cbCar.SelectedValue = _repair.CarId;
      cbRepairType.SelectedValue = _repair.RepairTypeID;
      cbServiceStantion.SelectedValue = _repair.ServiceStantionID;

      dtpDate.Value = _repair.Date;
      tbCost.Text = _repair.Cost;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _repair.File;
    }

    private void loadDictionary()
    {
      CarList carList = CarList.getInstance();
      cbCar.DataSource = carList.ToDataTable(Status.Actual);
      cbCar.ValueMember = "id";
      cbCar.DisplayMember = "Бортовой номер";

      RepairTypes repairTypes = RepairTypes.getInstance();
      cbRepairType.DataSource = repairTypes.ToDataTable();
      cbRepairType.ValueMember = "id";
      cbRepairType.DisplayMember = "Название";

      ServiceStantions serviceStantions = ServiceStantions.getInstance();
      cbServiceStantion.DataSource = serviceStantions.ToDataTable();
      cbServiceStantion.ValueMember = "id";
      cbServiceStantion.DisplayMember = "Название";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _repair.RepairTypeID = cbRepairType.SelectedValue.ToString();
        _repair.ServiceStantionID = cbServiceStantion.SelectedValue.ToString();

        _repair.Date = dtpDate.Value;
        _repair.Cost = tbCost.Text;

        TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
        _repair.File = tbFile.Text;

        _repair.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
