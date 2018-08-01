using System;
using System.Windows.Forms;
using BBAuto.Domain.Services.Documents;

namespace BBAuto.AddEdit
{
  public partial class DocumentForm : Form
  {
    private readonly Document _document;

    private readonly IDocumentsService _documentsService;

    public DocumentForm(
      Document document,
      IDocumentsService documentsService)
    {
      InitializeComponent();

      _document = document;
      _documentsService = documentsService;
    }

    private void DocumentForm_Load(object sender, EventArgs e)
    {
      tbName.Text = _document.Name;
      tbPath.Text = _document.Path;
      chbInstraction.Checked = _document.Instruction;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      var openFileDialog = new OpenFileDialog { Multiselect = false };
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        tbPath.Text = openFileDialog.FileName;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      _document.Name = tbName.Text;
      _document.Path = tbPath.Text;
      _document.Instruction = chbInstraction.Checked;

      _documentsService.Save(_document);
    }
  }
}
