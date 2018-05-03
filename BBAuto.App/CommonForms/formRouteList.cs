using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.App.GUI;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;

namespace BBAuto.App.CommonForms
{
  public partial class formRouteList : Form
  {
    private MainDgv _dgvMain;

    private RouteList _routeList;

    public formRouteList()
    {
      InitializeComponent();

      _routeList = RouteList.getInstance();
    }

    private void formRouteList_Load(object sender, EventArgs e)
    {
      loadRegions();

      loadPoints();

      _dgvMain = new MainDgv(dgv);
    }

    private void loadRegions()
    {
      Regions regions = Regions.getInstance();
      DataTable dt = regions.ToDataTable();

      cbRegion.DataSource = dt;
      cbRegion.ValueMember = dt.Columns[0].ColumnName;
      cbRegion.DisplayMember = dt.Columns[1].ColumnName;
    }

    private void loadPoints()
    {
      int idRegion;
      int.TryParse(cbRegion.SelectedValue.ToString(), out idRegion);

      MyPointList myPointList = MyPointList.getInstance();
      DataTable dt = myPointList.ToDataTable(idRegion);

      cbMyPoint1.DataSource = dt;
      cbMyPoint1.ValueMember = dt.Columns[0].ColumnName;
      cbMyPoint1.DisplayMember = dt.Columns[1].ColumnName;

      loadData();

      ResizeDGV();
    }

    private void dgv_Resize(object sender, EventArgs e)
    {
      ResizeDGV();
    }

    private void ResizeDGV()
    {
      if (dgv.Columns.Count > 0)
      {
        dgv.Columns[1].Width = Convert.ToInt32(dgv.Width * 0.8);
        dgv.Columns[2].Width = Convert.ToInt32(dgv.Width * 0.2);
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (cbMyPoint1.SelectedValue == null)
      {
        return;
      }

      int idMyPoint1;
      int.TryParse(cbMyPoint1.SelectedValue.ToString(), out idMyPoint1);
      MyPointList myPointList = MyPointList.getInstance();
      MyPoint myPoint1 = myPointList.getItem(idMyPoint1);

      openAddEdit(new Route(myPoint1));
    }

    private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      openAddEdit(_routeList.getItem(_dgvMain.GetId()));
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      _routeList.Delete(_dgvMain.GetId());

      loadData();
    }

    private void openAddEdit(Route route)
    {
      int idRegion;
      int.TryParse(cbRegion.SelectedValue.ToString(), out idRegion);

      Route_AddEdit routeAE = new Route_AddEdit(route, idRegion);
      if (routeAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void loadData()
    {
      if (cbMyPoint1.SelectedValue == null)
        return;

      int idMyPoint1;
      int.TryParse(cbMyPoint1.SelectedValue.ToString(), out idMyPoint1);
      MyPointList myPointList = MyPointList.getInstance();
      MyPoint myPoint1 = myPointList.getItem(idMyPoint1);

      dgv.DataSource = _routeList.ToDataTable(myPoint1);

      if (dgv.Columns.Count > 0)
        dgv.Columns[0].Visible = false;
    }

    private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      loadPoints();
    }

    private void cbMyPoint1_SelectedIndexChanged(object sender, EventArgs e)
    {
      loadData();
    }
  }
}
