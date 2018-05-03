using System;
using System.Windows.Forms;
using BBAuto.Logic.Services.Car.Sale;

namespace BBAuto.App.AddEdit
{
  public partial class SaleCarForm : Form, ISaleCarForm
  {
    private SaleCarModel _saleCar;
    private readonly ISaleCarService _saleCarService;

    public SaleCarForm(ISaleCarService saleCarService)
    {
      _saleCarService = saleCarService;
    }

    public DialogResult ShowDialog(SaleCarModel saleCar)
    {
      _saleCar = saleCar;

      return ShowDialog();
    }

    private void Car_Sale_Load(object sender, EventArgs e)
    {
      if (_saleCar.Date.HasValue)
      {
        dtpDate.Value = _saleCar.Date.Value;
        chbSale.Checked = true;
      }
      else
        chbSale.Checked = false;

      ChangeVisible();
      tbComm.Text = _saleCar.Comment;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      _saleCar.Date = dtpDate.Value.Date;
      _saleCar.Comment = tbComm.Text;

      _saleCarService.Save(_saleCar);
    }

    private void chbSale_CheckedChanged(object sender, EventArgs e)
    {
      ChangeVisible();
    }

    private void ChangeVisible()
    {
      dtpDate.Visible = chbSale.Checked;
    }
  }
}
