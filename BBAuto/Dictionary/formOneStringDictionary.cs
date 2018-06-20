using System;
using System.Windows.Forms;
using BBAuto.Domain.Static;

namespace BBAuto.Dictionary
{
  public partial class formOneStringDictionary : Form
  {
    private string _dicName;

    public formOneStringDictionary(string dicName, string header)
    {
      InitializeComponent();
      _dicName = dicName;
      this.Text = header;
    }

    private void Mark_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      _dgv.DataSource = OneStringDictionary.getDataTable(_dicName);

      _dgv.Columns[0].Visible = false;
      _dgv.Columns[1].Width = _dgv.Width;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      OneString_AddEdit ae = new OneString_AddEdit(_dicName);
      if (ae.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void _dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if ((e.ColumnIndex < 0) || (e.RowIndex < 0))
        return;

      int id = 0;
      int.TryParse(_dgv.Rows[e.RowIndex].Cells[0].Value.ToString(), out id);
      string name = _dgv.Rows[e.RowIndex].Cells[1].Value.ToString();

      OneString_AddEdit ae = new OneString_AddEdit(_dicName, id, name);
      if (ae.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      try
      {
        tryDelete();
        loadData();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void tryDelete()
    {
      foreach (DataGridViewCell cell in _dgv.SelectedCells)
      {
        int id = Convert.ToInt32(_dgv.Rows[cell.RowIndex].Cells[0].Value);

        OneStringDictionary.delete(_dicName, id);
      }
    }
  }
}
