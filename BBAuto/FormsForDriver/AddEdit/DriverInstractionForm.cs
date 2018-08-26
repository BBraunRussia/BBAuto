using System;
using System.Linq;
using System.Windows.Forms;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Services.DriverInstruction;

namespace BBAuto
{
  public partial class DriverInstractionForm : Form
  {
    private readonly DriverInstruction _driverInstruction;

    private WorkWithForm _workWithForm;

    private readonly IDriverInstructionService _driverInstructionService;

    public DriverInstractionForm(
      DriverInstruction driverInstruction,
      IDriverInstructionService driverInstructionService)
    {
      InitializeComponent();

      _driverInstruction = driverInstruction;
      _driverInstructionService = driverInstructionService;
    }

    private void Instraction_AddEdit_Load(object sender, EventArgs e)
    {
      IDocumentsService documentsService = new DocumentsService();
      cbDocuments.DataSource = documentsService.GetList().Where(doc => doc.Instruction).ToList();
      cbDocuments.ValueMember = "Id";
      cbDocuments.DisplayMember = "Name";

      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_driverInstruction.Id == 0);
    }

    private void FillFields()
    {
      cbDocuments.SelectedValue = _driverInstruction.DocumentId;
      dtpDate.Value = _driverInstruction.Date;
    }

    private void save_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _driverInstruction.Date = dtpDate.Value.Date;
        _driverInstruction.DocumentId = (cbDocuments.SelectedItem as Document).Id;

        _driverInstructionService.Save(_driverInstruction);
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
