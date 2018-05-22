using System.Windows.Forms;
using BBAuto.Logic.Services.Dictionary;

namespace BBAuto.App.Dictionary
{
  public interface IOneStringDictionaryListForm
  {
    DialogResult ShowDialog(string header, IBasicDictionaryService dictionaryService);
  }
}
