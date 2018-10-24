using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Static;
using BBAuto.Domain.Entities;

namespace BBAuto.Domain.Lists
{
    public class ColumnSizeList : MainList
    {
        private static ColumnSizeList uniqueInstance;
        List<ColumnSize> list;

        private ColumnSizeList()
        {
            list = new List<ColumnSize>();

            loadFromSql();
        }

        public static ColumnSizeList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ColumnSizeList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("ColumnSize");

            foreach (DataRow row in dt.Rows)
            {
                ColumnSize columnSize = new ColumnSize(row);
                Add(columnSize);
            }
        }

        public void Add(ColumnSize columnSize)
        {
            if (list.Exists(item => item == columnSize))
                return;

            list.Add(columnSize);
        }

        public ColumnSize getItem(Driver driver, Status status)
        {
            var columnSizes = from columnSize in list
                              where columnSize.IsEqualsIDs(driver, status)
                              select columnSize;

            if (columnSizes.Count() > 0)
                return columnSizes.First() as ColumnSize;
            else
                return driver.CreateColumnSize(status);
        }

        public DataTable ToDataTable()
        {
            return CreateTable(list);
        }

        private DataTable CreateTable(List<ColumnSize> columnSizes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Номер счёта");
            dt.Columns.Add("Тип полиса");
            dt.Columns.Add("Собственник");
            dt.Columns.Add("Сумма");
            dt.Columns.Add("Согласование");
            dt.Columns.Add("Файл");

            foreach (ColumnSize columnSize in columnSizes)
                dt.Rows.Add(columnSize.getRow());

            return dt;
        }
    }
}
