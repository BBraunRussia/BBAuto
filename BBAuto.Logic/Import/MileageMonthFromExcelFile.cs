using System;
using System.Runtime.InteropServices;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.DataBase;
using BBAuto.Logic.Logger;
using BBAuto.Logic.Services.Documents.Office;

namespace BBAuto.Domain.Senders
{
  public class MileageMonthFromExcelFile : IExcelImporter
  {
    public string FilePath { get; set; }
    private IProvider provider;

    public void StartImport()
    {
      try
      {
        using (ExcelDoc excel = new ExcelDoc(FilePath))
        {
          excel.SetList("Январь");
          if (excel.GetValue("M2") == null)
          {
            LogManager.Logger.Debug("В файле {file} на листе Январь в ячейке M2 должен быть указан год!", FilePath);
          }
          string year = excel.GetValue("M2").ToString();
          string date = year + "-01-01";

          int i = 2;
          string curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            int psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToInt32(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            int psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToInt32(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            int mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToInt32(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Январь. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }


          excel.SetList("Февраль");
          date = year + "-02-01";
          //ImportOneList(excel, date);

          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            int psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToInt32(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            int psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToInt32(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            int mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToInt32(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Февраль. Ошибка: {res}", i, res);


            i++;
            curCell = "D" + i;
          }

          excel.SetList("Март");
          date = year + "-03-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            int psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToInt32(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            int psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToInt32(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            int mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToInt32(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Март. Ошибка: {res}", i, res);


            i++;
            curCell = "D" + i;
          }

          excel.SetList("Апрель");
          date = year + "-04-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Апрель. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Май");
          date = year + "-05-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Май. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Июнь");
          date = year + "-06-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Июнь. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Июль");
          date = year + "-07-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Июль. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Август");
          date = year + "-08-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Август. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Сентябрь");
          date = year + "-09-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Сентябрь. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Октябрь");
          date = year + "-10-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Октябрь. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Ноябрь");
          date = year + "-11-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Ноябрь. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }

          excel.SetList("Декабрь");
          date = year + "-12-01";
          i = 2;
          curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            string res = mm.Save();
            if (res != "1")
              LogManager.Logger.Debug("Не записана строка {i} на листе Декабрь. Ошибка: {res}", i, res);

            i++;
            curCell = "D" + i;
          }
        }
      }
      catch (NullReferenceException ex)
      {
        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
      }
      catch (COMException ex)
      {
        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
      }
    }


    private void ImportOneList(ExcelDoc excel, string date)
    {
      try
      {
        using (excel)
        {
          double i = 2;
          string curCell = "D" + i;

          while (excel.GetValue(curCell) != null)
          {
            curCell = "D" + i;
            string number = excel.GetValue(curCell).ToString(); // номер машины

            curCell = "F" + i;
            double gasBegin = 0;
            if (excel.GetValue(curCell) != null)
              gasBegin = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на начало

            curCell = "E" + i;
            double gas = 0;
            if (excel.GetValue(curCell) != null)
              gas = Convert.ToDouble(excel.GetValue(curCell).ToString()); // получено

            curCell = "G" + i;
            double gasEnd = 0;
            if (excel.GetValue(curCell) != null)
              gasEnd = Convert.ToDouble(excel.GetValue(curCell).ToString()); // на конец

            curCell = "H" + i;
            double psn = 0;
            if (excel.GetValue(curCell) != null)
              psn = Convert.ToDouble(excel.GetValue(curCell).ToString()); // псн

            curCell = "I" + i;
            double psk = 0;
            if (excel.GetValue(curCell) != null)
              psk = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пск

            curCell = "J" + i;
            double mileage = 0;
            if (excel.GetValue(curCell) != null)
              mileage = Convert.ToDouble(excel.GetValue(curCell).ToString()); // пробег

            curCell = "K" + i;
            double norm = 0;
            if (excel.GetValue(curCell) != null)
              norm = Convert.ToDouble(excel.GetValue(curCell).ToString()); // норма

            MileageMonth mm = new MileageMonth(number, date, gas, gasBegin, gasEnd, norm, psn, psk, mileage);
            mm.Save();

            i++;
            curCell = "D" + i;
          }
        }
      }
      catch (NullReferenceException ex)
      {
        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
      }
      catch (COMException ex)
      {
        LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
      }
    }
  }
}
