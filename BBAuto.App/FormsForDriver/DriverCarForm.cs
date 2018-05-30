using System;
using System.Windows.Forms;
using BBAuto.Logic.Services.Driver.DriverCar;

namespace BBAuto.App.FormsForDriver
{
  public partial class DriverCarForm : Form, IDriverCarForm
  {
    private int _driverId;

    private readonly IDriverCarService _driverCarService;

    public DriverCarForm(IDriverCarService driverCarService)
    {
      _driverCarService = driverCarService;
      InitializeComponent();
    }

    public DialogResult ShowDialog(int driverId)
    {
      _driverId = driverId;

      return ShowDialog();
    }

    private void DriverCar_Load(object sender, EventArgs e)
    {
      dgvDriverCar.DataSource = _driverCarService.GetDataTableCarsByDriverId(_driverId);
      dgvDriverCar.Columns[0].Visible = false;
      dgvDriverCar.Columns[1].Visible = false;
      dgvDriverCar.Columns[8].Visible = false;
      dgvDriverCar.Columns[9].Visible = false;
    }
  }
}
