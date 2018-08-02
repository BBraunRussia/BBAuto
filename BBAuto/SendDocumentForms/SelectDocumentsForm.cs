using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.Documents;

namespace BBAuto.SendDocumentForms
{
  public partial class SelectDocumentsForm : Form
  {
    private readonly IList<Driver> _drivers;

    public SelectDocumentsForm(IList<Driver> drivers)
    {
      InitializeComponent();

      _drivers = drivers;
    }

    private void SelectDocumentsForm_Load(object sender, System.EventArgs e)
    {
      IDocumentsService documentsService = new DocumentsService();
      chbList.DataSource = documentsService.GetList();
      chbList.DisplayMember = "Name";
      chbList.ValueMember = "Id";

      listBoxDrivers.DataSource = _drivers;
      listBoxDrivers.DisplayMember = "Name";
      listBoxDrivers.ValueMember = "ID";
    }

    private void btnSend_Click(object sender, System.EventArgs e)
    {
      var documentsForSend = chbList.CheckedItems.Cast<Document>().ToList();

      
    }
  }
}
