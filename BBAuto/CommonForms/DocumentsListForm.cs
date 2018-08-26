using System;
using System.Windows.Forms;
using BBAuto.AddEdit;
using BBAuto.Domain.Services.Documents;

namespace BBAuto.CommonForms
{
  public partial class DocumentsListForm : Form
  {
    private readonly IDocumentsService _documentsService;

    public DocumentsListForm()
    {
      InitializeComponent();

      _documentsService = new DocumentsService();
    }

    private void DocumentsForSendListForm_Load(object sender, EventArgs e)
    {
      LoadData();
    }

    private void LoadData()
    {
      _dgv.DataSource = _documentsService.GetList();
      _dgv.Columns["Id"].Visible = false;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      OpenForm(new Document());
    }

    private void _dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var document = _dgv.Rows[_dgv.SelectedCells[0].RowIndex].DataBoundItem as Document;

      OpenForm(document);
    }

    private void OpenForm(Document document)
    {
      var documentForm = new DocumentForm(document, _documentsService);
      if (documentForm.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      var document = _dgv.Rows[_dgv.SelectedCells[0].RowIndex].DataBoundItem as Document;

      _documentsService.DeleteDocument(document);

      LoadData();
    }
  }
}
