using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Services.Mileage;
using Common.Resources;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class MileageForm : Form, IMileageForm
  {
    private MileageModel _mileage;

    private WorkWithForm _workWithForm;

    private readonly IMileageService _mileageService;

    public MileageForm(IMileageService mileageService)
    {
      _mileageService = mileageService;
      InitializeComponent();
    }

    public DialogResult ShowDialog(MileageModel mileage)
    {
      _mileage = mileage;

      return ShowDialog();
    }

    private void Mileage_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_mileage.Id == 0);
    }

    private void FillFields()
    {
      dtpDate.Value = _mileage.Date;
      tbCount.Text = _mileage.Count?.ToString();

      var prev = _mileageService.GetMileageByCarId(_mileage.CarId).OrderByDescending(m => m.Date).FirstOrDefault(m => m.Id != _mileage.Id);

      lbPrevMileage.Text = prev?.ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _mileage.Date = dtpDate.Value.Date;

        if (TrySetCount())
          _mileageService.Save(_mileage);

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private bool TrySetCount()
    {
      try
      {
        _mileage.SetCount(tbCount.Text);
        return true;
      }
      catch (InvalidCastException)
      {
        MessageBox.Show(Messages.MileageIsNotValid, Captions.Error, MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }
      catch (OverflowException)
      {
        MessageBox.Show(Messages.MileageMoreThan1000000, Captions.Error, MessageBoxButtons.OK,
          MessageBoxIcon.Error);
      }

      return false;
    }
  }
}
