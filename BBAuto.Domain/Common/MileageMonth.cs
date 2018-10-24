using BBAuto.Domain.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAuto.Domain.Common
{
    public class MileageMonth
    {
        private IProvider provider;

        public double PSN { get; set; }
        public double PSK { get; set; }
        public double Gas   {get; set;}
        public double GasBegin {get; set;}
        public double GasEnd  {get; set;}
        public double GasNorm {get; set;}
        public double Mileage {get; set;}

        private string Num;
        private string Date;

        public MileageMonth(string _number, string _date, double _gas, double _gasBegin, double _gasEnd, double _norm, double _psn, double _psk, double _mileage)
        {
            PSN = _psn;
            PSK = _psk;
            Gas = _gas;
            GasBegin = _gasBegin;
            GasEnd = _gasEnd;
            GasNorm = _norm;
            Mileage = _mileage;

            Num = _number;
            Date = _date;

            provider = Provider.GetProvider();
        }

        public string Save()
        {
            return provider.Insert("MileageMonth", Num, Date, Gas, GasBegin, GasEnd, GasNorm, PSN, PSK, Mileage);
        }

    }
}
