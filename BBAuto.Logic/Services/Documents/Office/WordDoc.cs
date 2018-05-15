using System;

namespace BBAuto.Logic.Services.Documents.Office
{
  public class WordDoc : OfficeDoc, IDisposable
  {
    private Microsoft.Office.Interop.Word.Application _wordApp;
    private Microsoft.Office.Interop.Word.Document _wordDoc;

    public WordDoc(string name) :
      base(name)
    {
      Init();
    }

    private void Init()
    {
      _wordApp = new Microsoft.Office.Interop.Word.Application();
      _wordDoc = _wordApp.Documents.Open(Name);
    }

    public void Show()
    {
      _wordApp.Visible = true;
    }

    public void Print()
    {
      _wordApp.PrintOut();

      Dispose();
    }

    public void Dispose()
    {
      _wordApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

      _wordDoc.Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Microsoft.Office.Interop.Word.WdOriginalFormat.wdWordDocument);
      _wordApp.Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges, Microsoft.Office.Interop.Word.WdOriginalFormat.wdWordDocument);

      releaseObject(_wordDoc);
      releaseObject(_wordApp);
    }

    public void setValue(string search, string replace)
    {
      Microsoft.Office.Interop.Word.Range myRange;
      object wMissing = Type.Missing;
      object textToFind = search;
      object replaceWith = replace;
      object replaceType = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

      for (int i = 1; i <= _wordApp.ActiveDocument.Sections.Count; i++)
      {
        myRange = _wordDoc.Sections[i].Range;

        myRange.Find.Execute(ref textToFind, ref wMissing, ref wMissing, ref wMissing, ref wMissing, ref wMissing,
          ref wMissing, ref wMissing, ref wMissing,
          ref replaceWith, ref replaceType, ref wMissing, ref wMissing, ref wMissing, ref wMissing);
      }
    }

    public void AddRowInTable(int tableIndex, params string[] Params)
    {
      Microsoft.Office.Interop.Word.Table wordTable = _wordDoc.Tables[tableIndex];
      wordTable.Rows.Add();

      int i = 1;

      foreach (string item in Params)
      {
        wordTable.Rows[wordTable.Rows.Count].Cells[i].Range.Text = item;
        i++;
      }
    }
  }
}
