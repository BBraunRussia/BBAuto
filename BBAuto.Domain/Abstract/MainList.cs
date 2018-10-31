using System.Collections.Generic;
using BBAuto.Domain.DataBase;

namespace BBAuto.Domain.Abstract
{
  public abstract class MainList<T>
  {
    protected List<T> _list;

    protected IProvider Provider;

    protected abstract void LoadFromSql();

    protected MainList()
    {
      Provider = DataBase.Provider.GetProvider();

      _list = new List<T>();

      LoadFromSql();
    }

    public void ReLoad()
    {
      _list.Clear();

      LoadFromSql();
    }

    public virtual void Add(T item)
    {
      if (_list.Contains(item))
        return;

      _list.Add(item);
    }
  }
}
