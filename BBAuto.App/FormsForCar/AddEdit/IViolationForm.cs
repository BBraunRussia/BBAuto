using System.Windows.Forms;
using BBAuto.Logic.Services.Violation;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IViolationForm
  {
    DialogResult ShowDialog(ViolationModel violation);
  }
}
