using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.Services.Dictionary;
using Common.Resources;

namespace BBAuto.App.Dictionary
{
  public partial class OneStringDictionaryListForm : Form, IOneStringDictionaryListForm
  {
    private IBasicDictionaryService _dictionaryService;
    private readonly IOneStringDictionaryForm _oneStringDictionaryForm;

    public OneStringDictionaryListForm(IOneStringDictionaryForm oneStringDictionaryForm)
    {
      _oneStringDictionaryForm = oneStringDictionaryForm;
      InitializeComponent();
    }

    public DialogResult ShowDialog(string header, IBasicDictionaryService dictionaryService)
    {
      _dictionaryService = dictionaryService;
      Text = header;

      return ShowDialog();
    }

    private void Mark_Load(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      _dgv.DataSource = _dictionaryService.GetItems();
      _dgv.Columns[0].HeaderText = Columns.Id;
      _dgv.Columns[1].HeaderText = Columns.Name;

      _dgv.Columns[0].Visible = false;
      _dgv.Columns[1].Width = _dgv.Width;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (_oneStringDictionaryForm.ShowDialog(0, _dictionaryService) == DialogResult.OK)
        LoadData();
    }

    private void _dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.RowIndex < 0)
        return;

      int.TryParse(_dgv.Rows[e.RowIndex].Cells[0].Value.ToString(), out int id);
      
      if (_oneStringDictionaryForm.ShowDialog(id, _dictionaryService) == DialogResult.OK)
        LoadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      try
      {
        TryDelete();
        LoadData();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void TryDelete()
    {
      foreach (DataGridViewCell cell in _dgv.SelectedCells)
      {
        var id = Convert.ToInt32(_dgv.Rows[cell.RowIndex].Cells[0].Value);

        _dictionaryService.Delete(id);
      }
    }
  }
}
