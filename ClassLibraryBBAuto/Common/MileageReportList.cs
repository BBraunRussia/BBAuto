﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Common
{
    public class MileageReportList : IEnumerable
    {
        private List<MileageReport> _list;

        public MileageReportList()
        {
            _list = new List<MileageReport>();
        }

        public void Add(MileageReport item)
        {
            _list.Add(item);
        }

        public string GetReportMessage()
        {
            int countFailed = 0;

            foreach (MileageReport item in this)
            {
                if (item.IsFailed)
                    countFailed++;
            }

            int countSuccess = _list.Count - countFailed;

            return string.Concat("Всего обработано файлов: ", _list.Count.ToString(), ". Из них пробеги удалось считать из ", countSuccess.ToString(), ". Не удалось считать из ", countFailed.ToString());
        }

        public IEnumerator GetEnumerator()
        {
            return new MileageReportListEnumerator(this);
        }

        private class MileageReportListEnumerator : IEnumerator
        {
            private MileageReportList _mileageReportList;
            private int _index;

            public MileageReportListEnumerator(MileageReportList mileageReportList)
            {
                _mileageReportList = mileageReportList;
                _index = -1;
            }

            public object Current
            {
                get { return _mileageReportList._list[_index]; }
            }

            public bool MoveNext()
            {
                if ((_index + 1) < _mileageReportList._list.Count)
                {
                    _index++;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
