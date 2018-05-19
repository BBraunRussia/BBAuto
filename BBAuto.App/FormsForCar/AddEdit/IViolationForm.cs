using System.Windows.Forms;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IViolationForm
  {
    DialogResult ShowDialog(int violationId, int carId, ICarForm carForm);
  }
}
