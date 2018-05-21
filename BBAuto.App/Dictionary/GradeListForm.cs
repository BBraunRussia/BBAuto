using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Grade;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.Dictionary
{
  public partial class GradeListForm : Form, IGradeListForm
  {
    private bool _load;

    private readonly IGradeService _gradeService;
    private readonly IGradeForm _gradeForm;

    public GradeListForm(
      IGradeService gradeService,
      IGradeForm gradeForm)
    {
      _gradeService = gradeService;
      _gradeForm = gradeForm;
      InitializeComponent();
    }

    DialogResult IGradeListForm.ShowDialog()
    {
      LoadMark();
      LoadModel();
      LoadGrade();

      return ShowDialog();
    }

    private void LoadMark()
    {
      _load = false;
      cbMark.DataSource = OneStringDictionary.getDataTable("Mark");
      cbMark.DisplayMember = "Название";
      cbMark.ValueMember = "mark_id";
      _load = true;
    }

    private void LoadModel()
    {
      _load = false;

      int.TryParse(cbMark.SelectedValue.ToString(), out int idMark);
      var models = ModelList.getInstance();

      cbModel.DataSource = models.ToDataTable(idMark);
      cbModel.DisplayMember = "Название";
      cbModel.ValueMember = "id";

      _load = true;
    }

    private void LoadGrade()
    {
      int.TryParse(cbModel.SelectedValue.ToString(), out int idModel);
      
      _dgv.DataSource = _gradeService.GetDataTable(idModel);
      _dgv.Columns[Columns.Id].Visible = false;
    }

    private void cbMark_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_load)
        return;

      LoadModel();
      LoadGrade();
    }

    private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_load)
        LoadGrade();
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
      var modelId = Convert.ToInt32(cbModel.SelectedValue);
      
      if (_gradeForm.ShowDialog(0, modelId) == DialogResult.OK)
      {
        LoadGrade();
      }
    }

    private void _dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.RowIndex < 0)
        return;

      var id = Convert.ToInt32(_dgv.Rows[e.RowIndex].Cells[0].Value);
      
      if (_gradeForm.ShowDialog(id) == DialogResult.OK)
      {
        LoadGrade();
      }
    }

    private void BtnDel_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewCell cell in _dgv.SelectedCells)
      {
        int.TryParse(_dgv.Rows[cell.RowIndex].Cells[0].Value.ToString(), out int gradeId);

        _gradeService.Delete(gradeId);
      }

      LoadGrade();
    }
  }
}
