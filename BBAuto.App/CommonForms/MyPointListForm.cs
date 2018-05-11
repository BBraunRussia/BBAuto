using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;
using Common.Resources;

namespace BBAuto.App.CommonForms
{
  public partial class MyPointListForm : Form, IMyPointListForm
  {
    private readonly MyPointList _myPointList;

    private readonly IMainDgv _mainDgv;

    public MyPointListForm(IMainDgv mainDgv)
    {
      _mainDgv = mainDgv;
      InitializeComponent();

      _myPointList = MyPointList.getInstance();
    }

    private void formMyPointList_Load(object sender, EventArgs e)
    {
      loadRegions();

      LoadData();

      _mainDgv.SetDgv(dgv);

      ResizeDgv();
    }

    private void LoadData()
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
        OpenAddEdit(new MyPoint(idRegion));
    }

    private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      OpenAddEdit(_myPointList.getItem(_mainDgv.GetId()));
    }

    private void OpenAddEdit(MyPoint myPoint)
    {
      MyPoint_AddEdit myPointAE = new MyPoint_AddEdit(myPoint);
      if (myPointAE.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      try
      {
        _myPointList.Delete(_mainDgv.GetId());

        LoadData();
      }
      catch (NotSupportedException ex)
      {
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void dgv_Resize(object sender, EventArgs e)
    {
      ResizeDgv();
    }

    private void ResizeDgv()
    {
      if (dgv.Columns.Count > 0)
        dgv.Columns[1].Width = dgv.Width;
    }

    private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadData();
    }
  }
}
