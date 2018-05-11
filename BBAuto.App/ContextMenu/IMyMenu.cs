using System.Windows.Forms;
using BBAuto.App.Utils.DGV;

namespace BBAuto.App.ContextMenu
{
  public interface IMyMenu
  {
    void SetMainDgv(IMainDgv dgvMain);
    MenuStrip CreateMainMenu();
    ContextMenuStrip CreateContextMenu();
  }
}
