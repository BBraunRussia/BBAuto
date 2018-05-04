using BBAuto.Logic.Common;
using BBAuto.Logic.DataBase;

namespace BBAuto.Logic.Abstract
{
  public abstract class MainDictionary
  {
    protected string FileBegin;
    protected static IProvider Provider;

    internal abstract object[] ToRow();

    public virtual void Save()
    {
    }

    internal virtual void Delete()
    {
    }

    protected MainDictionary()
    {
      Provider = DataBase.Provider.GetProvider();
    }

    protected void DeleteFile(string newFile)
    {
      if (FileBegin != string.Empty && FileBegin != newFile)
        WorkWithFiles.Delete(FileBegin);
    }

    public int Id { get; protected set; }
  }
}
