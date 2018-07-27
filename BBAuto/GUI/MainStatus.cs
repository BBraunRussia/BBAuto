using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Domain.Static;
using BBAuto.Domain.Dictionary;

namespace BBAuto
{
  internal class MainStatus
  {
    private static MainStatus _uniqueInstance;
    private Status _status;

    public event EventHandler<StatusEventArgs> StatusChanged;

    private MainStatus()
    {
    }

    protected virtual void OnStatusChanged(StatusEventArgs e)
    {
      EventHandler<StatusEventArgs> temp = StatusChanged;

      if (temp != null)
        temp(this, e);
    }

    public static MainStatus getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new MainStatus();

      return _uniqueInstance;
    }

    public Status Get()
    {
      return _status;
    }

    public void Set(int idStatus)
    {
      Set((Status) idStatus);
    }

    public void Set(Status status)
    {
      _status = status;

      StatusEventArgs e = new StatusEventArgs(status);

      OnStatusChanged(e);
    }

    public bool IsSale()
    {
      return _status == Status.Sale;
    }

    public override string ToString()
    {
      var statuses = Statuses.getInstance();
      return statuses.getItem(Convert.ToInt32(_status));
    }
  }
}
