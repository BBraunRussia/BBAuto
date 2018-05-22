using System;
using System.Windows.Forms;
using BBAuto.Logic.Services.Dictionary;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.AddEdit
{
  public partial class OneStringDictionaryForm : Form, IOneStringDictionaryForm
  {
    private int _id;
    private IBasicDictionaryService _dictionaryService;

    public OneStringDictionaryForm()
    {
      InitializeComponent();
    }

    public DialogResult ShowDialog(int id, IBasicDictionaryService dictionaryService)
    {
      _dictionaryService = dictionaryService;
      _id = id;

      if (id != 0)
      {
        Text = "Редактирование";
        var item = _dictionaryService.GetItemById(id);
        tbName.Text = item.Value;
      }
      else
      {
        Text = "Добавление";
        tbName.Text = string.Empty;
      }
      
      return ShowDialog();
    }
    
    private void btnOK_Click(object sender, EventArgs e)
    {
      try
      {
        TrySave();
        Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void TrySave()
    {
      _dictionaryService.Save(_id, tbName.Text);
    }
  }
}
