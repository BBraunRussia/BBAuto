using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.App.FormsForDriver.AddEdit;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.Mileage;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;
using Common.Resources;

namespace BBAuto.App.FormsForCar
{
  public partial class CarForm : Form, ICarForm
  {
    private System.Drawing.Point _curPosition;
    private Car _car;
    private STS _sts;
    private PTS _pts;

    private bool _load;

    private DiagCardList _diagCardList;
    private DriverCarList _driverCarList;
    private DriverList _driverList;
    private DTPList _dtpList;
    private InvoiceList _invoiceList;
    private PolicyList _policyList;
    private RepairList _repairList;
    private ViolationList _violationList;
    private ShipPartList _shipPartList;

    private WorkWithForm _workWithForm;

    private readonly IDealerService _dealerService;
    private readonly IMileageService _mileageService;

    private readonly IFormMileage _formMileage;
    private readonly IFormViolation _formViolation;

    public CarForm(
      IDealerService dealerService,
      IMileageService mileageService,
      IFormMileage formMileage,
      IFormViolation formViolation)
    {
      _dealerService = dealerService;
      _mileageService = mileageService;
      _formMileage = formMileage;
      _formViolation = formViolation;
    }

    public DialogResult ShowDialog(Car car)
    {
      _car = car;
      InitializeComponent();

      _diagCardList = DiagCardList.getInstance();
      _driverCarList = DriverCarList.getInstance();
      _driverList = DriverList.getInstance();
      _dtpList = DTPList.getInstance();
      _invoiceList = InvoiceList.getInstance();
      _policyList = PolicyList.getInstance();
      _repairList = RepairList.getInstance();
      _violationList = ViolationList.getInstance();
      _shipPartList = ShipPartList.getInstance();

      return ShowDialog();
    }

    private void Car_AddEdit_Load(object sender, EventArgs e)
    {
      LoadData();

      SetWindowHeader();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += SetNotEditableItems;
      _workWithForm.SetEditMode(_car.Id == 0 || !_car.IsGet);


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

    private void LoadData()
    {
      _load = false;
      loadOneStringDictionary(cbMark, "Mark");
      _load = true;
      loadModel();
      _load = false;
      loadOneStringDictionary(cbColor, "Color");
      loadOneStringDictionary(cbRegionBuy, "Region");
      loadOneStringDictionary(cbRegionUsing, "Region");
      loadOneStringDictionary(cbOwner, "Owner");

      var list = _dealerService.GetDealers();
      cbDealer.DataSource = list;
      cbDealer.DisplayMember = "Name";
      cbDealer.ValueMember = "Id";
      
      var region = GetRegion();

      cbDriver.DataSource = _driverList.ToDataTableByRegion(region);
      cbDriver.DisplayMember = "ФИО";
      cbDriver.ValueMember = "id";
      _load = true;

      fillFields();

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
      this.Text = string.Concat("Карточка автомобиля: ", _car.ToString());
    }

    private void cbMark_SelectedValueChanged(object sender, EventArgs e)
    {
      if (_load)
        loadModel();
    }

    private void loadModel()
    {
      if (_load)
      {
        _load = false;

        int idMark = 0;
        if (cbMark.SelectedValue != null)
          int.TryParse(cbMark.SelectedValue.ToString(), out idMark);

        ModelList models = ModelList.getInstance();

        cbModel.DataSource = models.ToDataTable(idMark);
        cbModel.DisplayMember = "Название";
        cbModel.ValueMember = "id";
        _load = true;
        loadGrade();
      }
    }

    private void loadGrade()
    {
      if (_load)
      {
        int idModel = 0;
        if (cbModel.SelectedValue != null)
          int.TryParse(cbModel.SelectedValue.ToString(), out idModel);
        GradeList grades = GradeList.getInstance();

        cbGrade.DataSource = grades.ToDataTable(idModel);
        cbGrade.DisplayMember = "Название";
        cbGrade.ValueMember = "id";
      }
    }

    private void fillFields()
    {
      cbMark.SelectedValue = (_car.Mark != null) ? _car.Mark.Id.ToString() : "0";
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
      cbRegionUsing.SelectedValue = _car.regionUsingID;
      cbDriver.SelectedValue = _car.driverID;
      cbDealer.SelectedValue = _car.idDiller;
      dtpDateOrder.Value = _car.dateOrder;
      chbIsGet.Checked = _car.IsGet;
      dtpDateGet.Value = _car.dateGet;
      tbEvents.Text = _car.events;
      tbCost.Text = _car.cost.ToString();
      tbDOP.Text = _car.dop;

      Driver driver = _driverCarList.GetDriver(_car) ?? new Driver();
      llDriver.Text = driver.GetName(NameType.Full);

      //если не назначен водитель
      if (driver.Region != null)
      {
        lbRegion.Text = driver.Region.Name;
      }

      PTSList ptsList = PTSList.getInstance();
      _pts = ptsList.getItem(_car);
      mtbNumberPTS.Text = _pts.Number;
      dtpDatePTS.Value = _pts.Date;
      tbGiveOrgPTS.Text = _pts.GiveOrg;
      TextBox tbFilePTS = ucFilePTS.Controls["tbFile"] as TextBox;
      tbFilePTS.Text = _pts.File;

      STSList stsList = STSList.getInstance();
      _sts = stsList.getItem(_car);
      mtbNumberSTS.Text = _sts.Number;
      dtpDateSTS.Value = _sts.Date;
      tbGiveOrgSTS.Text = _sts.GiveOrg;
      TextBox tbFileSTS = ucFileSTS.Controls["tbFile"] as TextBox;
      tbFileSTS.Text = _sts.File;

      MileageModel mileage = _mileageService.GetLastMileage(_car.Id);
      if (mileage != null)
        lbMileage.Text = mileage.ToString();

      ChangeDealer(_car.idDiller);

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

    private void CbDiller_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_load || cbDealer.SelectedValue == null)
        return;
      
      ChangeDealer(Convert.ToInt32(cbDealer.SelectedValue));
    }

