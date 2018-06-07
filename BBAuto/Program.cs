using System;
using System.Windows.Forms;
using BBAuto.Domain.Static;
using BBAuto.Domain.DataBase;

namespace BBAuto
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      DataBase.InitDataBase();
      Provider.InitSqlProvider();

      if (User.Login())
        Application.Run(new mainForm());
      else
        MessageBox.Show("У вас недостаточно прав для работы с программой", "Доступ заблокирован", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
    }
  }
}
