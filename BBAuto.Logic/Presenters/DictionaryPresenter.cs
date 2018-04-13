using System;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Views;

namespace BBAuto.Logic.Presenters
{
  public class DictionaryPresenter
  {
    private IDictionaryMvc _dictionary;
    private IViewDictionary _view;

    public DictionaryPresenter(IViewDictionary view, IDictionaryMvc dictionary)
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
