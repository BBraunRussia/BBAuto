using System.Windows.Forms;
using BBAuto.Logic.Services.Dictionary;

namespace BBAuto.App.AddEdit
{
  public interface IOneStringDictionaryForm
  {
    DialogResult ShowDialog(int id, IBasicDictionaryService dictionaryService);
  }
}
