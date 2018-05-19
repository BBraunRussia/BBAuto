using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class ShipPart_AddEdit : Form
  {
    private readonly ShipPart _shipPart;

    private WorkWithForm _workWithForm;

    public ShipPart_AddEdit(ShipPart shipPart)
    {
      InitializeComponent();

      _shipPart = shipPart;
    }

    private void ShipPart_AddEdit_Load(object sender, EventArgs e)
    {
      loadDictionary();

      loadData();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_shipPart.Id == 0);
    }

    private void loadDictionary()
    {
      CarList carList = CarList.getInstance();
      cbCar.DataSource = carList.ToDataTable(Status.All);
      cbCar.ValueMember = "idCar";
      cbCar.DisplayMember = "Регистрационный знак";

      DriverList driverList = DriverList.getInstance();
      cbDriver.DataSource = driverList.ToDataTable();
      cbDriver.ValueMember = "id";
      cbDriver.DisplayMember = "ФИО";
    }

    private void loadData()
    {
      cbCar.SelectedValue = _shipPart.CarId;
      cbDriver.SelectedValue = _shipPart.Driver.Id;
      tbNumber.Text = _shipPart.Number;
      mtbDateRequest.Text = _shipPart.DateRequest;
      mtbDateSent.Text = _shipPart.DateSent;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _shipPart.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _shipPart.CarId = Convert.ToInt32(cbCar.SelectedValue);
        _shipPart.Driver = DriverList.getInstance().getItem(Convert.ToInt32(cbDriver.SelectedValue));
        _shipPart.Number = tbNumber.Text;
        _shipPart.DateRequest = mtbDateRequest.Text;
        _shipPart.DateSent = mtbDateSent.Text;

        TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
        _shipPart.File = tbFile.Text;

        _shipPart.Save();
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
