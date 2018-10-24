using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Abstract
{
    public interface IActual
    {
        bool IsDateActual();
        bool IsHaveFile();
        bool IsActual();
    }
}
