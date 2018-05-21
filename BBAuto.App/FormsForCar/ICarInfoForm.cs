using System.Windows.Forms;
using BBAuto.Logic.Services.Car;

namespace BBAuto.App.FormsForCar
{
  public interface ICarInfoForm
  {
    DialogResult ShowDialog(CarModel car);
  }
}
