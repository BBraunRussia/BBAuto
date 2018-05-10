using System.Windows.Forms;
using BBAuto.Logic.Services.Mileage;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IMileageForm
  {
    DialogResult ShowDialog(MileageModel mileage);
  }
}
