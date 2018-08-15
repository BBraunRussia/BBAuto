using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;

namespace BBAuto
{
  public class DriverMails
  {
    private readonly MainDGV _dgvMain;

    public DriverMails(MainDGV dgvMain)
    {
      _dgvMain = dgvMain;
    }

    public override string ToString()
    {
      List<Driver> drivers = GetDrivers();

      StringBuilder sb = new StringBuilder();

      var list = from driver in drivers
        orderby driver.GetName(NameType.Full)
        select driver.Email;

      foreach (string email in list)
      {
        if (sb.ToString() != string.Empty)
          sb.Append("; ");

        sb.Append(email);
      }

      return sb.ToString();
    }

    public List<Driver> GetDrivers()
    {
      var drivers = new List<Driver>();
      var driverCarList = DriverCarList.getInstance();

      var carList = CarList.GetInstance();
      var driverList = DriverList.getInstance();

      foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
      {
        if (cell.Visible)
        {
          var car = carList.getItem(_dgvMain.GetCarID(cell.RowIndex));
          var driver = car == null
            ? driverList.getItem(_dgvMain.GetID(cell.RowIndex))
            : driverCarList.GetDriver(car);

          if (CanAddToList(drivers, driver.Email))
            drivers.Add(driver);
        }
      }

      return drivers;
    }

    private static bool CanAddToList(List<Driver> drivers, string newEmail)
    {
      if (string.IsNullOrEmpty(newEmail))
        return false;

      return drivers.All(item => item.Email != newEmail);
    }
  }
}
