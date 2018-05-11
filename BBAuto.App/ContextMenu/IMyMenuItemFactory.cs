using System.Windows.Forms;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Static;

namespace BBAuto.App.ContextMenu
{
  public interface IMyMenuItemFactory
  {
    void SetMainDgv(IMainDgv dgvMain);
    ToolStripItem CreateItem(ContextMenuItem separator);
    ToolStripItem CreateItem(Status status);
  }
}
