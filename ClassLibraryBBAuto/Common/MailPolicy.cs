using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Common
{
    public static class MailPolicy
    {
        public static string Send(Car car, PolicyType type)
        {
            EMail mail = new EMail();

            mail.sendMailPolicy(car, type);

            DriverCarList driverCarList = DriverCarList.getInstance();
            Driver driver = driverCarList.GetDriver(car);

            return string.Concat("Полис ", type.ToString(), " отправлен на адрес ", driver.Email);
        }
    }
}
