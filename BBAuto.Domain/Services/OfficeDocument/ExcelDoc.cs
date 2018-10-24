using System;
using System.IO;
using BBAuto.Domain.Common;
using Excel = Microsoft.Office.Interop.Excel;

namespace BBAuto.Domain.Services.OfficeDocument
{
  public class ExcelDoc : OfficeDoc, IDisposable, IExcelDoc
  {
    private Excel.Application xlApp;
    private Excel.Workbook xlWorkBook;
    private Excel.Worksheet xlSh;

    public ExcelDoc(string name)
      : base(name)
    {
      Init();
    }

    public ExcelDoc()
    {
      object misValue = System.Reflection.Missing.Value;

      xlApp = new Excel.Application();

      xlWorkBook = xlApp.Workbooks.Add(misValue);
      xlSh = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
    }

    private void Init()
    {
      xlApp = new Excel.Application
      {
        DisplayAlerts = false,
        EnableEvents = false
      };

      var fullPath = File.Exists(Name) ? Name : WorkWithFiles.GetFullPath(Name);

      xlWorkBook = xlApp.Workbooks.Open(fullPath, 0, true, 5, "", "", true,
        Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
      xlSh = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
    }

    public void setValue(int rowIndex, int columnIndex, string value)
    {
      xlSh.Cells[rowIndex, columnIndex] = value;
    }

    public void setColumnWidth(string columnName, double width)
    {
      Excel.Range range = xlSh.Range[columnName + "1", System.Type.Missing];
      range.EntireColumn.ColumnWidth = width;
    }

    public object getValue1(string cell)
    {
      return xlSh.get_Range(cell, cell).Value;
    }

    public object getValue(string cell)
    {
      return xlSh.get_Range(cell, cell).Value2;
    }

    public void SetList(string pageName)
    {
      try
      {
        xlSh = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(pageName);
      }
      catch
      {
        throw new IndexOutOfRangeException();
      }
    }

    public void SetList(int pageIndex)
    {
      xlSh = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(pageIndex);
    }

    public void Show()
    {
      xlApp.Visible = true;
    }

    public void Close()
    {
      Dispose();
    }

    public void AutoFitColumns()
    {
      xlSh.Columns.AutoFit();
    }

    public void Dispose()
    {
      object misValue = System.Reflection.Missing.Value;

      xlApp.DisplayAlerts = false;
      xlApp.EnableEvents = false;

      xlWorkBook.Close(false, misValue, misValue);

      xlApp.Quit();

      System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

      ReleaseObject(xlSh);
      ReleaseObject(xlWorkBook);
      ReleaseObject(xlApp);
    }

    public void Print()
    {
      object misValue = System.Reflection.Missing.Value;

      xlSh.Columns.AutoFit();

      xlSh.PrintOutEx(1, misValue, 1, false, misValue, misValue, misValue, misValue);

      Dispose();
    }

    internal void SetHeader(string text)
    {
      xlSh.PageSetup.LeftHeader = text + "\n" + DateTime.Today.ToShortDateString();
    }

    public void CopyRange(string copingCell1, string copingCell2, string pastingCell)
    {
      xlSh.Range[copingCell1, copingCell2].Copy();
      xlSh.Range[pastingCell, System.Type.Missing].Select();
      xlSh.Paste();
      xlApp.CutCopyMode = 0;
    }
  }
}
