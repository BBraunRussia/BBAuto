using System.Windows.Forms;
using BBAuto.Logic.Services.DiagCard;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public interface IDiagCardForm
  {
    DialogResult ShowDialog(DiagCardModel diagCard);
  }
}
