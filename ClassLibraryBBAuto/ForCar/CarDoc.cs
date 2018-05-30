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
    public class CarDoc : MainDictionary
    {
        public string Name { get; set; }
        public string File { get; set; }

        public Car Car { get; set; }

        public CarDoc(Car car)
        {
            Car = car;
            ID = 0;
        }
        
        public CarDoc(DataRow row)
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

            Name = row.ItemArray[2].ToString();
            File = row.ItemArray[3].ToString();
            _fileBegin = File;
        }

        public override void Save()
        {
            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Documents", Name);

            int id;
            int.TryParse(_provider.Insert("CarDoc", ID, Car.ID, Name, File), out id);
            ID = id;
        }

        internal override object[] getRow()
        {
            return new object[] { ID, Name, (File == string.Empty) ? string.Empty : "Показать" };
        }

        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("CarDoc", ID);
        }
    }
}
