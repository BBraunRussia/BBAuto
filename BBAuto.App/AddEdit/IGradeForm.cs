using System.Windows.Forms;

namespace BBAuto.App.AddEdit
{
  public interface IGradeForm
  {
    DialogResult ShowDialog(int gradeId, int modelId = 0);
  }
}
