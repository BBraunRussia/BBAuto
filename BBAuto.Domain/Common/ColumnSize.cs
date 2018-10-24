using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Static;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Common
{
    public class ColumnSize : MainDictionary
    {
        private const int DEFAULT_COLUMN_SIZE = 100;

        private int _idDriver;
        private int _idStatus;
        private int[] _arrayOfSize;

        private Status status { set { _idStatus = (int)value; } }

        internal ColumnSize(int idDriver, Status status)
        {
            _idDriver = idDriver;
            this.status = status;

            Init();
            for (int i = 0; i < _arrayOfSize.Count(); i++)
                _arrayOfSize[i] = DEFAULT_COLUMN_SIZE;
        }

        public ColumnSize(DataRow row)
        {
            Init();
            fillFields(row);
        }

        private void Init()
        {
            _arrayOfSize = new int[17];
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _idDriver);
            int.TryParse(row.ItemArray[1].ToString(), out _idStatus);

            for (int i = 2; i < row.ItemArray.Count(); i++)
            {
                int.TryParse(row.ItemArray[i].ToString(), out _arrayOfSize[i - 2]);
            }
        }

        public override void Save()
        {
            _provider.Insert("ColumnSize", _idDriver, _idStatus, _arrayOfSize[0], _arrayOfSize[1], _arrayOfSize[2], _arrayOfSize[3], _arrayOfSize[4], _arrayOfSize[5], _arrayOfSize[6], _arrayOfSize[7],
                _arrayOfSize[8], _arrayOfSize[9], _arrayOfSize[10], _arrayOfSize[11], _arrayOfSize[12], _arrayOfSize[13], _arrayOfSize[14], _arrayOfSize[15], _arrayOfSize[16]);

            ColumnSizeList columnSizeList = ColumnSizeList.getInstance();
            columnSizeList.Add(this);
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }

        internal bool IsEqualsIDs(Driver driver, Status status)
        {
            return driver.ID == _idDriver && _idStatus == (int)status;
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
