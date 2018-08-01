using BBAuto.Domain.Common;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Domain.Services.OfficeDocument;

namespace BBAuto.Domain.Loaders
{
  public class FuelLoader
  {
    private const int BENZIN_ID = 1;
    private const int DIESEL_ID = 2;
    private const string DIESEL_NAME = "ДТ";
    private const string DIESEL_FULLNAME = "ДИЗЕЛЬ";

    private string path;
    private FuelReport fuelReport;
    private static readonly Dictionary<FuelReport, Action<ExcelDoc>> loaders;

    private static FuelCardList fuelCardList;
    private static EngineTypeList engineTypeList;
    private static EngineType benzin;
    private static EngineType disel;

    private static List<string> erorrs;

    public FuelLoader(string path, FuelReport fuelReport)
    {
      this.path = path;
      this.fuelReport = fuelReport;
      erorrs.Clear();
    }

    static FuelLoader()
    {
      erorrs = new List<string>();

      fuelCardList = FuelCardList.getInstance();
      engineTypeList = EngineTypeList.getInstance();
      benzin = engineTypeList.getItem(BENZIN_ID);
      disel = engineTypeList.getItem(DIESEL_ID);

      loaders = new Dictionary<FuelReport, Action<ExcelDoc>>();

      loaders.Add(FuelReport.Петрол, LoadPetrol);
      loaders.Add(FuelReport.Neste, LoadNeste);
      loaders.Add(FuelReport.Чеки, LoadChecks);
    }

    private static void LoadPetrol(ExcelDoc excel)
    {
      int i = 4; //начальный индекс

      string currentCell = "B" + i;
      while (excel.getValue(currentCell) != null)
      {
        currentCell = "D" + i;
        string number = excel.getValue(currentCell).ToString();
        FuelCard fuelCard = fuelCardList.getItem(number);
        if (fuelCard == null)
        {
          i++;
          currentCell = "B" + i;
          erorrs.Add("Не найдена карта №" + number); //throw new NullReferenceException("Не найдена карта №" + number);
          continue;
        }

        currentCell = "B" + i;
        string dateString = excel.getValue1(currentCell).ToString();
        DateTime datetime;
        DateTime.TryParse(dateString, out datetime); //присутствует время, не забываем убирать

        currentCell = "G" + i;
        string engineTypeName = excel.getValue(currentCell).ToString();
        EngineType engineType = GetEngineType(engineTypeName);

        currentCell = "H" + i;
        double value;
        double.TryParse(excel.getValue(currentCell).ToString(), out value);

        Fuel fuel = new Fuel(fuelCard, datetime.Date, engineType);
        fuel.AddValue(value);
        fuel.Save();

        i++;
        currentCell = "B" + i;
      }
    }

    private static void LoadNeste(ExcelDoc excel)
    {
      int i = 4; //начальный индекс

      string currentCell = "A" + i;
      while (excel.getValue(currentCell) != null)
      {
        if (excel.getValue(currentCell).ToString() == "Grand Total")
          break;

        currentCell = "B" + i;
        if (excel.getValue(currentCell) != null)
        {
          i++;
          currentCell = "A" + i;
          continue;
        }

        currentCell = "A" + i;
        string number = excel.getValue(currentCell).ToString().Split(' ')[1]; //split example Карта: 7105066553656018
        FuelCard fuelCard = fuelCardList.getItem(number);
        if (fuelCard == null)
        {
          i++;
          erorrs.Add("Не найдена карта №" + number); //throw new NullReferenceException("Не найдена карта №" + number);
          continue;
        }

        currentCell = "C" + i;
        DateTime datetime;
        DateTime.TryParse(excel.getValue(currentCell).ToString(),
          out datetime); //присутствует время, не забываем убирать

        currentCell = "D" + i;
        string engineTypeName = excel.getValue(currentCell).ToString();
        EngineType engineType = GetEngineType(engineTypeName);

        currentCell = "E" + i;
        double value;
        double.TryParse(excel.getValue(currentCell).ToString(), out value);

        Fuel fuel = new Fuel(fuelCard, datetime.Date, engineType);
        fuel.AddValue(value);
        fuel.Save();

        i++;
        currentCell = "A" + i;
      }
    }

    private static void LoadChecks(ExcelDoc excel)
    {
      throw new NotImplementedException();
    }

    private static EngineType GetEngineType(string engineTypeName)
    {
      engineTypeName = engineTypeName.ToUpper();
      return ((engineTypeName == DIESEL_NAME) || (engineTypeName == DIESEL_FULLNAME)) ? disel : benzin;
    }

    public List<string> Load()
    {
      using (ExcelDoc excelDoc = new ExcelDoc(path))
      {
        loaders[fuelReport].Invoke(excelDoc);
      }

      return erorrs;
    }
  }
}
