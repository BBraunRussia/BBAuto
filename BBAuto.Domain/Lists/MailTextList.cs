using System.Collections.Generic;
using System.Linq;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Static;

namespace BBAuto.Domain.Lists
{
  public class MailTextList : MainList<MailText>
  {
    private static MailTextList _uniqueInstance;

    public static MailTextList getInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new MailTextList());
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
    
    public void Delete(int idMailText)
    {
      MailText mailText = getItem(idMailText);

      _list.Remove(mailText);

      mailText.Delete();
    }

    public MailText getItem(int id)
    {
      return _list.FirstOrDefault(t => t.ID == id);
    }

    public MailText getItemByType(MailTextType type)
    {
      var mailTexts = _list.Where(mailText => mailText.ID == (int) type);

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
        dt.Rows.Add(mailText.getRow());

      return dt;
    }
  }
}
