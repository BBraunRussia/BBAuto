using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Views
{
    public interface IViewDictionary
    {
        string InputName { get; }
        string InputText { get; }

        void SetName(string name);
        void SetText(string text);

        event EventHandler<EventArgs> LoadData;
        event EventHandler<EventArgs> SaveClick;
    }
}
