using System.Windows.Forms;
using BBAuto.App.FormsForDriver.AddEdit;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IViolationForm
  {
    DialogResult ShowDialog(int violationId, int carId, ICarForm carForm, IDriverForm driverForm);
  }
}
