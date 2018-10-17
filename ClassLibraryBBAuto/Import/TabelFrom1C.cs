using System;
using System.IO;
using System.Linq;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Import
{
  public class TabelFrom1C : IExcelImporter
  {
    public string FilePath { get; set; }

    public bool StartImport()
    {
      try
      {
        TabelList tabelList = TabelList.GetInstance();

        string[] files = Directory.GetFiles(FilePath, "*.txt");

        foreach (var file in files)
        {
          string date = file.Split('_')[2].Split('.')[0];
          int month = Convert.ToInt32(string.Concat(date[0], date[1]));
          int year = Convert.ToInt32(string.Concat("20", date[2], date[3]));

          string[] lines = File.ReadAllLines(file);

          for (int i = 1; i < lines.Count(); i++)
          {
            string[] fields = lines[i].Split(';');

            for (int j = 2; j < fields.Count(); j++)
            {
              if ((fields[j] == "Я") || (fields[j] == "Я/Н"))
              {
                Tabel tabel = new Tabel(fields[0], new DateTime(year, month, j - 1));
                tabel.Save();
              }
            }
          }

          File.Move(file, FilePath + @"\processed\" + DateTime.Today.ToShortDateString() + " " + Path.GetFileName(file));
        }

        return true;
      }
      catch(Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }
    }
  }
}
