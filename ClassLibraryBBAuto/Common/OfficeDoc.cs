using System;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace BBAuto.Domain.Common
{
  internal class WordDoc : OfficeDoc, IDisposable
  {
    private Word.Application wordApp;
    private Word.Document wordDoc;

    public WordDoc(string name) :
      base(name)
    {
      Init();
    }

    private void Init()
    {
      wordApp = new Word.Application();
      wordDoc = wordApp.Documents.Open(name);
    }

    public void Show()
    {
      wordApp.Visible = true;
    }

    public void Print()
    {
      wordApp.PrintOut();

      Dispose();
    }

    public void Dispose()
    {
      wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

      ((Word._Document) wordDoc).Close(Word.WdSaveOptions.wdDoNotSaveChanges, Word.WdOriginalFormat.wdWordDocument);
      ((Word._Application) wordApp).Quit(Word.WdSaveOptions.wdDoNotSaveChanges, Word.WdOriginalFormat.wdWordDocument);

      releaseObject(wordDoc);
      releaseObject(wordApp);
    }

    public void setValue(string search, string replace)
    {
      Word.Range myRange;
      object wMissing = Type.Missing;
      object textToFind = search;
      object replaceWith = replace;
      object replaceType = Word.WdReplace.wdReplaceAll;

      for (int i = 1; i <= wordApp.ActiveDocument.Sections.Count; i++)
      {
        myRange = wordDoc.Sections[i].Range;

        myRange.Find.Execute(ref textToFind, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing,
          ref wMissing, ref wMissing, ref wMissing,
          ref replaceWith, ref replaceType, ref wMissing, ref wMissing, ref wMissing, ref wMissing);
      }
    }

    public void AddRowInTable(int tableIndex, params string[] Params)
    {
      Word.Table wordTable = wordDoc.Tables[tableIndex];
      wordTable.Rows.Add();

      int i = 1;

      foreach (string item in Params)
      {
        wordTable.Rows[wordTable.Rows.Count].Cells[i].Range.Text = item;
        i++;
      }
    }
  }

  internal class ExcelDoc : OfficeDoc, IDisposable
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
      xlSh = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);
    }

    private void Init()
    {
      xlApp = new Excel.Application();

      xlApp.DisplayAlerts = false;
      xlApp.EnableEvents = false;

      xlWorkBook = xlApp.Workbooks.Open(name, 0, true, 5, "", "", true,
        Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
      xlSh = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);
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
        xlSh = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(pageName);
      }
      catch
      {
        throw new IndexOutOfRangeException();
      }
    }

    public void SetList(int pageIndex)
    {
      xlSh = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(pageIndex);
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

  internal class OfficeDoc
  {
    protected string name;

    protected OfficeDoc()
    {
    }

    protected OfficeDoc(string name)
    {
      this.name = name;
    }

    protected void releaseObject(object obj)
    {
      try
      {
        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        obj = null;
      }
      catch
      {
        obj = null;
      }
      finally
      {
        GC.Collect();
      }
    }
  }
}
