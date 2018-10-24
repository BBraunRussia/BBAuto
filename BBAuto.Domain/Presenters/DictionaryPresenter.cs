using BBAuto.Domain.Abstract;
using BBAuto.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Presenter
{
    public class DictionaryPresenter
    {
        private IDictionaryMVC _dictionary;
        private IViewDictionary _view;

        public DictionaryPresenter(IViewDictionary view, IDictionaryMVC dictionary)
        {
            _view = view;
            _dictionary = dictionary;

            _view.SaveClick += new EventHandler<EventArgs>(OnClickSave);
            _view.LoadData += new EventHandler<EventArgs>(OnLoad);
        }

        private void OnSetName(object sender, EventArgs e)
        {
            _dictionary.Name = _view.InputName;
        }

        private void OnSetContacts(object sender, EventArgs e)
        {
            _dictionary.Text = _view.InputText;
        }

        private void OnClickSave(object sender, EventArgs e)
        {
            _dictionary.Text = _view.InputText;
            _dictionary.Name = _view.InputName;
            _dictionary.Save();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _view.SetName(_dictionary.Name);
            _view.SetText(_dictionary.Text);
        }
    }
}
