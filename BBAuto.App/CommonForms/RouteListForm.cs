using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Tables;

namespace BBAuto.App.CommonForms
{
  public partial class RouteListForm : Form, IRouteListForm
  {
    private readonly RouteList _routeList;

    private readonly IMainDgv _mainDgv;

    public RouteListForm(IMainDgv mainDgv)
    {
      _mainDgv = mainDgv;
      InitializeComponent();

      _routeList = RouteList.getInstance();
    }

    private void formRouteList_Load(object sender, EventArgs e)
    {
      LoadRegions();

      LoadPoints();

      _mainDgv.SetDgv(dgv);
    }

    private void LoadRegions()
    {
      var regions = Regions.getInstance();
      var dt = regions.ToDataTable();

      cbRegion.DataSource = dt;
      cbRegion.ValueMember = dt.Columns[0].ColumnName;
      cbRegion.DisplayMember = dt.Columns[1].ColumnName;
    }

    private void LoadPoints()
    {
      int.TryParse(cbRegion.SelectedValue.ToString(), out int idRegion);

      var myPointList = MyPointList.getInstance();
      var dt = myPointList.ToDataTable(idRegion);

      cbMyPoint1.DataSource = dt;
      cbMyPoint1.ValueMember = dt.Columns[0].ColumnName;
      cbMyPoint1.DisplayMember = dt.Columns[1].ColumnName;

      LoadData();

      ResizeDgv();
    }

    private void dgv_Resize(object sender, EventArgs e)
    {
      ResizeDgv();
    }

    private void ResizeDgv()
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

      int.TryParse(cbMyPoint1.SelectedValue.ToString(), out int idMyPoint1);
      var myPointList = MyPointList.getInstance();
      var myPoint1 = myPointList.getItem(idMyPoint1);

      OpenAddEdit(new Route(myPoint1));
    }

    private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      OpenAddEdit(_routeList.getItem(_mainDgv.GetId()));
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      _routeList.Delete(_mainDgv.GetId());

      LoadData();
    }

    private void OpenAddEdit(Route route)
    {
      int.TryParse(cbRegion.SelectedValue.ToString(), out int idRegion);

      var routeAe = new Route_AddEdit(route, idRegion);
      if (routeAe.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void LoadData()
    {
      if (cbMyPoint1.SelectedValue == null)
        return;

      int.TryParse(cbMyPoint1.SelectedValue.ToString(), out int idMyPoint1);
      var myPointList = MyPointList.getInstance();
      var myPoint1 = myPointList.getItem(idMyPoint1);

      dgv.DataSource = _routeList.ToDataTable(myPoint1);

      if (dgv.Columns.Count > 0)
        dgv.Columns[0].Visible = false;
    }

    private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadPoints();
    }

    private void cbMyPoint1_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadData();
    }
  }
}
