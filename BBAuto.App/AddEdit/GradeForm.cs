using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Services.Grade;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.AddEdit
{
  public partial class GradeForm : Form, IGradeForm
  {
    private GradeModel _grade;

    private WorkWithForm _workWithForm;

    private readonly IGradeService _gradeService;

    public GradeForm(IGradeService gradeService)
    {
      _gradeService = gradeService;
      InitializeComponent();
    }

    public DialogResult ShowDialog(int gradeId, int modelId = 0)
    {
      _grade = _gradeService.GetById(gradeId) ?? new GradeModel(modelId);

      return ShowDialog();
    }

    private void aeGrade_Load(object sender, EventArgs e)
    {
      LoadTypeEngine();

      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_grade.Id == 0);
    }

    private void LoadTypeEngine()
    {
      cbEngineType.DataSource = OneStringDictionary.getDataTable("EngineType");
      cbEngineType.DisplayMember = "Название";
      cbEngineType.ValueMember = "engineType_id";
    }

    private void FillFields()
    {
      tbName.Text = _grade.Name;
      tbEPower.Text = _grade.Epower.ToString();
      tbEVol.Text = _grade.Evol.ToString();
      tbMaxLoad.Text = _grade.MaxLoad.ToString();
      tbNoLoad.Text = _grade.NoLoad.ToString();
      cbEngineType.SelectedValue = _grade.EngineTypeId;
    }

    private void BtnOK_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (!IsFill())
          return;

        _grade.Name = tbName.Text;

        if (int.TryParse(tbEPower.Text, out  int ePower))
          _grade.Epower = ePower;
        if (int.TryParse(tbEVol.Text, out int eVol))
          _grade.Evol = eVol;
        if (int.TryParse(tbMaxLoad.Text, out int maxLoad))
          _grade.MaxLoad = maxLoad;
        if (int.TryParse(tbNoLoad.Text, out int noLoad))
          _grade.NoLoad = noLoad;

        int.TryParse(cbEngineType.SelectedValue.ToString(), out int engineTypeId);
        _grade.EngineTypeId = engineTypeId;

        _gradeService.Save(_grade);

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private bool IsFill()
    {
      if (string.IsNullOrEmpty(tbName.Text))
      {
        MessageBox.Show(Messages.EnterName, Captions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
      if (string.IsNullOrEmpty(tbEPower.Text))
      {
        MessageBox.Show(Messages.EnterEPower, Captions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
      if (string.IsNullOrEmpty(tbEVol.Text))
      {
        MessageBox.Show(Messages.EnterEVol, Captions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
      if (string.IsNullOrEmpty(tbMaxLoad.Text))
      {
        MessageBox.Show(Messages.EnterMaxLoad, Captions.Warning, MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        return false;
      }
      if (string.IsNullOrEmpty(tbNoLoad.Text))
      {
        MessageBox.Show(Messages.EnterNoLoad, Captions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }

      return true;
    }

    private static void IsNumber(KeyPressEventArgs e)
    {
      if (!char.IsDigit(e.KeyChar))
      {
        if (e.KeyChar != (char) Keys.Back)
        {
          e.Handled = true;
        }
      }
    }

    private void TbEVol_KeyPress(object sender, KeyPressEventArgs e)
    {
      IsNumber(e);
    }

    private void TbEPower_KeyPress(object sender, KeyPressEventArgs e)
    {
      IsNumber(e);
    }

    private void TbMaxLoad_KeyPress(object sender, KeyPressEventArgs e)
    {
      IsNumber(e);
    }

    private void TbNoLoad_KeyPress(object sender, KeyPressEventArgs e)
    {
      IsNumber(e);
    }
  }
}
