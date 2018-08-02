using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Static;
using BBAuto.Domain.Lists;

namespace BBAuto
{
    public class DriverMails
    {
        private MainDGV _dgvMain;

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

        private List<Driver> GetDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            DriverCarList driverCarList = DriverCarList.getInstance();

            CarList carList = CarList.GetInstance();

            foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
            {
                if (cell.Visible)
                {
                    Car car = carList.getItem(_dgvMain.GetCarID(cell.RowIndex));
                    Driver driver = driverCarList.GetDriver(car);

                    if (CanAddToList(drivers, driver.Email))
                        drivers.Add(driver);
                }
            }

            return drivers;
        }

        private bool CanAddToList(List<Driver> drivers, string newEmail)
        {
            if (newEmail == string.Empty)
                return false;

            List<string> addresses = drivers.Where(item => item.Email == newEmail).Select(item => item.Email).ToList();

            return addresses.Count() == 0;
        }
    }
}
