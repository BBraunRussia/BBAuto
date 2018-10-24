using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.ForCar
{
    public class DTPFile : MainDictionary
    {
        public DTP DTP { get; set; }
        public string Name { get; set; }
        public string File { get; set; }

        public DTPFile(DTP dtp)
        {
            DTP = dtp;
            File = string.Empty;
        }

        public DTPFile(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int id;
            int.TryParse(row.ItemArray[0].ToString(), out id);
            ID = id;

            int idDTP;
            int.TryParse(row.ItemArray[1].ToString(), out idDTP);
            DTP = DTPList.getInstance().getItem(idDTP);

            Name = row.ItemArray[2].ToString();
            File = row.ItemArray[3].ToString();
            _fileBegin = File;
        }

        public override void Save()
        {
            int id;

            if (ID == 0)
            {
                int.TryParse(_provider.Insert("dtpFile", ID, DTP.ID, Name, File), out id);
                ID = id;
            }

            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "DTP", DTP.ID, string.Empty, Name);

            int.TryParse(_provider.Insert("dtpFile", ID, DTP.ID, Name, File), out id);
            ID = id;
        }

        internal override object[] getRow()
        {
            return new object[] { ID, Name, File == string.Empty ? string.Empty : "Просмотр" };
        }

        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("DtpFile", ID);
        }
    }
}
