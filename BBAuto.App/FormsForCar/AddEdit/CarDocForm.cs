using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Services.Car.Doc;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class CarDocForm : Form, ICarDocForm
  {
    private CarDocModel _carDoc;

    private WorkWithForm _workWithForm;

    private readonly ICarDocService _carDocService;

    public CarDocForm(ICarDocService carDocService)
    {
      _carDocService = carDocService;
      InitializeComponent();
    }

    public DialogResult ShowDialog(CarDocModel carDoc)
    {
      _carDoc = carDoc;

      return ShowDialog();
    }

    private void CarDoc_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_carDoc.Id == 0);
    }

    private void FillFields()
    {
      tbName.Text = _carDoc.Name;
      var tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _carDoc.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _carDoc.Name = tbName.Text;
        var tbFile = ucFile.Controls["tbFile"] as TextBox;
        _carDoc.File = tbFile.Text;

        _carDocService.Save(_carDoc);

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
