namespace BBAuto.Repository
{
  public interface IRepository<T> where T : class
  {
    T DbRepository { get; }
  }
}
