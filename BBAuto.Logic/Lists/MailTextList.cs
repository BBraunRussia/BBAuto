using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Lists
{
  public class MailTextList : MainList
  {
    private List<MailText> _list;
    private static MailTextList _uniqueInstance;

    private MailTextList()
    {
      _list = new List<MailText>();

      LoadFromSql();
    }

    public static MailTextList getInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new MailTextList();

      return _uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("MailText");

      foreach (DataRow row in dt.Rows)
      {
        MailText mailText = new MailText(row);
        Add(mailText);
      }
    }

    public void Add(MailText mailText)
    {
      if (_list.Exists(item => item == mailText))
        return;

      _list.Add(mailText);
    }

    public void Delete(int idMailText)
    {
      MailText mailText = getItem(idMailText);

      _list.Remove(mailText);

      mailText.Delete();
    }

    public MailText getItem(int id)
    {
      return _list.FirstOrDefault(t => t.Id == id);
    }

    public MailText getItemByType(MailTextType type)
    {
      var mailTexts = _list.Where(mailText => mailText.Id == (int) type);

      return (mailTexts.Count() == 0) ? null : mailTexts.First() as MailText;
    }

    public DataTable ToDataTable()
    {
      return CreateTable(_list);
    }

    private DataTable CreateTable(List<MailText> mailTexts)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");

      foreach (MailText mailText in mailTexts)
        dt.Rows.Add(mailText.ToRow());

      return dt;
    }
  }
}
