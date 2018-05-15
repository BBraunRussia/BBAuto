using System;

namespace BBAuto.Logic.Services.Documents.Office
{
  public class OfficeDoc
  {
    protected string Name { get; }

    protected OfficeDoc()
    {
    }

    protected OfficeDoc(string name)
    {
      Name = name;
    }

    protected void releaseObject(object obj)
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
