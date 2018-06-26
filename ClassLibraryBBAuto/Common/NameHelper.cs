namespace BBAuto.Domain.Common
{
  public static class NameHelper
  {
    public static string GetNameShort(string fio)
    {
      var list = fio.Split(' ');
      return list.Length == 3
        ? $"{list[0]} {list[1][0]}.{list[2][0]}."
        : fio;
    }

    public static string GetNameGenetive(string fio, string sex)
    {
      var list = fio.Split(' ');
      if (list.Length == 3)
      {
        var secondName = list[0];
        var lastSymbol = secondName[secondName.Length - 1];

        if (sex == "мужской")
        {
          if (lastSymbol == 'в' || lastSymbol == 'н')
            secondName += "а";
        }
        else
        {
          if (lastSymbol == 'а')
            secondName = secondName.Substring(0, secondName.Length - 1) + "ой";
        }
        return string.Concat(secondName, " ", list[1][0].ToString(), ".", list[2][0].ToString(), ".");
      }

      return fio;
    }
  }
}
