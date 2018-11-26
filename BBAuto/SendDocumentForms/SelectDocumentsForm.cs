using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Services.Mail;

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

      listBoxDrivers.DataSource = _drivers.OrderBy(driver => driver.Name).ToList();
      listBoxDrivers.DisplayMember = "Name";
      listBoxDrivers.ValueMember = "ID";
    }

    private void btnSend_Click(object sender, System.EventArgs e)
    {
      if (chbList.CheckedItems.Count == 0)
      {
        MessageBox.Show("Для продолжения выберите хотя бы один элемент", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (MessageBox.Show("Подтверждаете отправку выбранных документов пользователям автомобилей", "Отправка документов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;

      var documentsForSend = chbList.CheckedItems.Cast<Document>().ToList();
      
      IMailService mailService = new MailService();

      mailService.SendDocuments(_drivers, documentsForSend);

      MessageBox.Show("Документы отправлены", "Отправка документов", MessageBoxButtons.OK, MessageBoxIcon.Information);
      DialogResult = DialogResult.OK;
    }
  }
}
