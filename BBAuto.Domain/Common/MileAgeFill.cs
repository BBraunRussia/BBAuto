using System;
using System.IO;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.OfficeDocument;

namespace BBAuto.Domain.Common
{
  public class MileageFill
  {
    private readonly string[,] _literal = {
      {"A", "А"}, {"B", "В"}, {"E", "Е"}, {"K", "К"}, {"M", "М"}, {"H", "Н"}, {"O", "О"}, {"P", "Р"}, {"C", "С"},
      {"T", "Т"}, {"Y", "У"}, {"X", "Х"}, {"RUS", ""}, {"/", ""}
    };

    private DateTime _date;
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
      string[] filenames = Directory.GetFiles(_folder);

      foreach (string fileName in filenames)
      {
        ReadFile(fileName);
      }
    }

    private void ReadFile(string filename)
    {
      try
      {
        using (ExcelDoc excelDoc = new ExcelDoc(filename))
        {
          try
          {
            excelDoc.SetList("Расходы по а-м");

            string grz = (excelDoc.getValue("B4") != null) ? excelDoc.getValue("B4").ToString() : string.Empty;

            Car car = GetCar(grz);

            if (car == null)
            {
              string driverFIO = (excelDoc.getValue("B5") != null) ? excelDoc.getValue("B5").ToString() : string.Empty;

              DriverList driverList = DriverList.getInstance();
              Driver driver = driverList.getItemByFIO(driverFIO);

              if (driver != null)
              {
                DriverCarList driverCarList = DriverCarList.GetInstance();
                car = driverCarList.GetCar(driver);
              }

              if (car == null)
                _mileageReportList.Add(new MileageReport(null,
                  string.Concat("Не найден автомобиль: ", grz, " сотрудник: ", driverFIO, ". Файл: ", filename)));
            }

            if (car != null)
            {
              string value = (excelDoc.getValue("C8") != null) ? excelDoc.getValue("C8").ToString() : string.Empty;

              SetMileage(car, value);
            }
          }

          catch (IndexOutOfRangeException ex)
          {
            _mileageReportList.Add(new MileageReport(null, $"Ошибка при чтении файла: {filename} Ошибка: {ex.Message}"));
          }
          catch (OverflowException)
          {
            _mileageReportList.Add(new MileageReport(null,
              string.Concat("Указан слишком большой пробег в файле: ", filename)));
          }
        }
      }
      catch(Exception ex)
      {
        _mileageReportList.Add(new MileageReport(null, $"Ошибка при открытии файла: {filename} Ошибка: {ex.Message}"));
      }
    }

    private Car GetCar(string grz)
    {
      if (grz == string.Empty)
        return null;

      CarList carList = CarList.GetInstance();
      return carList.getItem(FormatGRZ(grz));
    }

    private string FormatGRZ(string value)
    {
      value = value.ToUpper();

      for (int i = 0; i < 14; i++)
      {
        value = value.Replace(_literal[i, 0], _literal[i, 1]);
      }

      return value;
    }

    private void SetMileage(Car car, string value)
    {
      if (!int.TryParse(value, out int count))
        return;

      var lastMileage = MileageList.getInstance().getItemByCarId(car.ID);
      var loadMonthMileage = MileageList.getInstance().getItem(car.ID, _date) ?? new Mileage(car.ID, new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month)));

      if ((lastMileage?.Count ?? 0) > count)
      {
        _mileageReportList.Add(new MileageReport(car, "Новое значение пробега меньше, чем уже внесённый в систему."));
        return;
      }

      loadMonthMileage.SetCount(value);
      loadMonthMileage.Save();
      _mileageReportList.Add(new MileageReport(car, "Пробег загружен"));
    }

    public MileageReportList GetMileageReportList()
    {
      return _mileageReportList;
    }
  }
}
