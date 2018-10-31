using System;
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
      return _uniqueInstance ?? (_uniqueInstance = new MainStatus());
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
      if (_status == status)
        return;

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
