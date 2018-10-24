using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAuto.Domain.Lists
{
    public class MileageMonthList 
    {
        protected IProvider _provider;
        private List<MileageMonth> _list;

        public string PSN;
        public string PSK;
        public string Gas;
        public string GasBegin;
        public string GasEnd;
        public string GasNorm;
        public string Mileage;


        private static MileageMonthList _uniqueInstance;

        private MileageMonthList()
        {
            _provider = Provider.GetProvider();
            
            _list = new List<MileageMonth>();

            PSN = "";
            PSK = "";
            Gas = "";
            GasBegin = "";
            GasEnd = "";
            GasNorm = "";
            Mileage = "";

            loadFromSql();
        }

        public MileageMonthList(int carID, string date)
        {
            _provider = Provider.GetProvider();

            PSN = "";
            PSK = "";
            Gas = "";
            GasBegin = "";
            GasEnd = "";
            GasNorm = "";
            Mileage = "";

            loadFromSql(carID, date);
        }

        private void loadFromSql(int carID = 0, string date = "")
        {
            DataTable dt = _provider.DoOther("exec MileageMonth_Select @p1, @p2", carID, date);
            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    Mileage = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[0].ToString()), 0).ToString(); 
                    PSN = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[1].ToString()), 0).ToString(); 
                    PSK = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[2].ToString()), 0).ToString();
                    Gas = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[3].ToString()), 2).ToString(); 
                    GasBegin = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[4].ToString()), 2).ToString(); 
                    GasEnd = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[5].ToString()), 2).ToString(); 
                    GasNorm = Math.Round(Convert.ToDouble(dt.Rows[0].ItemArray[6].ToString()), 2).ToString(); 
                }
            }

            
        }
        
        
    }
}
