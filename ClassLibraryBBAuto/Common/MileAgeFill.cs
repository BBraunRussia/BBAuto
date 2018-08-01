using System;
using System.IO;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.OfficeDocument;

namespace BBAuto.Domain.Common
{
  public class MileageFill
  {
    private string[,] literal = new string[,]
    {
      {"A", "А"}, {"B", "В"}, {"E", "Е"}, {"K", "К"}, {"M", "М"}, {"H", "Н"}, {"O", "О"}, {"P", "Р"}, {"C", "С"},
      {"T", "Т"}, {"Y", "У"}, {"X", "Х"}, {"RUS", ""}, {"/", ""}
    };

    private DateTime _date;
    private MileageReportList _mileageReportList;

    private string _folder;

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
                DriverCarList driverCarList = DriverCarList.getInstance();
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

      CarList carList = CarList.GetInstance();
      return carList.getItem(FormatGRZ(grz));
    }

    private string FormatGRZ(string value)
    {
      value = value.ToUpper();

      for (int i = 0; i < 14; i++)
      {
        value = value.Replace(literal[i, 0], literal[i, 1]);
      }

      return value;
    }

    private void SetMileage(Car car, string value)
    {
      int count;
      int.TryParse(value, out count);

      if (count == 0)
        return;

      MileageList mileageList = MileageList.getInstance();
      Mileage mileage = mileageList.getItem(car);

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
