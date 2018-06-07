using System;
using System.Windows.Forms;
using BBAuto.AddEdit;
using BBAuto.Domain.Services.Customer;

namespace BBAuto.Dictionary
{
  public partial class CustomerListForm : Form
  {
    private ICustomerService _customerService;

    public CustomerListForm()
    {
      InitializeComponent();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void CustomerListForm_Load(object sender, EventArgs e)
    {
      _customerService = new CustomerService();

      LoadData();
    }

    private void LoadData()
    {
      _dgv.DataSource = _customerService.GetCustomerList();
      _dgv.Columns[nameof(Customer.Id)].Visible = false;
      _dgv.Columns[nameof(Customer.PassportNumber)].Visible = false;
      _dgv.Columns[nameof(Customer.PassportGiveDate)].Visible = false;
      _dgv.Columns[nameof(Customer.PassportGiveOrg)].Visible = false;
      _dgv.Columns[nameof(Customer.Address)].Visible = false;

      _dgv.Columns[nameof(Customer.LastName)].HeaderText = "Фамилия";
      _dgv.Columns[nameof(Customer.FirstName)].HeaderText = "Имя";
      _dgv.Columns[nameof(Customer.SecondName)].HeaderText = "Отчество";
      _dgv.Columns[nameof(Customer.Inn)].HeaderText = "ИНН";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      ShowForm();
    }

    private void _dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int id);

      ShowForm(id);
    }

    private void ShowForm(int id = 0)
    {
      var customerForm = new CustomerForm(_customerService, id);
      if (customerForm.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int id);

      try
      {
        _customerService.DeleteCustomer(id);
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("Не удаётся удалить покупателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
