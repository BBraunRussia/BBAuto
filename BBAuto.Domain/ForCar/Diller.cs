using BBAuto.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
    public class Diler : MainDictionary, IDictionaryMVC
    {
        public string Name { get; set; }
        public string Text { get; set; }
        
        public Diler()
        {
            ID = 0;
            Text = string.Empty;
        }

        public Diler(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            ID = id;

            Name = row.ItemArray[1].ToString();
            Text = row.ItemArray[2].ToString();
        }

        public override void Save()
        {
            _provider.Insert("Diller", ID, Name, Text);
        }

        internal override void Delete()
        {
            _provider.Delete("Diller", ID);
        }

        internal override object[] getRow()
        {
            return new object[3] { ID, Name, Text };
        }
    }
}
