using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Common;
using BBAuto.Domain.Services.OfficeDocument;
using BBAuto.FormsForCar.AddEdit;

namespace BBAuto
{
  public partial class CarForm : Form
  {
    private System.Drawing.Point _curPosition;
    private readonly Car _car;
    private STS _sts;
    private PTS _pts;

    private bool _load;

    private readonly DiagCardList _diagCardList;
    private readonly DriverCarList _driverCarList;
    private readonly DriverList _driverList;
    private readonly DTPList _dtpList;
    private readonly InvoiceList _invoiceList;
    private readonly MileageList _mileageList;
    private readonly PolicyList _policyList;
    private readonly RepairList _repairList;
    private readonly ViolationList _violationList;
    private readonly ShipPartList _shipPartList;

    private WorkWithForm _workWithForm;

    public CarForm(Car car)
    {
      InitializeComponent();

      _car = car;

      _diagCardList = DiagCardList.getInstance();
      _driverCarList = DriverCarList.GetInstance();
      _driverList = DriverList.getInstance();
      _dtpList = DTPList.getInstance();
      _invoiceList = InvoiceList.getInstance();
      _mileageList = MileageList.getInstance();
      _policyList = PolicyList.getInstance();
      _repairList = RepairList.getInstance();
      _violationList = ViolationList.getInstance();
      _shipPartList = ShipPartList.getInstance();
    }

    private void Car_AddEdit_Load(object sender, EventArgs e)
    {
      loadData();

      SetWindowHeader();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += SetNotEditableItems;
      _workWithForm.SetEditMode(_car.ID == 0 || (!_car.IsGet));


      /*TODO: Столярова видит основную инфу */
      if (User.GetDriver().UserRole == RolesList.AccountantWayBill)
      {
        foreach (TabPage tab in tabControl1.TabPages)
        {
          tab.Parent = null;
        }

        tabMileage.Parent = tabControl1;
        tabMain.Parent = tabControl1;
      }
    }

    private void loadData()
    {
      _load = false;
      loadOneStringDictionary(cbMark, "Mark");
      _load = true;
      LoadModel();
      _load = false;
      loadOneStringDictionary(cbColor, "Color");
      loadOneStringDictionary(cbRegionBuy, "Region");
      loadOneStringDictionary(cbRegionUsing, "Region");
      loadOneStringDictionary(cbOwner, "Owner");
      loadOneStringDictionary(cbDealer, "Diller");

      Region region = GetRegion();

      cbDriver.DataSource = _driverList.ToDataTableByRegion(region);
      cbDriver.DisplayMember = "ФИО";
      cbDriver.ValueMember = "id";
      _load = true;

      FillFields();

      loadCarDoc();
    }

    private Region GetRegion()
    {
      int.TryParse(cbRegionUsing.SelectedValue.ToString(), out int idRegion);
      var regionList = RegionList.getInstance();
      return regionList.getItem(idRegion);
    }

    private void SetNotEditableItems(Object sender, EditModeEventArgs e)
    {
      if (_car.IsGet)
      {
        cbMark.Enabled = false;
        cbModel.Enabled = false;
        cbGrade.Enabled = false;
        mtbGRZ.ReadOnly = true;
        tbVin.ReadOnly = true;
        tbENumber.ReadOnly = true;
        tbBodyNumber.ReadOnly = true;
        tbYear.ReadOnly = true;
        cbColor.Enabled = false;

        cbOwner.Enabled = false;
        cbRegionBuy.Enabled = false;
        cbRegionUsing.Enabled = false;
        cbDriver.Enabled = false;
        dtpDateOrder.Enabled = false;
        dtpDateGet.Enabled = false;
        tbCost.ReadOnly = true;
        tbDOP.ReadOnly = true;
        tbEvents.ReadOnly = true;
        cbDealer.Enabled = false;
        tbDealerContacts.Enabled = false;
        chbLising.Enabled = false;
        mtbLising.Enabled = false;
      }

      tbInvertoryNumber.ReadOnly = true;
      tbBbNumber.Enabled = false;
    }

    private void loadOneStringDictionary(ComboBox combo, string name)
    {
      combo.DataSource = OneStringDictionary.getDataTable(name);
      combo.DisplayMember = "Название";
      combo.ValueMember = name + "_id";
    }

    private void SetWindowHeader()
    {
      Text = string.Concat("Карточка автомобиля: ", _car.ToString());
    }

