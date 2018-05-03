using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;

namespace BBAuto.App.CommonForms
{
  public partial class formMyPointList : Form
  {
    private MainDgv _dgvMain;

    private MyPointList _myPointList;

    public formMyPointList()
    {
      InitializeComponent();

      _myPointList = MyPointList.getInstance();
    }

    private void formMyPointList_Load(object sender, EventArgs e)
    {
      loadRegions();

      loadData();

      _dgvMain = new MainDgv(dgv);

      ResizeDGV();
    }

    private void loadData()
    {
      if (cbRegion.SelectedValue == null)
        return;

      int idRegion;
      int.TryParse(cbRegion.SelectedValue.ToString(), out idRegion);

      dgv.DataSource = _myPointList.ToDataTable(idRegion);

      if (dgv.Columns.Count > 0)
        dgv.Columns[0].Visible = false;
    }

    private void loadRegions()
    {
      Regions regions = Regions.getInstance();
      DataTable dt = regions.ToDataTable();

      cbRegion.DataSource = dt;
      cbRegion.ValueMember = dt.Columns[0].ColumnName;
      cbRegion.DisplayMember = dt.Columns[1].ColumnName;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      int idRegion;
      int.TryParse(cbRegion.SelectedValue.ToString(), out idRegion);

      if (idRegion != 0)
        openAddEdit(new MyPoint(idRegion));
    }

    private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      openAddEdit(_myPointList.getItem(_dgvMain.GetId()));
    }

    private void openAddEdit(MyPoint myPoint)
    {
      MyPoint_AddEdit myPointAE = new MyPoint_AddEdit(myPoint);
      if (myPointAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      try
      {
        _myPointList.Delete(_dgvMain.GetId());

        loadData();
      }
      catch (NotSupportedException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void dgv_Resize(object sender, EventArgs e)
    {
      ResizeDGV();
    }

    private void ResizeDGV()
    {
      if (dgv.Columns.Count > 0)
        dgv.Columns[1].Width = dgv.Width;
    }

    private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      loadData();
    }
  }
}
