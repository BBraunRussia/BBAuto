using System;
using System.Windows.Forms;
using BBAuto.Domain.Common;

namespace BBAuto.CommonForms
{
  public partial class TemplateAddEdit : Form
  {
    readonly Template _template;

    public TemplateAddEdit(Template template)
    {
      InitializeComponent();

      this._template = template;
    }

    private void TemplateAddEdit_Load(object sender, EventArgs e)
    {
      tbName.Text = _template.Name;
      tbPath.Text = _template.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      _template.Name = tbName.Text;
      _template.File = tbPath.Text;

      _template.Save();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      var openFileDialog = new OpenFileDialog { Multiselect = false };
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        tbPath.Text = openFileDialog.FileName;
    }
  }
}
