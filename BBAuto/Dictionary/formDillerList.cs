using System;
using System.Windows.Forms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Presenter;

namespace BBAuto
{
  public partial class formDillerList : Form
  {
    private DealerList dillerList;

    public formDillerList()
    {
      InitializeComponent();

      dillerList = DealerList.getInstance();
    }

    private void DillerList_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      _dgv.DataSource = dillerList.ToDataTable();
      _dgv.Columns[0].Visible = false;
      resizeDGV();
    }

    private void _dgvDiller_Resize(object sender, EventArgs e)
    {
      resizeDGV();
    }

    private void resizeDGV()
    {
      int halfSize = _dgv.Width / 2;

      _dgv.Columns[1].Width = halfSize;
      _dgv.Columns[2].Width = halfSize;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      ShowAddEdit(new Diler());
    }

    private void _dgvDiller_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDiller;
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idDiller);

      DealerList dillerList = DealerList.getInstance();
      Diler diller = dillerList.getItem(idDiller);

      ShowAddEdit(diller);
    }

    private void ShowAddEdit(Diler diller)
    {
      Dictionary_AddEdit view = new Dictionary_AddEdit("Карточка \"Дилера\"");
      DictionaryPresenter presenter = new DictionaryPresenter(view, diller);
      if (view.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int idDiller = Convert.ToInt32(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value);
      dillerList.Delete(idDiller);
    }
  }
}