    private void cbMark_SelectedValueChanged(object sender, EventArgs e)
    {
      if (_load)
        LoadModel();
    }

    private void LoadModel()
    {
      if (!_load)
        return;

      _load = false;

      int.TryParse(cbMark.SelectedValue?.ToString(), out int idMark);

      var models = ModelList.getInstance();

      cbModel.DataSource = models.ToDataTable(idMark);
      cbModel.DisplayMember = "Название";
      cbModel.ValueMember = "id";
      _load = true;
      LoadGrade();
    }

    private void LoadGrade()
    {
      if (!_load)
        return;

      int.TryParse(cbModel.SelectedValue?.ToString(), out int idModel);
      var grades = GradeList.getInstance();

      cbGrade.DataSource = grades.ToDataTable(idModel);
      cbGrade.DisplayMember = "Название";
      cbGrade.ValueMember = "id";
    }

    private void FillFields()
    {
      cbMark.SelectedValue = _car.Mark?.ID.ToString() ?? "0";
      cbModel.SelectedValue = _car.ModelID;
      cbGrade.SelectedValue = _car.GradeID;
      /* когда Audi не заполняется таблица с инфо о машине */
      if (dgvCarInfo.DataSource == null)
        ChangedGrade();
      cbColor.SelectedValue = _car.ColorID;

      tbBbNumber.Text = _car.BBNumber;
      tbVin.Text = _car.vin;
      tbYear.Text = _car.Year;
      tbENumber.Text = _car.eNumber;
      tbBodyNumber.Text = _car.bodyNumber;
      mtbGRZ.Text = _car.Grz;
      cbOwner.SelectedValue = _car.ownerID;
      cbRegionBuy.SelectedValue = _car.RegionBuyID;
      cbRegionUsing.SelectedValue = _car.RegionUsingId;
      cbDriver.SelectedValue = _car.driverID;
      cbDealer.SelectedValue = _car.idDiller;
      dtpDateOrder.Value = _car.dateOrder;
      chbIsGet.Checked = _car.IsGet;
      dtpDateGet.Value = _car.dateGet;
      tbEvents.Text = _car.events;
      tbCost.Text = _car.cost.ToString();
      tbDOP.Text = _car.dop;

      Driver driver = _driverCarList.GetDriver(_car) ?? new Driver();
      llDriver.Text = driver.FullName;

      //если не назначен водитель
      if (driver.Region != null)
      {
        lbRegion.Text = driver.Region.Name;
      }

      var ptsList = PTSList.getInstance();
      _pts = ptsList.getItem(_car);
      mtbNumberPTS.Text = _pts.Number;
      dtpDatePTS.Value = _pts.Date;
      tbGiveOrgPTS.Text = _pts.GiveOrg;
      TextBox tbFilePTS = ucFilePTS.Controls["tbFile"] as TextBox;
      tbFilePTS.Text = _pts.File;

      var stsList = STSList.getInstance();
      _sts = stsList.getItem(_car);
      mtbNumberSTS.Text = _sts.Number;
      dtpDateSTS.Value = _sts.Date;
      tbGiveOrgSTS.Text = _sts.GiveOrg;
      TextBox tbFileSTS = ucFileSTS.Controls["tbFile"] as TextBox;
      tbFileSTS.Text = _sts.File;

      var mileage = _mileageList.getItem(_car);
      if (mileage != null)
        lbMileage.Text = mileage.ToString();

      ChangeDiler(_car.idDiller);

      if (_car.Lising == string.Empty)
      {
        lbLising.Visible = false;
        mtbLising.Visible = false;
        chbLising.Checked = false;
      }
      else
      {
        lbLising.Visible = true;
        mtbLising.Visible = true;
        chbLising.Checked = true;
        mtbLising.Text = _car.Lising;
      }

      tbInvertoryNumber.Text = _car.InvertoryNumber;
    }

    private void cbDiller_SelectedIndexChanged(object sender, EventArgs e)
    {
      if ((_load) && (cbDealer.SelectedValue != null))
        ChangeDiler(Convert.ToInt32(cbDealer.SelectedValue));
    }

    private void ChangeDiler(int idDiler)
    {
      var diller = DilerList.getInstance().getItem(idDiler);

      tbDealerContacts.Text = diller?.Text;
    }

