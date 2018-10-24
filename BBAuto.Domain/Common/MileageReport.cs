using BBAuto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Common
{
    public class MileageReport
    {
        private Car _car;
        private string _message;

        public MileageReport(Car car, string message)
        {
            _car = car;
            _message = message;
        }

        public override string ToString()
        {
            return (_car == null) ? _message : _message + " " + _car.ToString();
        }

        public bool IsFailed
        {
            get { return _car == null; }
        }
    }
}
