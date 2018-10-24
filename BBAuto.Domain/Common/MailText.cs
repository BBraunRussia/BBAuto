using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;

namespace BBAuto.Domain.Common
{
    public class MailText : MainDictionary, IDictionaryMVC
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public MailText()
        {
            ID = 0;
            Name = string.Empty;
            Text = string.Empty;
        }

        public MailText(DataRow row)
        {
            FillFields(row);
        }

        private void FillFields(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            ID = id;

            Name = row.ItemArray[1].ToString();
            Text = row.ItemArray[2].ToString();
        }

        public override void Save()
        {
            int id;
            int.TryParse(_provider.Insert("MailText", ID, Name, Text), out id);
            ID = id;

            MailTextList mailTextList = MailTextList.getInstance();
            mailTextList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("MailText", ID);
        }

        internal override object[] getRow()
        {
            return new object[2] { ID, Name };
        }
    }
}