    private void ChangeDealer(int selectedId)
    {
      var dealer = (DealerModel)cbDealer.Items[selectedId];
      
      if (dealer != null)
        tbDealerContacts.Text = dealer.Contacts;
    }

    private void model_SelectedIndexChanged(object sender, EventArgs e)
    {
      loadGrade();
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
        MessageBox.Show(Messages.NeedSelectGrade, Captions.FieldIsRequired, MessageBoxButtons.OK,
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
      _car.regionUsingID = cbRegionUsing.SelectedValue;
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

    private void loadInvoice()
    {
      _dgvInvoice.DataSource = _invoiceList.ToDataTable(_car);

      formatDGVInvoice();
    }

    private void formatDGVInvoice()
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
      loadPage();
    }

    private void loadPage()
    {
      if (tabControl1.SelectedTab == tabInvoice)
        loadInvoice();
      else if (tabControl1.SelectedTab == tabPolicy)
        loadPolicy();
      else if (tabControl1.SelectedTab == tabDTP)
        loadDTP();
      else if (tabControl1.SelectedTab == tabViolation)
        loadViolation();
      else if (tabControl1.SelectedTab == tabDiagCard)
        loadDiagCard();
      else if (tabControl1.SelectedTab == tabMileage)
        LoadMileage();
      else if (tabControl1.SelectedTab == tabRepair)
        loadRepair();
      else if (tabControl1.SelectedTab == tabShipParts)
        loadShipPart();
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

    private void LoadMileage()
    {
      _dgvMileage.DataSource = _mileageService.ToDataTable(_car.Id);
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
      Invoice invoice = _car.createInvoice();

      if (openAddEditDialog(invoice))
      {
        _invoiceList.Add(invoice);

        _driverCarList.ReLoad();

        loadInvoice();
      }
    }

    private void _dgvInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);

      Invoice invoice = _invoiceList.getItem(idInvoice);

      if ((e.ColumnIndex == 4) && (invoice.File != string.Empty))
        WorkWithFiles.OpenFile(invoice.File);
      else if (openAddEditDialog(invoice))
        loadInvoice();
    }

    private bool openAddEditDialog(Invoice invoice)
    {
      Invoice_AddEdit invoiceAE = new Invoice_AddEdit(invoice);
      return (invoiceAE.ShowDialog() == System.Windows.Forms.DialogResult.OK);
    }

    private void btnDelInvoice_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить накладную?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          System.Windows.Forms.DialogResult.Yes)
      {
        int idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);
        _invoiceList.Delete(idInvoice);

        _driverCarList.ReLoad();

