using BBAuto.Domain.DataBase;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Abstract
{
  public abstract class MainDictionary
  {
    protected string _fileBegin;
    protected static IProvider _provider;

    internal abstract object[] getRow();

    public virtual void Save() { }

    internal virtual void Delete() { }

    protected MainDictionary()
    {
      _provider = Provider.GetProvider();
    }

    protected void DeleteFile(string newFile)
    {
      if (!string.IsNullOrEmpty(_fileBegin) && _fileBegin != newFile)
        WorkWithFiles.Delete(_fileBegin);
    }

    public int ID { get; protected set; }
  }
}
