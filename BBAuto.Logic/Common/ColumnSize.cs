using System;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Common
{
  public class ColumnSize : MainDictionary
  {
    private const int DefaultColumnSize = 100;

    private int _idDriver;
    private int _idStatus;
    private int[] _arrayOfSize;

    private Status Status
    {
      set => _idStatus = (int) value;
    }

    internal ColumnSize(int idDriver, Status status)
    {
      _idDriver = idDriver;
      Status = status;

      Init();
      for (var i = 0; i < _arrayOfSize.Count(); i++)
        _arrayOfSize[i] = DefaultColumnSize;
    }

    public ColumnSize(DataRow row)
    {
      Init();
      FillFields(row);
    }

    private void Init()
    {
      _arrayOfSize = new int[17];
    }

    private void FillFields(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out _idDriver);
      int.TryParse(row.ItemArray[1].ToString(), out _idStatus);

      for (var i = 2; i < row.ItemArray.Count(); i++)
      {
        int.TryParse(row.ItemArray[i].ToString(), out _arrayOfSize[i - 2]);
      }
    }

    public override void Save()
    {
      Provider.Insert("ColumnSize", _idDriver, _idStatus, _arrayOfSize[0], _arrayOfSize[1], _arrayOfSize[2],
        _arrayOfSize[3], _arrayOfSize[4], _arrayOfSize[5], _arrayOfSize[6], _arrayOfSize[7],
        _arrayOfSize[8], _arrayOfSize[9], _arrayOfSize[10], _arrayOfSize[11], _arrayOfSize[12], _arrayOfSize[13],
        _arrayOfSize[14], _arrayOfSize[15], _arrayOfSize[16]);

      var columnSizeList = ColumnSizeList.getInstance();
      columnSizeList.Add(this);
    }

    internal override object[] ToRow()
    {
      throw new NotImplementedException();
    }

    internal bool IsEqualsIDs(Driver driver, Status status)
    {
      return driver.Id == _idDriver && _idStatus == (int) status;
    }

    public int GetSize(int index)
    {
      return _arrayOfSize[index];
    }

    public void SetSize(int index, int width)
    {
      _arrayOfSize[index] = width;
      Save();
    }
  }
}
