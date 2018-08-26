using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using System;
using System.Data;

namespace BBAuto.Domain.ForDriver
{
  public class Instruction : MainDictionary
  {
    public DateTime Date { get; set; }

    public string Name { get; set; }
    public int DriverId { get; set; }
    public int DocumentId { get; set; }

    public Instruction(int driverId)
    {
      DriverId = driverId;
      ID = 0;
    }

    public Instruction(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);
      Name = row.ItemArray[1].ToString();
      DateTime.TryParse(row.ItemArray[2].ToString(), out DateTime date);
      Date = date;

      int.TryParse(row.ItemArray[3].ToString(), out int driverId);
      DriverId = driverId;

      int.TryParse(row.ItemArray[3].ToString(), out int documentId);
      DocumentId = documentId;
    }

    internal override void Delete()
    {
      _provider.Delete("DriverInstruction", ID);
    }

    public override void Save()
    {
      ID = Convert.ToInt32(_provider.Insert("DriverInstruction", ID, DriverId, Name, Date, DocumentId));

      InstractionList.getInstance().Add(this);
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Name, Date};
    }

    public override string ToString()
    {
      return ID == 0 ? "нет данных" : string.Concat("№", Name, " дата ", Date);
    }
  }
}
