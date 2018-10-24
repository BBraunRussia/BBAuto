using BBAuto.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
    public class Model : MainDictionary
    {
        public int MarkID { get; private set; }
        public string Name { get; set; }
        
        public Model(int idMark)
        {
            ID = 0;
            MarkID = idMark;
        }

        public Model(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            ID = Convert.ToInt32(row.ItemArray[0]);
            Name = row.ItemArray[1].ToString();

            int idMark;
            int.TryParse(row.ItemArray[2].ToString(), out idMark);
            MarkID = idMark;
        }

        internal override void Delete()
        {
            _provider.Delete("Model", ID);
        }

        public override void Save()
        {
            _provider.Insert("Model", ID, Name, MarkID);
        }

        internal override object[] getRow()
        {
            return new object[2] { ID, Name };
        }
    }
}
