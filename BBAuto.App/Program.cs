using System;
using System.Windows.Forms;
using BBAuto.App.config;
using BBAuto.Logic.DataBase;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App
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

      AutoMapperConfiguration.Initialize();
      WindsorConfiguration.Register();

      DataBase.InitDataBase();
      Provider.InitSQLProvider();

      var container = WindsorConfiguration.Container;

      var form = container.Resolve<IForm>();

      if (User.Login())
        Application.Run((Form)form);
      else
        MessageBox.Show(Messages.HaveNotRights, Captions.CannotAccess, MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
    }
  }
}
