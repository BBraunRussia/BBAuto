using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.ForCar;

namespace BBAuto.Domain.Lists
{
    public class GradeList : MainList
    {
        private static GradeList _uniqueInstance;
        private List<Grade> _list;

        private GradeList()
        {
            _list = new List<Grade>();

            loadFromSql();
        }

        public static GradeList getInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new GradeList();

            return _uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Grade");

            _list.Clear();

            foreach (DataRow row in dt.Rows)
            {
                Grade grade = new Grade(row);
                Add(grade);
            }
        }

        public void Add(Grade grade)
        {
            if (_list.Exists(item => item.ID == grade.ID))
                return;

            _list.Add(grade);
        }

        public Grade getItem(int id)
        {
            return _list.FirstOrDefault(g => g.ID == id);
        }

        public void Delete(int idGrade)
        {
            Grade grade = getItem(idGrade);

            _list.Remove(grade);

            grade.Delete();
        }
        
        public DataTable ToDataTable(int idModel)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");

            foreach (Grade grade in _list.Where(g => g.Model.ID == idModel))
                dt.Rows.Add(grade.getRow());

            return dt;
        }
    }
}
