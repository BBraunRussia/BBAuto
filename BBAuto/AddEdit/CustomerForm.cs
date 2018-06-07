using System;
using System.Windows.Forms;
using BBAuto.Domain.Services.Customer;

namespace BBAuto.AddEdit
{
  public partial class CustomerForm : Form
  {
    private readonly ICustomerService _customerService;
    private readonly Customer _customer;

    private WorkWithForm _workWithForm;

    public CustomerForm(
      ICustomerService customerService,
      int id)
    {
      InitializeComponent();

      _customerService = customerService;
      _customer = id == 0
        ? new Customer()
        : _customerService.GetCustomerById(id);
    }

    private void CustomerForm_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);

      _workWithForm.SetEditMode(_customer.Id == 0);
    }

    private void FillFields()
    {
      tbFirstName.Text = _customer.FirstName;
      tbLastName.Text = _customer.LastName;
      tbSecondName.Text = _customer.SecondName;
      mtbNumber.Text = _customer.PassportNumber;
      mtbGiveDate.Text = _customer.PassportGiveDate.ToShortDateString();
      tbGiveOrg.Text = _customer.PassportGiveOrg;
      tbAddress.Text = _customer.Address;
      tbInn.Text = _customer.Inn;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        CopyFields();
        _customerService.SaveCustomer(_customer);
        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void CopyFields()
    {
      _customer.FirstName = tbFirstName.Text;
      _customer.LastName = tbLastName.Text;
      _customer.SecondName = tbSecondName.Text;
      _customer.PassportNumber = mtbNumber.Text;
      DateTime.TryParse(mtbGiveDate.Text, out DateTime date);
      _customer.PassportGiveDate = date;
      _customer.PassportGiveOrg = tbGiveOrg.Text;
      _customer.Address = tbAddress.Text;
      _customer.Inn = tbInn.Text;
    }
  }
}
