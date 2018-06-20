using System;
using System.Windows.Forms;
using BBAuto.AddEdit;
using BBAuto.Domain.Services.Comp;

namespace BBAuto.Dictionary
{
  public partial class CompListForm : Form
  {
    private ICompService _compService;

    public CompListForm()
    {
      InitializeComponent();
    }

    private void CompListForm_Load(object sender, EventArgs e)
    {
      _compService = new CompService();

      LoadData();
    }

    private void LoadData()
    {
      _dgv.DataSource = _compService.GetCompList();
      _dgv.Columns[nameof(Comp.Id)].Visible = false;

      _dgv.Columns[nameof(Comp.Name)].HeaderText = "Название";
      _dgv.Columns[nameof(Comp.KaskoPaymentCount)].HeaderText = "Количество взносов КАСКО";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      ShowForm();
    }

    private void _dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int id);

      ShowForm(id);
    }

    private void ShowForm(int id = 0)
    {
      var compForm = new CompForm(_compService, id);
      if (compForm.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int id);

      try
      {
        _compService.DeleteComp(id);
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("Не удаётся удалить страховую компанию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
