using System.Windows.Forms;
using BBAuto.Logic.Services.Car;

namespace BBAuto.App.FormsForCar
{
  public interface ICarForm
  {
    DialogResult ShowDialog(int carId);
  }
}
