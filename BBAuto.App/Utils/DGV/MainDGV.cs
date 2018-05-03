using System;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.Utils.DGV
{
  public class MainDgv
  {
    public DataGridView Dgv { get; }
    private readonly DGVFormat _dgvFormated;

    public DataGridViewSelectedCellCollection SelectedCells => Dgv.SelectedCells;

    public DataGridViewCell CurrentCell => Dgv.CurrentCell;

    public MainDgv(DataGridView dgv)
    {
      Dgv = dgv;
      _dgvFormated = new DGVFormat(dgv);
    }

    public int GetId()
    {
      return Dgv.CurrentCell == null
        ? 0
        : GetId(0, Dgv.CurrentCell.RowIndex);
    }

    public int GetId(int rowIndex)
    {
      return GetId(0, rowIndex);
    }

    public Car GetCar()
    {
      return Dgv.CurrentCell == null
        ? null
        : CarList.getInstance().getItem(GetId(1, Dgv.CurrentCell.RowIndex));
    }

    public Car GetCar(DataGridViewCell cell)
    {
      return cell == null
        ? null
        : CarList.getInstance().getItem(GetId(1, cell.RowIndex));
    }

    public int GetCarId()
    {
      return Dgv.CurrentCell == null
        ? 0
        : GetId(1, Dgv.CurrentCell.RowIndex);
    }

    public int GetCarId(int rowIndex)
    {
      return GetId(1, rowIndex);
    }

    public string GetFio(int rowIndex)
    {
      if (Dgv.CurrentCell != null)
        return Dgv.Rows[rowIndex].Cells[8].Value.ToString();

      MessageBox.Show(Messages.SelectRowBeforeAction, Captions.Error, MessageBoxButtons.OK,
        MessageBoxIcon.Error);
      return "0";
    }


    private int GetId(int columnIndex, int rowIndex)
    {
      if (Dgv.CurrentCell != null)
        return Convert.ToInt32(Dgv.Rows[rowIndex].Cells[columnIndex].Value);

      MessageBox.Show(Messages.SelectRowBeforeAction, Captions.Error, MessageBoxButtons.OK,
        MessageBoxIcon.Error);
      return 0;

    }

    public void Format(Status status)
    {
      _dgvFormated.HideTwoFirstColumns();

      if (status != Status.FuelCard)
        _dgvFormated.FormatByOwner();

      _dgvFormated.Format(status);
    }
  }
}