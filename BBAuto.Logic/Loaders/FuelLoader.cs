using System;
using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Dictionary.EngineType;
using BBAuto.Logic.Services.Documents.Office;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Loaders
{
  public class FuelLoader : IFuelLoader
  {
    private static Dictionary<FuelReport, Action<ExcelDoc>> _loaders;
    private FuelCardList _fuelCardList;
    private List<string> _erorrs;

    private readonly IEngineTypeService _engineTypeService;

    public FuelLoader(IEngineTypeService engineTypeService)
    {
      _engineTypeService = engineTypeService;
    }
    
    public IList<string> Load(string path, FuelReport fuelReport)
    {
      _erorrs = new List<string>();

      _fuelCardList = FuelCardList.getInstance();

      _loaders = new Dictionary<FuelReport, Action<ExcelDoc>>
      {
        {FuelReport.Петрол, LoadPetrol},
        {FuelReport.Neste, LoadNeste},
        {FuelReport.Чеки, LoadChecks}
      };

      using (var excelDoc = new ExcelDoc(path))
      {
        _loaders[fuelReport].Invoke(excelDoc);
      }

      return _erorrs;
    }

    private void LoadPetrol(ExcelDoc excel)
    {
      var i = 4; //начальный индекс

      var currentCell = "B" + i;
      while (excel.GetValue(currentCell) != null)
      {
        currentCell = "D" + i;
        var number = excel.GetValue(currentCell).ToString();
        var fuelCard = _fuelCardList.getItem(number);
        if (fuelCard == null)
        {
          i++;
          currentCell = "B" + i;
          _erorrs.Add("Не найдена карта №" + number); //throw new NullReferenceException("Не найдена карта №" + number);
          continue;
        }

        currentCell = "B" + i;
        var dateString = excel.GetValue1(currentCell).ToString();
        DateTime.TryParse(dateString, out var datetime); //присутствует время, не забываем убирать

        currentCell = "G" + i;
        var engineTypeName = excel.GetValue(currentCell).ToString();
        var engineType = GetEngineType(engineTypeName);

        currentCell = "H" + i;
        double.TryParse(excel.GetValue(currentCell).ToString(), out var value);

        var fuel = new Fuel(fuelCard, datetime.Date, engineType.Id);
        fuel.AddValue(value);
        fuel.Save();

        i++;
        currentCell = "B" + i;
      }
    }

    private void LoadNeste(ExcelDoc excel)
    {
      var i = 4; //начальный индекс

      var currentCell = "A" + i;
      while (excel.GetValue(currentCell) != null)
      {
        if (excel.GetValue(currentCell).ToString() == "Grand Total")
          break;

        currentCell = "B" + i;
        if (excel.GetValue(currentCell) != null)
        {
          i++;
          currentCell = "A" + i;
          continue;
        }

        currentCell = "A" + i;
        var number = excel.GetValue(currentCell).ToString().Split(' ')[1]; //split example Карта: 7105066553656018
        var fuelCard = _fuelCardList.getItem(number);
        if (fuelCard == null)
        {
          i++;
          _erorrs.Add("Не найдена карта №" + number); //throw new NullReferenceException("Не найдена карта №" + number);
          continue;
        }

        currentCell = "C" + i;
        DateTime.TryParse(excel.GetValue(currentCell).ToString(),
          out var datetime); //присутствует время, не забываем убирать

        currentCell = "D" + i;
        var engineTypeName = excel.GetValue(currentCell).ToString();
        var engineType = GetEngineType(engineTypeName);

        currentCell = "E" + i;
        double.TryParse(excel.GetValue(currentCell).ToString(), out var value);

        var fuel = new Fuel(fuelCard, datetime.Date, engineType.Id);
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

    private EngineTypeModel GetEngineType(string engineTypeName)
    {
      var result = _engineTypeService.GetItems()
        .FirstOrDefault(e => e.Name == engineTypeName || (e as EngineTypeModel)?.ShortName == engineTypeName);

      return result as EngineTypeModel;
    }
  }
}