        loadInvoice();
      }
    }

    private void btnAddInsurance_Click(object sender, EventArgs e)
    {
      Policy_AddEdit pAE = new Policy_AddEdit(_car.CreatePolicy());
      if (pAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadPolicy();
    }

    private void btnDeletePolicy_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить полис?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          System.Windows.Forms.DialogResult.Yes)
      {
        int idPolicy = Convert.ToInt32(_dgvPolicy.Rows[_dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value);

        _policyList.Delete(idPolicy);

        loadPolicy();
      }
    }

    private void _dgvPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idPolicy = Convert.ToInt32(_dgvPolicy.Rows[e.RowIndex].Cells[0].Value);

      Policy policy = _policyList.getItem(idPolicy);

      if ((e.ColumnIndex == 4) && (policy.File != string.Empty))
      {
        WorkWithFiles.OpenFile(policy.File);
      }
      else
      {
        Policy_AddEdit pAE = new Policy_AddEdit(policy);
        pAE.ShowDialog();

        loadPolicy();
      }
    }

    private void btnAddDTP_Click(object sender, EventArgs e)
    {
      DTP dtp = _car.createDTP();

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);

      if (dtpAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
      
      if (_formViolation.ShowDialog(violation) == DialogResult.OK)
      {
        _violationList.Add(violation);
        loadViolation();
      }
    }

    private void _dgvViolation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idViolation = Convert.ToInt32(_dgvViolation.Rows[e.RowIndex].Cells[0].Value);

      var violation = _violationList.getItem(idViolation);

      if (e.ColumnIndex == 6 && (violation.File != string.Empty))
        WorkWithFiles.OpenFile(violation.File);
      else if (e.ColumnIndex == 7 && (violation.FilePay != string.Empty))
        WorkWithFiles.OpenFile(violation.FilePay);
      else
      {
        if (_formViolation.ShowDialog(violation) == DialogResult.OK)
          loadViolation();
      }
    }

    private void btnViolation_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить нарушение?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          System.Windows.Forms.DialogResult.Yes)
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
      if (diagcardAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
        WorkWithFiles.OpenFile(diagCard.File);
      else
      {
        DiagCard_AddEdit diagcardAE = new DiagCard_AddEdit(diagCard);

        if (diagcardAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          loadDiagCard();
      }
    }

    private void btnDeleteDiagCard_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить диагностическую карту?", Captions.Delete, MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
      var mileage = new MileageModel(_car.Id);
      if (_formMileage.ShowDialog(mileage) == DialogResult.OK)
      {
        _mileageService.Save(mileage);

        LoadMileage();
      }
    }

    private void _dgvMileage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idMileage = Convert.ToInt32(_dgvMileage.Rows[e.RowIndex].Cells[0].Value);

      var mileage = _mileageService.GetMileage(idMileage);

      if (_formMileage.ShowDialog(mileage) == DialogResult.OK)
        LoadMileage();
    }

    private void vin_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = IsSpace(e.KeyChar);
    }

    private void year_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = IsSpace(e.KeyChar);
    }

    private static bool IsSpace(char ch)
    {
      return ch == ' ';
    }

    private void btnAddCarDoc_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog {Multiselect = true};
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

    private static bool IsCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }

    private void btnCarDocDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.DeleteDocument, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
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

      if (repairAE.ShowDialog() == DialogResult.OK)
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

      if (repairAE.ShowDialog() == DialogResult.OK)
        loadRepair();
    }

    private void btnDelRepair_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить запись о ремонте?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
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

        WorkWithFiles.OpenFile(policy.File);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void _dgvDTP_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {
      _curPosition.X = e.ColumnIndex;
      _curPosition.Y = e.RowIndex;
    }

    private void _dgvDTP_MouseDown(object sender, MouseEventArgs e)
    {
      if (IsCellNoHeader(_curPosition.X) && IsCellNoHeader(_curPosition.Y))
        _dgvDTP.CurrentCell = _dgvDTP.Rows[_curPosition.Y].Cells[_curPosition.X];
    }

    private void sTSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        WorkWithFiles.OpenFile(_sts.File);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void driverLicenseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DTP dtp = _dtpList.getItem(Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value));

      Driver driver = _driverCarList.GetDriver(dtp.Car, dtp.Date);

      LicenseList licencesList = LicenseList.getInstance();
      DriverLicense driverLicense = licencesList.getItem(driver);

      WorkWithFiles.OpenFile(driverLicense.File);
    }

    private void messageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int idDTP = 0;
      int.TryParse(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idDTP);

      DTP dtp = _dtpList.getItem(idDTP);

      CreateDocument doc = new CreateDocument(_car);

      doc.ShowNotice(dtp);
    }

    private void btnDelDTP_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.DeleteDTP, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int idDTP = Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value);

        _dtpList.Delete(idDTP);

        loadDTP();
      }
    }

    private void btnMileageDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.NeedDeleteMileage, Captions.Delete, MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var idMileage = Convert.ToInt32(_dgvMileage.Rows[_dgvMileage.SelectedCells[0].RowIndex].Cells[0].Value);

        _mileageService.Delete(idMileage);

        LoadMileage();
      }
    }

    private void dgvCarDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (IsCellNoHeader(e.RowIndex))
      {
        int idCarDoc = Convert.ToInt32(dgvCarDoc.Rows[e.RowIndex].Cells[0].Value);

        CarDocList carDocList = CarDocList.getInstance();
        CarDoc carDoc = carDocList.getItem(idCarDoc);

        if (e.ColumnIndex == 2)
        {
          WorkWithFiles.OpenFile(carDoc.File);
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

      if (MessageBox.Show("Удалить информацию об отправки запчастей?", Captions.Delete, MessageBoxButtons.YesNo,
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
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void llDriver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Driver driver = _driverCarList.GetDriver(_car);

      if (driver == null)
        return;

      DriverForm driverAE = new DriverForm(driver);
      driverAE.ShowDialog();
    }

    private void _dgvInvoice_Sorted(object sender, EventArgs e)
    {
      formatDGVInvoice();
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
  }
}
