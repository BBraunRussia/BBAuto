using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;

namespace BBAuto.App.Dictionary
{
  public partial class SsDtpListForm : Form, ISsDtpListForm
  {
    private readonly ISsDtpForm _ssDtpForm;

    public SsDtpListForm(ISsDtpForm ssDtpForm)
    {
      _ssDtpForm = ssDtpForm;

      InitializeComponent();
    }

    DialogResult ISsDtpListForm.ShowDialog()
    {
      return ShowDialog();
    }

    private void FormSsDTPList_Load(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      var ssDtPs = SsDTPList.getInstance();

      _dgvSsDTP.DataSource = ssDtPs.ToDataTable();
      _dgvSsDTP.Columns[0].Visible = false;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      var ssDtPs = SsDTPList.getInstance();
      var ssDtp = new SsDTP();
      
      if (_ssDtpForm.ShowDialog(ssDtp) == DialogResult.OK)
      {
        ssDtPs.Add(ssDtp);
        LoadData();
      }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      var mark = GetMark();

      SsDTPList.getInstance().Delete(mark);

      LoadData();
    }

    private void _dgvSsDTP_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      var mark = GetMark();

      var ssDtp = SsDTPList.getInstance().GetItem(mark);

      if (_ssDtpForm.ShowDialog(ssDtp) == DialogResult.OK)
        LoadData();
    }

    private int GetMark()
    {
      if (int.TryParse(_dgvSsDTP.Rows[_dgvSsDTP.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int markId))
        return markId;

      throw new NullReferenceException();
    }
  }
}
