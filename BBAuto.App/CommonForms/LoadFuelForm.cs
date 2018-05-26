using System;
using System.Windows.Forms;
using BBAuto.Logic.Loaders;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.CommonForms
{
  public partial class LoadFuelForm : Form, ILoadFuelForm
  {
    private readonly IFuelLoader _fuelLoader;

    public LoadFuelForm(IFuelLoader fuelLoader)
    {
      _fuelLoader = fuelLoader;

      InitializeComponent();
    }

    DialogResult ILoadFuelForm.ShowDialog()
    {
      return ShowDialog();
    }

    private void FormLoadFuel_Load(object sender, EventArgs e)
    {
      cbFirm.Items.Add(FuelReport.Петрол);
      cbFirm.Items.Add(FuelReport.Neste);
      cbFirm.SelectedIndex = 0;
      //cbFirm.Items.Add(FuelReport.Чеки);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(tbPath.Text))
      {
        MessageBox.Show("Не выбран файл", Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var list = _fuelLoader.Load(tbPath.Text, (FuelReport)cbFirm.SelectedItem);
      if (list.Count == 0)
      {
        MessageBox.Show("Загрузка завершена без ошибок", Captions.Load, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        MessageBox.Show("Загрузка завершена с ошибками", Captions.Load, MessageBoxButtons.OK, MessageBoxIcon.Information);
        foreach (var item in list)
        {
          MessageBox.Show(item, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
      DialogResult = DialogResult.OK;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog
      {
        Multiselect = false,
        Filter = "(Excel files)|*.xls"
      };
      if (ofd.ShowDialog() == DialogResult.OK)
      {
        tbPath.Text = ofd.FileName;
      }
    }
  }
}
