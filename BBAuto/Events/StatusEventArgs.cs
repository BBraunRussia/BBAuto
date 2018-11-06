using BBAuto.Domain.Static;
using System;

namespace BBAuto
{
  public class StatusEventArgs : EventArgs
  {
    public StatusEventArgs(Status status)
    {
      Status = status;
    }

    public Status Status { get; }
  }
}
