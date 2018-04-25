using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.Services.Dealer;
using Common;

namespace BBAuto.App.Dictionary
{
  public partial class DealerListForm : Form, IDealerListForm
  {
    private readonly IDealerService _dealerService;

    public DealerListForm(IDealerService dealerService)
    {
      _dealerService = dealerService;
      InitializeComponent();
    }

    private void DillerList_Load(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      _dgv.DataSource = _dealerService.GetDealers().ToArray();
      _dgv.Columns[0].Visible = false;
      _dgv.Columns[1].HeaderText = Columns.Name;
      _dgv.Columns[2].HeaderText = Columns.Contacts;
      
      ResizeDgv();
    }

    private void _dgvDiller_Resize(object sender, EventArgs e)
    {
      ResizeDgv();
    }

    private void ResizeDgv()
    {
      var halfSize = _dgv.Width / 2;

      _dgv.Columns[1].Width = halfSize;
      _dgv.Columns[2].Width = halfSize - 20;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      ShowDealerForm(new DealerModel());
    }

    private void _dgvDiller_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int idDiller);

      var dealer = _dealerService.GetDealer(idDiller);

      ShowDealerForm(dealer);
    }

    private void ShowDealerForm(DealerModel dealer)
    {
      var dealerForm = new DealerForm(dealer);
      if (dealerForm.ShowDialog() == DialogResult.OK)
      {
        _dealerService.Save(dealer);
        LoadData();
      }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      var idDealer = Convert.ToInt32(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value);
      _dealerService.Delete(idDealer);
    }
  }
}