    private void model_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadGrade();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (CopyFields())
        {
          _car.Save();
          DialogResult = DialogResult.OK;
        }
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private bool CopyFields()
    {
      if (cbGrade.SelectedValue == null)
      {
        MessageBox.Show("Для сохранения необходимо выбрать комплектацию", "Недостаточно данных", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        return false;
      }

      int idMark;
      int.TryParse(cbMark.SelectedValue.ToString(), out idMark);
      _car.Mark = MarkList.getInstance().getItem(idMark);
      _car.ModelID = cbModel.SelectedValue.ToString();
      _car.GradeID = cbGrade.SelectedValue.ToString();
      _car.ColorID = cbColor.SelectedValue;
      _car.vin = tbVin.Text;
      _car.Grz = mtbGRZ.Text;
      _car.eNumber = tbENumber.Text.ToUpper();
      _car.bodyNumber = tbBodyNumber.Text.ToUpper();
      _car.Year = tbYear.Text;

      _car.ownerID = cbOwner.SelectedValue;
      _car.RegionBuyID = cbRegionBuy.SelectedValue;

      if (int.TryParse(cbRegionUsing.SelectedValue?.ToString(), out int regionUsingId))
        _car.RegionUsingId = regionUsingId;

      _car.driverID = cbDriver.SelectedValue;
      _car.dateOrder = dtpDateOrder.Value;
      _car.IsGet = chbIsGet.Checked;
      _car.dateGet = dtpDateGet.Value;
      _car.cost = Convert.ToDouble(tbCost.Text);
      _car.dop = tbDOP.Text;
      _car.events = tbEvents.Text;
      _car.idDiller = Convert.ToInt32(cbDealer.SelectedValue);

      _pts.Number = mtbNumberPTS.Text;
      _pts.Date = Convert.ToDateTime(dtpDatePTS.Text);
      _pts.GiveOrg = tbGiveOrgPTS.Text;

      TextBox tbFilePTS = ucFilePTS.Controls["tbFile"] as TextBox;
      _pts.File = tbFilePTS.Text;
      _pts.Save();

      _sts.Number = mtbNumberSTS.Text;
      _sts.Date = Convert.ToDateTime(dtpDateSTS.Text);
      _sts.GiveOrg = tbGiveOrgSTS.Text;

      TextBox tbFileSTS = ucFileSTS.Controls["tbFile"] as TextBox;
      _sts.File = tbFileSTS.Text;
      _sts.Save();

      _car.Lising = (chbLising.Checked) ? mtbLising.Text : string.Empty;

      _car.InvertoryNumber = tbInvertoryNumber.Text;

      return true;
    }

    private void LoadInvoice()
    {
      _dgvInvoice.DataSource = _invoiceList.ToDataTable(_car);

      FormatDgvInvoice();
    }

    private void FormatDgvInvoice()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvInvoice);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
      dgvFormated.HideColumn(3);
      dgvFormated.Format(Status.Invoice);
    }

    private void chbIsGet_CheckedChanged(object sender, EventArgs e)
    {
      labelDateGet.Visible = chbIsGet.Checked;
      dtpDateGet.Visible = chbIsGet.Checked;
    }

    private void dateOrder_ValueChanged(object sender, EventArgs e)
    {
      dtpDateGet.Value = dtpDateOrder.Value;
    }

    private void cbRegionUsing_SelectedIndexChanged(object sender, EventArgs e)
    {
      if ((_load) && (cbRegionUsing.SelectedValue != null))
      {
        Region region = GetRegion();

        cbDriver.DataSource = _driverList.ToDataTableByRegion(region);
      }
    }

    private string getFilePath()
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = false;
      ofd.ShowDialog();

