using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.Common
{
  public class DriverMails
  {
    private IMainDgv _dgvMain;

    public DriverMails(IMainDgv dgvMain)
    {
      _dgvMain = dgvMain;
    }

    public override string ToString()
    {
      List<Driver> drivers = GetDrivers();

      StringBuilder sb = new StringBuilder();

      var list = from driver in drivers
        orderby driver.GetName(NameType.Full)
        select driver.email;

      foreach (string email in list)
      {
        if (sb.ToString() != string.Empty)
          sb.Append("; ");

        sb.Append(email);
      }

      return sb.ToString();
    }

    private List<Driver> GetDrivers()
    {
      List<Driver> drivers = new List<Driver>();
      DriverCarList driverCarList = DriverCarList.getInstance();

      CarList carList = CarList.getInstance();

      foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
      {
        if (cell.Visible)
        {
          Car car = carList.getItem(_dgvMain.GetCarId(cell.RowIndex));
          Driver driver = driverCarList.GetDriver(car.Id);

          if (CanAddToList(drivers, driver.email))
            drivers.Add(driver);
        }
      }

      return drivers;
    }

    private bool CanAddToList(List<Driver> drivers, string newEmail)
    {
      if (newEmail == string.Empty)
        return false;

      List<string> addresses = drivers.Where(item => item.email == newEmail).Select(item => item.email).ToList();

      return addresses.Count() == 0;
    }
  }
}
