using System;
using System.Windows.Forms;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Services.DriverTransponder;
using BBAuto.Domain.Services.Transponder;

namespace BBAuto.AddEdit
{
  public partial class TransponderForm : Form
  {
    private Transponder _transponder;
    private DriverTransponder _driverTransponder;

    private readonly IDriverTransponderService _driverTransponderService;
    private readonly ITransponderService _transponderService;

    private WorkWithForm _workWithForm;

    public TransponderForm(Transponder transponder)
    {
      InitializeComponent();
      
      _transponder = transponder;

      _transponderService = new TransponderService();
      _driverTransponderService = new DriverTransponderService();
    }

    private void TransponderForm_Load(object sender, System.EventArgs e)
    {
      LoadRegions();

      LoadDriverList();

      LoadData();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_transponder.Id == 0);
    }

    private void LoadRegions()
    {
      var regions = Regions.getInstance();
      cbRegion.DataSource = regions.ToDataTable();
      cbRegion.ValueMember = "id";
      cbRegion.DisplayMember = "Название";
    }

    private void LoadData()
    {
      tbNumber.Text = _transponder.Number;
      cbRegion.SelectedValue = _transponder.RegionId;
      
      chbLost.Checked = _transponder.Lost;

      tbComment.Text = _transponder.Comment;
    }

    private void LoadDriverList()
    {
      _dgv.DataSource = _driverTransponderService.GetDriversByTransponderId(_transponder.Id);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (Save())
          DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private bool Save()
    {
      try
      {
        CopyFields();

        var isNew = _transponder.Id == 0;

        _transponder = _transponderService.Save(_transponder);

        if (isNew)
        {
          _driverTransponder = _driverTransponderService.Save(new DriverTransponder {TransponderId = _transponder.Id});

          if (_driverTransponder == null)
            throw new NullReferenceException("Не удалось привязать водителя к транспондеру");
        }

        return true;
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show("Не удалось сохранить транспондер. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return false;
      }
    }

    private void CopyFields()
    {
      _transponder.Number = tbNumber.Text;

      if (int.TryParse(cbRegion.SelectedValue?.ToString(), out int regionId))
        _transponder.RegionId = regionId;
      else
        throw new NullReferenceException("Не выбран регион.");

      _transponder.Lost = chbLost.Checked;
      _transponder.Comment = tbComment.Text;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      var reloadDrivers = _driverTransponder == null;

      if (_driverTransponder == null && !Save())
        return;

      ShowDriverTransponderForm(_driverTransponder);

      if (reloadDrivers)
        LoadDriverList();
    }
    
    private void _dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var driverTransponder = _dgv.Rows[_dgv.SelectedCells[0].RowIndex].DataBoundItem as DriverTransponder;
      
      ShowDriverTransponderForm(driverTransponder);
    }

    private void ShowDriverTransponderForm(DriverTransponder driverTransponder)
    {
      var driverTransponderForm = new DriverTransponderForm(driverTransponder, _driverTransponderService);
      if (driverTransponderForm.ShowDialog() == DialogResult.OK)
        LoadDriverList();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить запись о водителе?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int driverTransponderId);

        _driverTransponderService.Delete(driverTransponderId);

        LoadDriverList();
      }
    }
  }
}
