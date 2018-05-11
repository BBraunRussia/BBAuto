using System.Windows.Forms;
using BBAuto.Logic.Entities;

namespace BBAuto.App.Utils.DGV
{
  public interface IMainDgv
  {
    void SetDgv(DataGridView dgv);
    Car GetCar();
    DataGridView Dgv { get; }
    DataGridViewCell CurrentCell { get; }
    DataGridViewSelectedCellCollection SelectedCells { get; }
    int GetId();
    int GetCarId(int cellRowIndex);
    int GetId(int rowIndex);
    Car GetCar(DataGridViewCell cell);
    string GetFio(int cellRowIndex);
    int GetCarId();
  }
}
