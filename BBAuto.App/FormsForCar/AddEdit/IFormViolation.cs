using System.Windows.Forms;
using BBAuto.Logic.ForCar;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IFormViolation
  {
    DialogResult ShowDialog(Violation violation);
  }
}
