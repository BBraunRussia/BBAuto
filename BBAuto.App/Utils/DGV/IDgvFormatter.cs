using System.Windows.Forms;
using BBAuto.Logic.Static;

namespace BBAuto.App.Utils.DGV
{
  public interface IDgvFormatter
  {
    void SetDgv(DataGridView dgv);

    void HideTwoFirstColumns();
    void HideColumn(int index);

    void FormatDgv(Status status);

    void SetFormatMileage();
    void SetFormatRepair();
    void FormatByOwner();
  }
}
