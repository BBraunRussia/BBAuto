using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Windows.Forms;
using BBAuto.Domain.Services.Documents;
using BBAuto.Domain.Services.DriverInstruction;

namespace BBAuto
{
  public partial class formInstractionList : Form
  {
    private readonly Driver _driver;
    private readonly IDriverInstructionService _driverInstructionService;

    public formInstractionList(Driver driver)
    {
      InitializeComponent();

      _driver = driver;
      _driverInstructionService = new DriverInstructionService();
    }

    private void InstractionList_Load(object sender, EventArgs e)
    {
      LoadData();
      SetEnable();
    }

    private void LoadData()
    {
      dgvInstractions.DataSource = _driverInstructionService.GetDriverInstructionsByDriverId(_driver.ID);
    }

    private void SetEnable()
    {
      btnAdd.Enabled = User.IsFullAccess();
      btnDelete.Enabled = User.IsFullAccess();
    }

    private void add_Click(object sender, EventArgs e)
    {
      var driverInstractionForm = new DriverInstractionForm(new DriverInstruction(_driver.ID), _driverInstructionService);
      if (driverInstractionForm.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void delete_Click(object sender, EventArgs e)
    {
      var driverInstruction = dgvInstractions.Rows[dgvInstractions.SelectedCells[0].RowIndex].DataBoundItem as DriverInstruction;
      _driverInstructionService.DeleteDriverInstruction(driverInstruction?.Id ?? 0);

      LoadData();
    }

    private void dgvInstractions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (isCellNoHeader(e.RowIndex))
      {
        var driverInstruction = dgvInstractions.Rows[e.RowIndex].DataBoundItem as DriverInstruction;
        if (driverInstruction == null)
          return;

        if (dgvInstractions.Columns[e.ColumnIndex].HeaderText == "Название")
        {
          IDocumentsService documentsService = new DocumentsService();
          var document = documentsService.GetDocumentById(driverInstruction.DocumentId);
          WorkWithFiles.openFile(document.Path);
        }
        else
        {
          var driverInstractionForm = new DriverInstractionForm(driverInstruction, _driverInstructionService);
          if (driverInstractionForm.ShowDialog() == DialogResult.OK)
            LoadData();
        }
      }
    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }
  }
}
