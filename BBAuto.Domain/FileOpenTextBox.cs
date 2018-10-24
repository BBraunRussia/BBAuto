using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.Common;

namespace BBAuto.Domain
{
  public partial class FileOpenTextBox : UserControl
  {
    public FileOpenTextBox()
    {
      InitializeComponent();
      HaveFile();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = false;
      if (ofd.ShowDialog() == DialogResult.OK)
        tbFile.Text = ofd.FileName;
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
      WorkWithFiles.openFile(tbFile.Text);
    }

    private void tbFile_TextChanged(object sender, EventArgs e)
    {
      HaveFile();
    }

    private void HaveFile()
    {
      btnShow.Visible = (!string.IsNullOrEmpty(tbFile.Text));
      btnShow.Enabled = (!string.IsNullOrEmpty(tbFile.Text));
      label1.Text = (string.IsNullOrEmpty(tbFile.Text)) ? "Отсутствует" : "Имеется";
    }

    private void btnShow_EnabledChanged(object sender, EventArgs e)
    {
      btnShow.Enabled = (!string.IsNullOrEmpty(tbFile.Text));
    }
  }
}
