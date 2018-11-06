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
    public event EventHandler<EventArgs> DataSourceChanged;

    private MainStatus()
    {
    }

    protected virtual void OnStatusChanged(StatusEventArgs e)
    {
      StatusChanged?.Invoke(this, e);
    }

    protected virtual void OnDataSourceChanged(EventArgs e)
    {
      DataSourceChanged?.Invoke(this, e);
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

      OnStatusChanged(new StatusEventArgs(status));
    }

    public void Reload()
    {
      OnDataSourceChanged(new EventArgs());
    }
    
    public override string ToString()
    {
      var statuses = Statuses.getInstance();
      return statuses.getItem(Convert.ToInt32(_status));
    }
  }
}
