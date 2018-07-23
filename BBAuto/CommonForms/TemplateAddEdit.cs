using BBAuto.Domain.Common;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class TemplateAddEdit : Form
  {
    Template template;

    public TemplateAddEdit(Template template)
    {
      InitializeComponent();

      this.template = template;
    }

    private void TemplateAddEdit_Load(object sender, EventArgs e)
    {
      tbName.Text = template.Name;
      tbPath.Text = template.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      template.Name = tbName.Text;
      template.File = tbPath.Text;

      template.Save();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      tbPath.Text = getFilePath();
    }

    private string getFilePath()
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = false;
      ofd.ShowDialog();

      return ofd.FileName;
    }
  }
}
