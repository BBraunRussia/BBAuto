using System;

namespace Common
{
  public abstract class DisposableObject : IDisposable
  {
    volatile bool disposed;
    readonly object disposeSyncRoot = new object();

    ~DisposableObject()
    {
      if (!disposed)
      {
        lock (disposeSyncRoot)
        {
          if (!disposed)
          {
            Dispose(false);
          }
        }
      }
    }

    public void Dispose()
    {
      if (!disposed)
      {
        lock (disposeSyncRoot)
        {
          if (!disposed)
          {
            Dispose(true);
          }
          disposed = true;
        }
        GC.SuppressFinalize(this);
      }
    }

    protected abstract void Dispose(bool disposing);
  }
}
