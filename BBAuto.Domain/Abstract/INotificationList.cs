using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Abstract
{
    public interface INotificationList
    {
        List<INotification> ToList();
    }
}
