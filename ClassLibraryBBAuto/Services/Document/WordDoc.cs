using System;
using BBAuto.Domain.Common;
using Word = Microsoft.Office.Interop.Word;

namespace BBAuto.Domain.Services.Document
{
  public class WordDoc : OfficeDoc, IDisposable, IDocument
  {
    private Word.Application _wordApp;
    private Word.Document _wordDoc;

    public WordDoc(string name) :
      base(name)
    {
      Init();
    }

    private void Init()
    {
      _wordApp = new Word.Application();

      var fullPath = WorkWithFiles.GetFullPath(Name);

      _wordDoc = _wordApp.Documents.Open(fullPath);
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

    public void Close()
    {
      Dispose();
    }

    public void Dispose()
    {
      _wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

      _wordDoc.Close(Word.WdSaveOptions.wdDoNotSaveChanges, Word.WdOriginalFormat.wdWordDocument);
      _wordApp.Quit(Word.WdSaveOptions.wdDoNotSaveChanges, Word.WdOriginalFormat.wdWordDocument);

      ReleaseObject(_wordDoc);
      ReleaseObject(_wordApp);
    }

    public void SetValue(string search, string replace)
    {
      Word.Range myRange;
      object wMissing = Type.Missing;
      object textToFind = search;
      object replaceWith = replace;
      object replaceType = Word.WdReplace.wdReplaceAll;

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
      Word.Table wordTable = _wordDoc.Tables[tableIndex];
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
