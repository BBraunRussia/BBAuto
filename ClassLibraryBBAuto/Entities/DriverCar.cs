using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Entities
{
    public class DriverCar
    {
        private DateTime dateBegin;
        public readonly DateTime dateEnd;
        public int Number { get; private set; }
        public Car Car { get; private set; }
        public Driver Driver { get; private set; }

        public DriverCar(DataRow row)
        {
            int idCar;
            int.TryParse(row.ItemArray[0].ToString(), out idCar);
            Car = CarList.GetInstance().getItem(idCar);

            if (idCar == 191)
                idCar = 191;
            int idDriver;
            int.TryParse(row.ItemArray[1].ToString(), out idDriver);
            Driver = DriverList.getInstance().getItem(idDriver);

            DateTime.TryParse(row.ItemArray[2].ToString(), out dateBegin);
            DateTime.TryParse(row.ItemArray[3].ToString(), out dateEnd);

            int number;
            int.TryParse(row.ItemArray[4].ToString(), out number);
            Number = number;
            
            dateEnd = dateEnd.Date;
        }
        
        internal bool isDriverCar(Car car, DateTime date)
        {
            if ((date >= DateTime.Today) && (dateEnd == DateTime.Today))
                return car.ID == Car.ID && date >= dateBegin;
            else
                return car.ID == Car.ID && date >= dateBegin && date < dateEnd;
        }
        
        internal bool isCarsDriver(Driver driver, DateTime date)
        {
            if ((date >= DateTime.Today) && (dateEnd == DateTime.Today))
                return driver.ID == Driver.ID && date >= dateBegin;
            else
                return driver.ID == Driver.ID && date >= dateBegin && date < dateEnd;
        }
    }
}
