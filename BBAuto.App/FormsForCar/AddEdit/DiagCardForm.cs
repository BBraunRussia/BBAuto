using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.DiagCard;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class DiagCardForm : Form, IDiagCardForm
  {
    private DiagCardModel _diagCard;
    private WorkWithForm _workWithForm;

    private readonly ICarService _carService;
    private readonly IDiagCardService _diagCardService;

    public DiagCardForm(
      ICarService carService,
      IDiagCardService diagCardService)
    {
      _carService = carService;
      _diagCardService = diagCardService;

      InitializeComponent();
    }

    public DialogResult ShowDialog(DiagCardModel diagCard)
    {
      _diagCard = diagCard;

      return ShowDialog();
    }

    private void DiagCard_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_diagCard.Id == 0);
    }

    private void FillFields()
    {
      tbNumber.Text = _diagCard.Number;
      dtpDate.Value = _diagCard.DateEnd;

      var tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _diagCard.File;

      var car = _carService.GetCarById(_diagCard.CarId);
      lbCarInfo.Text = car.ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _diagCard.Number = tbNumber.Text;
        _diagCard.DateEnd = dtpDate.Value.Date;

        var tbFile = ucFile.Controls["tbFile"] as TextBox;
        _diagCard.File = tbFile.Text;

        _diagCardService.Save(_diagCard);

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
