using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Abstract
{
    public interface INotification
    {
        DateTime DateEnd { get; }
        bool IsNotificationSent { get; }
        void SendNotification();
        Driver Driver { get; set; }
    }
}