      return ofd.FileName;
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadPage();
    }

    private void LoadPage()
    {
      if (tabControl1.SelectedTab == tabInvoice)
        LoadInvoice();
      else if (tabControl1.SelectedTab == tabPolicy)
        loadPolicy();
      else if (tabControl1.SelectedTab == tabDTP)
        loadDTP();
      else if (tabControl1.SelectedTab == tabViolation)
        loadViolation();
      else if (tabControl1.SelectedTab == tabDiagCard)
        loadDiagCard();
      else if (tabControl1.SelectedTab == tabMileage)
        loadMileage();
      else if (tabControl1.SelectedTab == tabRepair)
        loadRepair();
      else if (tabControl1.SelectedTab == tabShipParts)
        loadShipPart();
      else if (tabControl1.SelectedTab == tabDrivers)
        LoadDrivers();
    }

    private void loadPolicy()
    {
      _dgvPolicy.DataSource = _policyList.ToDataTable(_car);

      formatDGVPolicy();
    }

    private void formatDGVPolicy()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvPolicy);
      dgvFormated.Format(Status.Policy);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
      dgvFormated.HideColumn(3);
    }

    private void loadDTP()
    {
      _dgvDTP.DataSource = _dtpList.ToDataTable(_car);
      FormatDgvDTP();
    }

    private void FormatDgvDTP()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvDTP);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.Format(Status.DTP);
    }

    private void loadDiagCard()
    {
      _dgvDiagCard.DataSource = _car.getDataTableDiagCard();
      formatDGVDiagCard();
    }

    private void formatDGVDiagCard()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvDiagCard);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
      dgvFormated.HideColumn(3);
      dgvFormated.Format(Status.DiagCard);
    }

    private void loadMileage()
    {
      _dgvMileage.DataSource = _mileageList.ToDataTable(_car);
      FormatDGVMileage();
    }

    private void FormatDGVMileage()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvMileage);
      dgvFormated.HideColumn(0);
      dgvFormated.SetFormatMileage();
    }

    private void loadRepair()
    {
      dgvRepair.DataSource = _repairList.ToDataTableByCar(_car);
      FormatDGVRepair();
    }

    private void FormatDGVRepair()
    {
      DGVFormat dgvFormated = new DGVFormat(dgvRepair);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
      dgvFormated.HideColumn(3);
      dgvFormated.SetFormatRepair();
    }

    private void btnAddInvoice_Click(object sender, EventArgs e)
    {
      var invoice = _car.createInvoice();

      if (OpenAddEditDialog(invoice))
      {
        _invoiceList.Add(invoice);

        _driverCarList.ReLoad();

        LoadInvoice();
      }
    }

    private void _dgvInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);

      var invoice = _invoiceList.getItem(idInvoice);

      if (e.ColumnIndex == 4 && (invoice.File != string.Empty))
        WorkWithFiles.openFile(invoice.File);
      else if (OpenAddEditDialog(invoice))
      {
        _driverCarList.ReLoad();

        LoadInvoice();
      }
    }

    private static bool OpenAddEditDialog(Invoice invoice)
    {
      var invoiceForm = new Invoice_AddEdit(invoice);
      return invoiceForm.ShowDialog() == DialogResult.OK;
    }

    private void btnDelInvoice_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить накладную?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);
        _invoiceList.Delete(idInvoice);

        _driverCarList.ReLoad();

        LoadInvoice();
      }
    }

    private void btnAddInsurance_Click(object sender, EventArgs e)
    {
      PolicyForm pAE = new PolicyForm(_car.CreatePolicy());
      if (pAE.ShowDialog() == DialogResult.OK)
        loadPolicy();
    }

    private void btnDeletePolicy_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить полис?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int idPolicy = Convert.ToInt32(_dgvPolicy.Rows[_dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value);

        _policyList.Delete(idPolicy);

        loadPolicy();
      }
    }

    private void _dgvPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (!int.TryParse(_dgvPolicy.Rows[e.RowIndex].Cells[0].Value.ToString(), out int policyId))
        return;

      var policy = _policyList.getItem(policyId);

      if (e.ColumnIndex == 4 && !string.IsNullOrEmpty(policy.File))
      {
        WorkWithFiles.openFile(policy.File);
      }
      else
      {
        var policyForm = new PolicyForm(policy);
        if (policyForm.ShowDialog() == DialogResult.OK)
          loadPolicy();
      }
    }

    private void btnAddDTP_Click(object sender, EventArgs e)
    {
      DTP dtp = _car.createDTP();

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);

      if (dtpAE.ShowDialog() == DialogResult.OK)
      {
        _dtpList.Add(dtp);

        loadDTP();
      }
    }

    private void _dgvDTP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDTP = Convert.ToInt32(_dgvDTP.Rows[e.RowIndex].Cells[0].Value);

      DTP dtp = _dtpList.getItem(idDTP);

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);

      if (dtpAE.ShowDialog() == DialogResult.OK)
        loadDTP();
    }

    private void btnAddViolation_Click(object sender, EventArgs e)
    {
      Violation violation = _car.createViolation();

      ViolationForm vAE = new ViolationForm(violation);

      if (vAE.ShowDialog() == DialogResult.OK)
      {
        _violationList.Add(violation);
        loadViolation();
      }
    }

    private void _dgvViolation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idViolation = Convert.ToInt32(_dgvViolation.Rows[e.RowIndex].Cells[0].Value);

      Violation violation = _violationList.getItem(idViolation);

      if ((e.ColumnIndex == 6) && (violation.File != string.Empty))
        WorkWithFiles.openFile(violation.File);
      else if ((e.ColumnIndex == 7) && (violation.FilePay != string.Empty))
        WorkWithFiles.openFile(violation.FilePay);
      else
      {
        ViolationForm vAE = new ViolationForm(violation);
        if (vAE.ShowDialog() == DialogResult.OK)
          loadViolation();
      }
    }

    private void btnViolation_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить нарушение?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int idViolation = Convert.ToInt32(_dgvViolation.Rows[_dgvViolation.SelectedCells[0].RowIndex].Cells[0].Value);

        _violationList.Delete(idViolation);

        loadViolation();
      }
    }

    private void loadViolation()
    {
      _dgvViolation.DataSource = _violationList.ToDataTable(_car);

      FormatDGVViolation();
    }

    private void FormatDGVViolation()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvViolation);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
      dgvFormated.HideColumn(3);
      dgvFormated.Format(Status.Violation);
    }

    private void btnAddDiagCard_Click(object sender, EventArgs e)
    {
      DiagCard diagCard = _car.createDiagCard();

      DiagCard_AddEdit diagcardAE = new DiagCard_AddEdit(diagCard);
      if (diagcardAE.ShowDialog() == DialogResult.OK)
      {
        _diagCardList.Add(diagCard);

        loadDiagCard();
      }
    }

    private void _dgvDiagCard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDiagCard = Convert.ToInt32(_dgvDiagCard.Rows[e.RowIndex].Cells[0].Value);

      DiagCard diagCard = _diagCardList.getItem(idDiagCard);


      if ((e.ColumnIndex == 4) && (diagCard.File != string.Empty))
        WorkWithFiles.openFile(diagCard.File);
      else
      {
        DiagCard_AddEdit diagcardAE = new DiagCard_AddEdit(diagCard);

        if (diagcardAE.ShowDialog() == DialogResult.OK)
          loadDiagCard();
      }
    }

    private void btnDeleteDiagCard_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить диагностическую карту?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        int idDiagCard = Convert.ToInt32(_dgvDiagCard.Rows[_dgvDiagCard.SelectedCells[0].RowIndex].Cells[0].Value);

        _diagCardList.Delete(idDiagCard);

        loadDiagCard();
      }
    }

    private void showAddEditDiagCard(DiagCard dCard)
    {
      DiagCard_AddEdit vAE = new DiagCard_AddEdit(dCard);
      vAE.ShowDialog();
    }

    private void btnAddMileage_Click(object sender, EventArgs e)
    {
      Mileage mileage = _car.createMileage();

      Mileage_AddEdit mAE = new Mileage_AddEdit(mileage);

      if (mAE.ShowDialog() == DialogResult.OK)
      {
        _mileageList.Add(mileage);

        loadMileage();
      }
    }

    private void _dgvMileage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idMileage = Convert.ToInt32(_dgvMileage.Rows[e.RowIndex].Cells[0].Value);

      Mileage mileage = _mileageList.getItem(idMileage);

      Mileage_AddEdit mAE = new Mileage_AddEdit(mileage);

      if (mAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadMileage();
    }

    private void vin_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = isSpace(e.KeyChar);
    }

    private void year_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = isSpace(e.KeyChar);
    }

    private bool isSpace(char ch)
    {
      return ch == ' ';
    }

    private void btnAddCarDoc_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = true;
      ofd.ShowDialog();

      CarDocList carDocList = CarDocList.getInstance();

      foreach (string file in ofd.FileNames)
      {
        CarDoc carDoc = _car.createCarDoc(file);
        carDoc.Save();

        carDocList.Add(carDoc);
      }

      loadCarDoc();
    }

    private void loadCarDoc()
    {
      CarDocList carDocList = CarDocList.getInstance();
      dgvCarDoc.DataSource = carDocList.ToDataTableByCar(_car);

      if (dgvCarDoc.DataSource != null)
        formatDGVCardDoc();
    }

    private void formatDGVCardDoc()
    {
      DGVFormat dgvFormated = new DGVFormat(dgvCarDoc);
      dgvFormated.HideColumn(0);
    }

    private void dgvCarDoc_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }

    private void btnCarDocDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          System.Windows.Forms.DialogResult.Yes)
      {
        int idCarDoc = Convert.ToInt32(dgvCarDoc.Rows[dgvCarDoc.SelectedCells[0].RowIndex].Cells[0].Value);

        CarDocList carDocList = CarDocList.getInstance();

        carDocList.Delete(idCarDoc);

        loadCarDoc();
      }
    }

    private void btnAddRepair_Click(object sender, EventArgs e)
    {
      Repair repair = _car.createRepair();
      Repair_AddEdit repairAE = new Repair_AddEdit(repair);

      if (repairAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        _repairList.Add(repair);
        loadRepair();
      }
    }

    private void _dgvRepair_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idRepair = Convert.ToInt32(dgvRepair.Rows[dgvRepair.SelectedCells[0].RowIndex].Cells[0].Value);

      Repair repair = _repairList.getItem(idRepair);

      Repair_AddEdit repairAE = new Repair_AddEdit(repair);

      if (repairAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadRepair();
    }

    private void btnDelRepair_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить запись о ремонте?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          System.Windows.Forms.DialogResult.Yes)
      {
        int idRepair = Convert.ToInt32(dgvRepair.Rows[dgvRepair.SelectedCells[0].RowIndex].Cells[0].Value);

        _repairList.Delete(idRepair);

        loadRepair();
      }
    }

    private void chbLising_CheckedChanged(object sender, EventArgs e)
    {
      lbLising.Visible = chbLising.Checked;
      mtbLising.Visible = chbLising.Checked;
      chbLising.Checked = chbLising.Checked;
    }

    private void kaskoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        Policy policy = _policyList.getItem(_car, PolicyType.КАСКО);

        WorkWithFiles.openFile(policy.File);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void _dgvDTP_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {
      _curPosition.X = e.ColumnIndex;
      _curPosition.Y = e.RowIndex;
    }

    private void _dgvDTP_MouseDown(object sender, MouseEventArgs e)
    {
      if (isCellNoHeader(_curPosition.X) && isCellNoHeader(_curPosition.Y))
        _dgvDTP.CurrentCell = _dgvDTP.Rows[_curPosition.Y].Cells[_curPosition.X];
    }

    private void sTSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        WorkWithFiles.openFile(_sts.File);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void driverLicenseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DTP dtp = _dtpList.getItem(Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value));

      Driver driver = _driverCarList.GetDriver(dtp.Car, dtp.Date);

      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItem(driver);

      WorkWithFiles.openFile(driverLicense.File);
    }

    private void messageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int.TryParse(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int idDtp);

      var dtp = _dtpList.getItem(idDtp);

      IExcelDocumentService excelDocumentService = new ExcelDocumentService();

      var doc = excelDocumentService.CreateNotice(_car, dtp);

      doc.Show();
    }

    private void btnDelDTP_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить ДТП?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int idDTP = Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value);

        _dtpList.Delete(idDTP);

        loadDTP();
      }
    }

    private void btnMileageDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить информацию о пробеге?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var idMileage = Convert.ToInt32(_dgvMileage.Rows[_dgvMileage.SelectedCells[0].RowIndex].Cells[0].Value);

        _mileageList.Delete(idMileage);

        loadMileage();
      }
    }

    private void dgvCarDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (isCellNoHeader(e.RowIndex))
      {
        int idCarDoc = Convert.ToInt32(dgvCarDoc.Rows[e.RowIndex].Cells[0].Value);

        CarDocList carDocList = CarDocList.getInstance();
        CarDoc carDoc = carDocList.getItem(idCarDoc);

        if (e.ColumnIndex == 2)
        {
          WorkWithFiles.openFile(carDoc.File);
        }
        else
        {
          CarDoc_AddEdit carDocAE = new CarDoc_AddEdit(carDoc);
          if (carDocAE.ShowDialog() == DialogResult.OK)
            loadCarDoc();
        }
      }
    }

    private void loadShipPart()
    {
      dgvShipPart.DataSource = _shipPartList.ToDataTable(_car);
      FormatDGVShipPart();
    }

    private void FormatDGVShipPart()
    {
      DGVFormat dgvFormated = new DGVFormat(dgvShipPart);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
    }

    private void btnAddShipPart_Click(object sender, EventArgs e)
    {
      ShipPart shipPart = _car.createShipPart();
      ShipPart_AddEdit shipPartAE = new ShipPart_AddEdit(shipPart);

      if (shipPartAE.ShowDialog() == DialogResult.OK)
      {
        _shipPartList.Add(shipPart);
        loadShipPart();
      }
    }

    private void dgvShipPart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idShipPart = 0;
      int.TryParse(dgvShipPart.Rows[e.RowIndex].Cells[0].Value.ToString(), out idShipPart);

      ShipPart shipPart = _shipPartList.getItem(idShipPart);
      ShipPart_AddEdit shipPartAE = new ShipPart_AddEdit(shipPart);

      if (shipPartAE.ShowDialog() == DialogResult.OK)
        loadShipPart();
    }

    private void btnDelShipPart_Click(object sender, EventArgs e)
    {
      int idShipPart = 0;
      int.TryParse(dgvShipPart.Rows[dgvShipPart.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idShipPart);

      if (MessageBox.Show("Удалить информацию об отправки запчастей?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        _shipPartList.Delete(idShipPart);
        loadShipPart();
      }
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DataGridView dgv = findDGV();
      if (dgv != null)
        tryCopy(dgv);
    }

    private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      DataGridView dgv = findDGV();
      if (dgv != null)
        tryCopy(dgv);
    }

    private DataGridView findDGV()
    {
      foreach (var item in tabControl1.SelectedTab.Controls)
      {
        if (item is DataGridView)
          return item as DataGridView;
      }

      return null;
    }

    private void tryCopy(DataGridView dgv)
    {
      try
      {
        MyBuffer.Copy(dgv);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    
    private void llDriver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Driver driver = _driverCarList.GetDriver(_car);

      if (driver == null)
        return;

      Driver_AddEdit driverAE = new Driver_AddEdit(driver);
      driverAE.ShowDialog();
    }

    private void _dgvInvoice_Sorted(object sender, EventArgs e)
    {
      FormatDgvInvoice();
    }

    private void _dgvPolicy_Sorted(object sender, EventArgs e)
    {
      formatDGVPolicy();
    }

    private void _dgvDTP_Sorted(object sender, EventArgs e)
    {
      FormatDgvDTP();
    }

    private void _dgvViolation_Sorted(object sender, EventArgs e)
    {
      FormatDGVViolation();
    }

    private void _dgvDiagCard_Sorted(object sender, EventArgs e)
    {
      formatDGVDiagCard();
    }

    private void _dgvMileage_Sorted(object sender, EventArgs e)
    {
      FormatDGVMileage();
    }

    private void dgvCarDoc_Sorted(object sender, EventArgs e)
    {
      formatDGVCardDoc();
    }

    private void dgvRepair_Sorted(object sender, EventArgs e)
    {
      FormatDGVRepair();
    }

    private void dgvShipPart_Sorted(object sender, EventArgs e)
    {
      FormatDGVShipPart();
    }

    private void cbGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
      ChangedGrade();
    }

    private void ChangedGrade()
    {
      if (_load)
      {
        int id = 0;
        if (cbGrade.SelectedValue != null)
          int.TryParse(cbGrade.SelectedValue.ToString(), out id);

        if (id == 0)
          return;

        GradeList gradeList = GradeList.getInstance();
        Grade grade = gradeList.getItem(id);

        DataTable dt = _car.info.ToDataTable();

        DataTable dt2 = grade.ToDataTable();
        foreach (DataRow row in dt2.Rows)
          dt.Rows.Add(row.ItemArray);

        dgvCarInfo.DataSource = dt;
      }
    }

    private void btnTermination_Click(object sender, EventArgs e)
    {
      if (!int.TryParse(_dgvPolicy.Rows[_dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int policyId))
        return;

      var policy = PolicyList.getInstance().getItem(policyId);

      IWordDocumentService wordDocumentService = new WordDocumentService();
      var documnent = wordDocumentService.CreateTermination(policy);
      documnent.Print();
    }

    private void btnExtraTermination_Click(object sender, EventArgs e)
    {
      if (!int.TryParse(_dgvPolicy.Rows[_dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int policyId))
        return;

      var policy = PolicyList.getInstance().getItem(policyId);

      IWordDocumentService wordDocumentService = new WordDocumentService();
      var documnent = wordDocumentService.CreateExtraTermination(policy);
      documnent.Print();
    }

    private void LoadDrivers()
    {
      dgvDrivers.DataSource = DriverCarList.GetInstance().ToDataTable(_car);
      dgvDrivers.Columns[1].Width = 200;
    }
  }
}
