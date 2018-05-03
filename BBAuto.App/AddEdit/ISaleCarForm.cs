using System.Windows.Forms;
using BBAuto.Logic.Services.Car.Sale;

namespace BBAuto.App.AddEdit
{
  public interface ISaleCarForm
  {
    DialogResult ShowDialog(SaleCarModel car);
  }
}
