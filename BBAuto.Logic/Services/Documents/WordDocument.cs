using BBAuto.Logic.Services.Documents.Office;

namespace BBAuto.Logic.Services.Documents
{
  public class WordDocument
  {
    private readonly WordDoc _wordDoc;

    public WordDocument(string name)
    {
      _wordDoc = new WordDoc(name);
    }

    public void AddRowInTable(int tableIndex, params string[] Params)
    {
      _wordDoc.AddRowInTable(tableIndex, Params);
    }

    public void setValue(string search, string replace)
    {
      _wordDoc.setValue(search, replace);
    }

    public void Show()
    {
      _wordDoc.Show();
    }

    public void Print()
    {
      _wordDoc.Print();
    }
  }
}
