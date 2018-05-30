using System.Windows.Forms;
using BBAuto.Logic.Services.Driver;

namespace BBAuto.App.FormsForDriver.AddEdit
{
  public interface IDriverForm
  {
    DialogResult ShowDialog(DriverModel driver);
  }
}
