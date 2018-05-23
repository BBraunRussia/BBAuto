namespace BBAuto.Repositories.Entities
{
  public class DbDictionary
  {
    public DbDictionary() { }

    public DbDictionary(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
  }
}
