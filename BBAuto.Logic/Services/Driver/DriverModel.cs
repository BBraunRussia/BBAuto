using System;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Services.Driver
{
  public class DriverModel
  {
    public int Id { get; set; }
    public string Fio { get; set; }
    public int RegionId { get; set; }
    public DateTime? DateBirth { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public bool Fired { get; set; }
    public int? ExpSince { get; set; }
    public int PositionId { get; set; }
    public int? DeptId { get; set; }
    public string Login { get; set; }
    public int? OwnerId { get; set; }
    public string SuppyAddress { get; set; }
    public int Sex { get; set; }
    public bool Decret { get; set; }
    public DateTime? DateStopNotification { get; set; }
    public string Number { get; set; }
    public bool IsDriver { get; set; }
    public bool From1C { get; set; }

    public string SexString => Sex == 0 ? "мужской" : "женский";

    public string GetName(NameType nameType)
    {
      if (string.IsNullOrEmpty(Fio))
        return "(��� ��������)";

      if (nameType == NameType.Short)
        return GetNameShort();
      if (nameType == NameType.Genetive)
        return GetNameGenetive();

      return Fio;
    }

    private string GetNameShort()
    {
      var list = Fio.Split(' ');
      return list.Length == 3
        ? string.Concat(list[0], " ", list[1][0].ToString(), ".", list[2][0].ToString(), ".")
        : Fio;
    }

    private string GetNameGenetive()
    {
      var list = Fio.Split(' ');
      if (list.Length != 3)
        return Fio;

      var secondName = list[0];
      var lastSymbol = secondName[secondName.Length - 1];

      if (Sex == 0)
      {
        if (lastSymbol == '�' || lastSymbol == '�')
          secondName += "�";
      }
      else
      {
        if (lastSymbol == '�')
          secondName = secondName.Substring(0, secondName.Length - 1) + "��";
      }
      return string.Concat(secondName, " ", list[1][0].ToString(), ".", list[2][0].ToString(), ".");
    }

    public bool IsStopNotification()
    {
      return DateStopNotification == null || DateStopNotification < DateTime.Today;
    }
  }
}
