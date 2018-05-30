using System.Windows.Forms;
using BBAuto.Logic.Static;

namespace BBAuto.App.CommonForms
{
  public interface IInputDateForm
  {
    DialogResult ShowDialog(Logic.Static.Actions action, WayBillType type);
  }
}
