using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
    public class DiagCard : MainDictionary
    {
        private int _notifacationSent;

        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string File { get; set; }
        public Car Car { get; set; }
        
        public bool IsNotificationSent
        {
            get { return Convert.ToBoolean(_notifacationSent); }
            private set { _notifacationSent = Convert.ToInt32(value); }
        }

        internal DiagCard(Car car)
        {
            ID = 0;
            Car = car;
            Number = string.Empty;
            Date = DateTime.Today;
            File = string.Empty;
        }

        public DiagCard(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            ID = id;

            int idCar;
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            Car = CarList.GetInstance().getItem(idCar);
            
            Number = row.ItemArray[2].ToString();

            DateTime date;
            DateTime.TryParse(row.ItemArray[3].ToString(), out date);
            Date = date;

            File = row.ItemArray[4].ToString();
            _fileBegin = File;
            int.TryParse(row.ItemArray[5].ToString(), out _notifacationSent);
        }

        public override void Save()
        {
            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "DiagCard", Number);

            ExecSave();
        }

        private void ExecSave()
        {
            int id;
            int.TryParse(_provider.Insert("DiagCard", ID, Car.ID, Number, Date, File, _notifacationSent), out id);
            ID = id;
        }

        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("DiagCard", ID);
        }

        internal override object[] getRow()
        {
            return new object[] { ID, Car.ID, Car.BBNumber, Car.Grz, Number, Date };
        }
        
        internal string ToMail()
        {
            IsNotificationSent = true;
            ExecSave();
            
            StringBuilder sb = new StringBuilder();
            sb.Append(Car.Grz);
            sb.Append(" ");
            sb.Append(Number);
            sb.Append(" ");
            sb.Append(Date.ToShortDateString());
            return sb.ToString();
        }
    }
}
