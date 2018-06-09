using System;
using System.Windows.Forms;
using BBAuto.Domain.Services.CarSale;
using BBAuto.Domain.Services.Customer;

namespace BBAuto.AddEdit
{
  public partial class Car_Sale : Form
  {
    private readonly CarSale _carSale;
    private readonly ICarSaleService _carSaleService;
    private readonly ICustomerService _customerService;

    public Car_Sale(int carId)
    {
      InitializeComponent();

      _carSaleService = new CarSaleService();
      _carSale = _carSaleService.GetCarSaleByCarId(carId);

      _customerService = new CustomerService();
    }

    private void Car_Sale_Load(object sender, EventArgs e)
    {
      if (_carSale.Date.HasValue)
      {
        dtpDate.Value = Convert.ToDateTime(_carSale.Date);
        chbSale.Checked = true;
      }
      else
        chbSale.Checked = false;

      changeVisible();
      tbComm.Text = _carSale.Comment;

      cbCustomer.DataSource = _customerService.GetCustomerList();
      cbCustomer.ValueMember = nameof(Customer.Id);
      cbCustomer.DisplayMember = nameof(Customer.FullName);

      cbCustomer.SelectedItem = _carSale.CustomerId;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      var date = "";
      if (chbSale.Checked)
        date = dtpDate.Value.Date.ToShortDateString();

      if (DateTime.TryParse(date, out DateTime dateSale))
        _carSale.Date = dateSale;
      else
        _carSale.Date = null;

      _carSale.Comment = tbComm.Text;

      var customer = cbCustomer.SelectedItem as Customer;
      if (customer != null)
        _carSale.CustomerId = customer.Id;

      _carSaleService.SaveCarSale(_carSale);
    }

    private void chbSale_CheckedChanged(object sender, EventArgs e)
    {
      changeVisible();
    }

    private void changeVisible()
    {
      dtpDate.Visible = chbSale.Checked;
    }
  }
}
