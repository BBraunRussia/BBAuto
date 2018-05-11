using System.Windows.Forms;

namespace BBAuto.Logic.Services.Documents
{
  public interface IDocument
  {
    void Print();
    void Show();
    void Close();
    void SetValue(int rowIndex, int columnIndex, string value);
    void WriteHeader(DataGridView dgv, int minColumn, int columnCount);
    void CreateHeader(string text);
  }
}
