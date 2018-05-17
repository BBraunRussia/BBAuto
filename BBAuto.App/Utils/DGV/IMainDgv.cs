using System.Windows.Forms;

namespace BBAuto.App.Utils.DGV
{
  public interface IMainDgv
  {
    void SetDgv(DataGridView dgv);
    DataGridView Dgv { get; }
    DataGridViewCell CurrentCell { get; }
    DataGridViewSelectedCellCollection SelectedCells { get; }

    int GetId();
    int GetId(int rowIndex);

    int GetCarId();
    int GetCarId(int cellRowIndex);
    int GetCarId(DataGridViewCell cell);
    
    string GetFio(int cellRowIndex);
  }
}
