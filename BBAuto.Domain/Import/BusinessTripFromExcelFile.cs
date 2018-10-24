using System;
using System.IO;
using System.Linq;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Import
{
  public class BusinessTripFromExcelFile //: IExcelImporter
  {
    private const string FileVygruzkaAvto = "VygruzkaAvto_Braun_КК1.txt";

    public string FilePath { get; set; }

    /* Excel не загружает, если из планировщика */
    //public void StartImportExcel()
    //{
    //    try
    //    {
    //        using (ExcelDoc excel = new ExcelDoc(FilePath))
    //        {
    //            int i = 2;

    //            string curCell = "C" + i;
    //            while (excel.getValue(curCell) != null)
    //            {
    //                curCell = "A" + i;
    //                if ((excel.getValue(curCell) == null) ||
    //                    ((excel.getValue(curCell).ToString().ToUpper() != "AM") && (excel.getValue(curCell).ToString().ToUpper() != "АМ")))
    //                {
    //                    curCell = "C" + i;
    //                    i++;
    //                    continue;
    //                }

    //                curCell = "C" + i;
    //                string number = excel.getValue(curCell).ToString();

    //                curCell = "D" + i;
    //                DateTime dateBegin;
    //                DateTime.TryParse(excel.getValue1(curCell).ToString(), out dateBegin);

    //                curCell = "E" + i;
    //                DateTime dateEnd;
    //                DateTime.TryParse(excel.getValue1(curCell).ToString(), out dateEnd);

    //                for (DateTime date = dateBegin; date <= dateEnd; date = date.AddDays(1))
    //                {
    //                    Tabel tabel = new Tabel(number, date) { Comment = "businessTrip" };
    //                    tabel.Save();
    //                }

    //                curCell = "C" + i;
    //                i++;
    //            }
    //        }
    //    }
    //    catch (NullReferenceException ex)
    //    {
    //        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
    //    }
    //    catch (COMException ex)
    //    {
    //        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
    //    }
    //}

    /* txt формат предыдущего файла */
    public bool StartImport()
    {
      try
      {
        string[] files = Directory.GetFiles(FilePath, FileVygruzkaAvto);

        if (!files.Any())
        {
          Logger.LogManager.Logger.Warning($"Файл {FilePath}\\{FileVygruzkaAvto} не найден");
          return false;
        }
        
        string[] lines = File.ReadAllLines(files[0]);

        for (int i = 1; i < lines.Count(); i++)
        {
          string[] fields = lines[i].Split(';');

          if (fields[0] != "АМ")
          {
            continue;
          }

          string number = fields[2]; //табельный номер

          DateTime dateBegin;
          DateTime.TryParse(fields[3], out dateBegin);
          DateTime dateEnd;
          DateTime.TryParse(fields[4], out dateEnd);

          for (DateTime date = dateBegin; date <= dateEnd; date = date.AddDays(1))
          {
            Tabel tabel = new Tabel(number, date) { Comment = "businessTrip" };
            tabel.Save();
          }

        }

        File.Move(files[0], FilePath + @"\processed\" + $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}_{Path.GetFileName(files[0])}");

      }
      catch(Exception ex)
      {
        Logger.LogManager.Logger.Error(ex, ex.Message);
        return false;
      }

      return true;
    }



    /*Старый файл с командировками */
    //public void StartImport2()
    //{
    //    try
    //    {
    //        using (ExcelDoc excel = new ExcelDoc(FilePath))
    //        {
    //            int i = 7;

    //            string curCell = "H" + i;
    //            while (excel.getValue(curCell) != null)
    //            {
    //                curCell = "A" + i;
    //                if ((excel.getValue(curCell) == null) ||
    //                    ((excel.getValue(curCell).ToString().ToUpper() != "AM") && (excel.getValue(curCell).ToString().ToUpper() != "АМ")))
    //                {
    //                    curCell = "H" + i;
    //                    i++;
    //                    continue;
    //                }

    //                curCell = "H" + i;
    //                string number = excel.getValue(curCell).ToString();

    //                curCell = "D" + i;
    //                DateTime dateBegin;
    //                DateTime.TryParse(excel.getValue1(curCell).ToString(), out dateBegin);

    //                curCell = "E" + i;
    //                DateTime dateEnd;
    //                DateTime.TryParse(excel.getValue1(curCell).ToString(), out dateEnd);

    //                for (DateTime date = dateBegin; date <= dateEnd; date = date.AddDays(1))
    //                {
    //                    Tabel tabel = new Tabel(number, date) { Comment = "businessTrip" };
    //                    tabel.Save2();
    //                }

    //                curCell = "H" + i;
    //                i++;
    //            }
    //        }
    //    }
    //    catch (NullReferenceException ex)
    //    {
    //        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
    //    }
    //    catch (COMException ex)
    //    {
    //        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
    //    }
    //}

  }
}
