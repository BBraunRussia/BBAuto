using System;

namespace BBAuto.Domain.Services.OfficeDocument
{
  public abstract class OfficeDoc
  {
    protected string Name;

    protected OfficeDoc()
    {
    }

    protected OfficeDoc(string fileName)
    {
      Name = fileName;
    }

    protected void ReleaseObject(object obj)
    {
      try
      {
        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        obj = null;
      }
      catch
      {
        obj = null;
      }
      finally
      {
        GC.Collect();
      }
    }
  }
}
