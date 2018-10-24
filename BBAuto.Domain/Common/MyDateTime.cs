using System;

namespace BBAuto.Domain.Common
{
  public class MyDateTime
  {
    private DateTime _date;

    public MyDateTime(string sdate)
    {
      DateTime.TryParse(sdate, out _date);
    }

    public MyDateTime(DateTime date)
    {
      _date = date;
    }

    private enum MonthsGenitive
    {
      января = 1,
      февраля = 2,
      марта = 3,
      апреля = 4,
      мая = 5,
      июня = 6,
      июля = 7,
      августа = 8,
      сентября = 9,
      октября = 10,
      ноября = 11,
      декабря = 12
    };

    private enum MonthsNominative
    {
      январь = 1,
      февраль = 2,
      март = 3,
      апрель = 4,
      май = 5,
      июнь = 6,
      июль = 7,
      август = 8,
      сентябрь = 9,
      октябрь = 10,
      ноябрь = 11,
      декабрь = 12
    };

    private enum MonthsPrepositive
    {
      январе = 1,
      феврале = 2,
      марте = 3,
      апреле = 4,
      мае = 5,
      июне = 6,
      июле = 7,
      августе = 8,
      сентябре = 9,
      октябре = 10,
      ноябре = 11,
      декабре = 12
    };

    

    public string DaysRange => string.Concat(_date.Day.ToString(), "-", DateTime.DaysInMonth(_date.Year, _date.Month).ToString());

    public string Year => _date.Year.ToString();

    public override string ToString()
    {
      string month = _date.Month.ToString();
      if (_date.Month < 10)
        month = "0" + month;

      return string.Concat(_date.Year.ToString(), "-", month);
    }

    public string ToLongString()
    {
      var ms = (MonthsGenitive) _date.Month;

      return $"{_date.Day} {ms} {_date.Year} года";
    }

    public string MonthToStringGenitive()
    {
      var ms = (MonthsGenitive) _date.Month;

      return ms.ToString();
    }

    public string MonthToStringNominative()
    {
      var ms = (MonthsNominative) _date.Month;

      return ms.ToString();
    }

    public string MonthToStringPrepositive()
    {
      var ms = (MonthsPrepositive) _date.Month;

      return ms.ToString();
    }

    public string MonthSlashYear()
    {
      var month = _date.Month.ToString();
      if (_date.Month < 10)
        month = "0" + month;

      return string.Concat(month, "/", _date.Year.ToString().Substring(2, 2));
    }

    public static bool IsDate(string value)
    {
      return DateTime.TryParse(value, out DateTime _);
    }
  }
}
