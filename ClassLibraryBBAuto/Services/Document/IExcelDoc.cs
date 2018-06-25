namespace BBAuto.Domain.Services.Document
{
  public interface IExcelDoc : IDocument
  {
    void setValue(int rowIndex, int columnIndex, string value);
    void CopyRange(string copingCell1, string copingCell2, string pastingCell);
    object getValue(string cell);
  }
}
