using System;
using System.Collections.Generic;
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
    private readonly IList<MileageReport> _mileageReportList;

    private readonly string _folder;

    public MileageFill(string folder, DateTime date)
    {
      _mileageReportList = new List<MileageReport>();

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
      var mileageReport = new MileageReport { Filename = filename };

      try
      {
        using (var excelDoc = new ExcelDoc(filename))
        {
          try
          {
            excelDoc.SetList("Расходы по а-м");

            var grz = excelDoc.getValue("B4") != null ? excelDoc.getValue("B4").ToString() : string.Empty;
            mileageReport.Grz = grz;

            var car = GetCar(grz);
            mileageReport.Car = car;

            var driverFio = excelDoc.getValue("B5") != null ? excelDoc.getValue("B5").ToString() : string.Empty;
            mileageReport.Fio = driverFio;
            
            if (car == null)
            {
              var driver = DriverList.getInstance().getItemByFIO(driverFio);

              if (driver != null)
              {
                car = DriverCarList.GetInstance().GetCar(driver);
              }

              if (car == null)
              {
                mileageReport.Message = "Не найден автомобиль";
                _mileageReportList.Add(mileageReport);
              }
            }
            
            if (car != null)
            {
              var value = excelDoc.getValue("C8") != null ? excelDoc.getValue("C8").ToString() : string.Empty;

              mileageReport.Car = car;
              mileageReport.Mileage = value;
              SetMileage(mileageReport);
              _mileageReportList.Add(mileageReport);
            }
          }

          catch (IndexOutOfRangeException ex)
          {
            mileageReport.Message = $"Ошибка при чтении файла: {ex.Message}";
            _mileageReportList.Add(mileageReport);
          }
          catch (OverflowException)
          {
            mileageReport.Message = "Указан слишком большой пробег в файле";
            _mileageReportList.Add(mileageReport);
          }
        }
      }
      catch(Exception ex)
      {
        mileageReport.Message = $"Ошибка при открытии файла: {ex.Message}";
        _mileageReportList.Add(mileageReport);
      }
    }

    private Car GetCar(string grz)
    {
      return string.IsNullOrEmpty(grz)
        ? null
        : CarList.GetInstance().getItem(FormatGrz(grz));
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

    private void SetMileage(MileageReport mileageReport)
    {
      if (!int.TryParse(mileageReport.Mileage, out int count))
        return;

      var lastMileage = MileageList.getInstance().getItemByCarId(mileageReport.Car.ID);
      var loadMonthMileage = MileageList.getInstance().getItem(mileageReport.Car.ID, _date) ?? new Mileage(mileageReport.Car.ID, new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month)));

      if ((lastMileage?.Count ?? 0) > count)
      {
        mileageReport.Message = "Новое значение пробега меньше, чем уже внесённый в систему";
        return;
      }

      loadMonthMileage.SetCount(mileageReport.Mileage);
      loadMonthMileage.Save();
      mileageReport.Message = "Пробег загружен";
    }

    public IList<MileageReport> GetMileageReportList()
    {
      return _mileageReportList;
    }
  }
}
