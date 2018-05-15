using System;
using System.IO;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Documents.Office;

namespace BBAuto.Logic.Common
{
  public class MileageFill
  {
    private readonly string[,] _literal = {
      {"A", "А"}, {"B", "В"}, {"E", "Е"}, {"K", "К"}, {"M", "М"}, {"H", "Н"}, {"O", "О"}, {"P", "Р"}, {"C", "С"},
      {"T", "Т"}, {"Y", "У"}, {"X", "Х"}, {"RUS", ""}, {"/", ""}
    };

    private readonly DateTime _date;
    private readonly MileageReportList _mileageReportList;

    private readonly string _folder;

    public MileageFill(string folder, DateTime date)
    {
      _mileageReportList = new MileageReportList();

      _folder = folder;
      _date = date;
    }

    public void Begin()
    {
      var filenames = Directory.GetFiles(_folder);

      foreach (var fileName in filenames)
      {
        ReadFile(fileName);
      }
    }

    private void ReadFile(string filename)
    {
      try
      {
        using (var excelDoc = new ExcelDoc(filename))
        {
          try
          {
            excelDoc.SetList("Расходы по а-м");

            var grz = (excelDoc.GetValue("B4") != null) ? excelDoc.GetValue("B4").ToString() : string.Empty;

            var car = GetCar(grz);

            if (car == null)
            {
              var driverFio = (excelDoc.GetValue("B5") != null) ? excelDoc.GetValue("B5").ToString() : string.Empty;

              var driverList = DriverList.getInstance();
              var driver = driverList.getItemByFIO(driverFio);

              if (driver != null)
              {
                var driverCarList = DriverCarList.getInstance();
                car = driverCarList.GetCar(driver);
              }

              if (car == null)
                _mileageReportList.Add(new MileageReport(null,
                  string.Concat("Не найден автомобиль: ", grz, " сотрудник: ", driverFio, ". Файл: ", filename)));
            }

            if (car != null)
            {
              string value = excelDoc.GetValue("C8") != null ? excelDoc.GetValue("C8").ToString() : string.Empty;

              SetMileage(car, value);
            }
          }

          catch (IndexOutOfRangeException)
          {
            _mileageReportList.Add(new MileageReport(null, string.Concat("Ошибка при чтении файла: ", filename)));
          }
          catch (OverflowException)
          {
            _mileageReportList.Add(new MileageReport(null,
              string.Concat("Указан слишком большой пробег в файле: ", filename)));
          }
        }
      }
      catch
      {
        _mileageReportList.Add(new MileageReport(null, string.Concat("Ошибка при чтении файла: ", filename)));
      }
    }

    private Car GetCar(string grz)
    {
      if (grz == string.Empty)
        return null;

      var carList = CarList.getInstance();
      return carList.getItem(FormatGrz(grz));
    }

    private string FormatGrz(string value)
    {
      value = value.ToUpper();

      for (var i = 0; i < 14; i++)
      {
        value = value.Replace(_literal[i, 0], _literal[i, 1]);
      }

      return value;
    }

    private void SetMileage(Car car, string value)
    {
      int.TryParse(value, out int count);

      if (count == 0)
        return;

      var mileageList = MileageList.getInstance();
      var mileage = mileageList.getItem(car);

      if (count > Convert.ToInt32(mileage.Count))
      {
        if (mileage.Count != string.Empty)
        {
          mileage = car.createMileage();
        }

        mileage.Date = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));
        mileage.SetCount(value);
        mileage.Save();
        _mileageReportList.Add(new MileageReport(car, "Пробег загружен"));
      }
      else if (count < Convert.ToInt32(mileage.Count))
      {
        _mileageReportList.Add(new MileageReport(car, "Значение пробега меньше, чем уже внесён в систему."));
      }
      else
      {
        _mileageReportList.Add(new MileageReport(car,
          "Новое значение пробега равно значению пробега уже внесённому в систему."));
      }
    }

    public MileageReportList GetMileageReportList()
    {
      return _mileageReportList;
    }
  }
}
