using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
    public class DTPFileList : MainList
    {
        private static DTPFileList uniqueInstance;
        private List<DTPFile> list;

        private DTPFileList()
        {
            list = new List<DTPFile>();

            loadFromSql();
        }

        public static DTPFileList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DTPFileList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("DTPFile");

            foreach (DataRow row in dt.Rows)
            {
                DTPFile dtpFile = new DTPFile(row);
                Add(dtpFile);
            }
        }
        
        public void Add(DTPFile dtpFile)
        {
            if (list.Exists(item => item.ID == dtpFile.ID))
                return;

            list.Add(dtpFile);
        }

        public DTPFile getItem(int id)
        {
            return list.FirstOrDefault(f => f.ID == id);
        }

        public void Delete(int idDTPFile)
        {
            DTPFile dtpFile = getItem(idDTPFile);

            list.Remove(dtpFile);

            dtpFile.Delete();
        }

        public DataTable ToDataTable(DTP dtp)
        {
            return createTable(list.Where(f => f.DTP.ID == dtp.ID));
        }

        private DataTable createTable(IEnumerable<DTPFile> dtpFiles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название файла");
            dt.Columns.Add("Просмотр скана");

            foreach (DTPFile dtpFile in dtpFiles)
                dt.Rows.Add(dtpFile.getRow());

            return dt;
        }
    }
}
