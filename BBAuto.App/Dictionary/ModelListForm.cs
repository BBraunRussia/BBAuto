using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Mark;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.Dictionary
{
  public partial class ModelListForm : Form, IModelListForm
  {
    private bool _load;

    private readonly IMarkService _markService;

    public ModelListForm(IMarkService markService)
    {
      _markService = markService;
      InitializeComponent();
    }

    DialogResult IModelListForm.ShowDialog()
    {
      LoadMark();
      LoadModel();

      return ShowDialog();
    }

    private void LoadMark()
    {
      _load = false;
      cbMark.DataSource = _markService.GetMarks();
      cbMark.DisplayMember = Columns.Name;
      cbMark.ValueMember = Columns.Id;
      _load = true;
    }

    private void LoadModel()
    {
      if (_load)
      {
        int.TryParse(cbMark.SelectedValue.ToString(), out int idMark);

        var models = ModelList.getInstance();

        _dgv.DataSource = models.ToDataTable(idMark);

        _dgv.Columns[0].Visible = false;
        _dgv.Columns[1].Width = _dgv.Width;
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      var markId = Convert.ToInt32(cbMark.SelectedValue);

      var model = new Model(markId);
      var modelAddEdit = new Model_AddEdit(model);

      if (modelAddEdit.ShowDialog() == DialogResult.OK)
      {
        var models = ModelList.getInstance();
        models.Add(model);

        LoadModel();
      }
    }

    private void cbMark_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_load)
      {
        LoadModel();
      }
    }

    private void _dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.RowIndex < 0)
        return;

      var idModel = Convert.ToInt32(_dgv.Rows[e.RowIndex].Cells[0].Value);

      var models = ModelList.getInstance();

      var aeM = new Model_AddEdit(models.getItem(idModel));
      if (aeM.ShowDialog() == DialogResult.OK)
      {
        LoadModel();
      }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      var models = ModelList.getInstance();

      foreach (DataGridViewCell cell in _dgv.SelectedCells)
      {
        int.TryParse(_dgv.Rows[cell.RowIndex].Cells[0].Value.ToString(), out int idModel);

        models.Delete(idModel);
      }

      LoadModel();
    }
  }
}
