using System.Windows.Forms;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Documents.Office;

namespace BBAuto.Logic.Services.Documents
{
  public class ExcelDocument
  {
    private readonly ExcelDoc _excelDoc;

    public ExcelDocument()
    {
      _excelDoc = new ExcelDoc();
    }

    public ExcelDocument(string name)
    {
      var templateList = TemplateList.getInstance();
      var template = templateList.getItem(name);
      _excelDoc = new ExcelDoc(template.File);
    }
    
    public void Print()
    {
      _excelDoc.Print();
    }

    public void Show()
    {
      _excelDoc.Show();
    }

    public void Close()
    {
      _excelDoc.Dispose();
    }

    public void CreateHeader(string text)
    {
      _excelDoc.SetHeader(text);
    }

    public void CopyRange(string copyCell1, string copyCell2, string pastingCell)
    {
      _excelDoc.CopyRange(copyCell1, copyCell2, pastingCell);
    }

    public object GetValue(string cell)
    {
      return _excelDoc.GetValue(cell);
    }

    public void SetValue(int rowIndex, int columnIndex, string value)
    {
      _excelDoc.SetValue(rowIndex, columnIndex, value);
    }

    public void WriteHeader(DataGridView dgv, int minColumn, int columnCount)
    {
      var index = 1;

      for (var j = minColumn; j < columnCount; j++)
      {
        if (dgv.Columns[j].Visible)
        {
          _excelDoc.SetValue(1, index, dgv.Columns[j].HeaderText);
          index++;
        }
      }
    }
  }
}
