using System;

namespace BBAuto.Logic.Services.Documents.Office
{
  public class ExcelDoc : OfficeDoc, IDisposable
  {
    private Microsoft.Office.Interop.Excel.Application xlApp;
    private Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
    private Microsoft.Office.Interop.Excel.Worksheet xlSh;

    public ExcelDoc(string name)
      : base(name)
    {
      Init();
    }

    public ExcelDoc()
    {
      object misValue = System.Reflection.Missing.Value;

      xlApp = new Microsoft.Office.Interop.Excel.Application();

      xlWorkBook = xlApp.Workbooks.Add(misValue);
      xlSh = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
    }

    private void Init()
    {
      xlApp = new Microsoft.Office.Interop.Excel.Application
      {
        DisplayAlerts = false,
        EnableEvents = false
      };
      
      xlWorkBook = xlApp.Workbooks.Open(Name, 0, true, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
      xlSh = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
    }

    public void SetValue(int rowIndex, int columnIndex, string value)
    {
      xlSh.Cells[rowIndex, columnIndex] = value;
    }

    public void SetColumnWidth(string columnName, double width)
    {
      Microsoft.Office.Interop.Excel.Range range = xlSh.Range[columnName + "1", System.Type.Missing];
      range.EntireColumn.ColumnWidth = width;
    }

    public object GetValue1(string cell)
    {
      return xlSh.get_Range(cell, cell).Value;
    }

    public object GetValue(string cell)
    {
      return xlSh.get_Range(cell, cell).Value2;
    }

    public void SetList(string pageName)
    {
      try
      {
        xlSh = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(pageName);
      }
      catch
      {
        throw new IndexOutOfRangeException();
      }
    }

    public void SetList(int pageIndex)
    {
      xlSh = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(pageIndex);
    }

    public void Show()
    {
      xlApp.Visible = true;
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

      releaseObject(xlSh);
      releaseObject(xlWorkBook);
      releaseObject(xlApp);
    }

    internal void Print()
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

    internal void CopyRange(string copingCell1, string copingCell2, string pastingCell)
    {
      xlSh.Range[copingCell1, copingCell2].Copy();
      xlSh.Range[pastingCell, System.Type.Missing].Select();
      xlSh.Paste();
      xlApp.CutCopyMode = 0;
    }
  }
}
