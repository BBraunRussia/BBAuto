using System.Windows.Forms;
using BBAuto.Logic.Services.Car.Doc;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface ICarDocForm
  {
    DialogResult ShowDialog(CarDocModel carDoc);
  }
}
