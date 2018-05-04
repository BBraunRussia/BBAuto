using System.Windows.Forms;
using BBAuto.Logic.Services.Mileage;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IFormMileage
  {
    DialogResult ShowDialog(MileageModel mileage);
  }
}
