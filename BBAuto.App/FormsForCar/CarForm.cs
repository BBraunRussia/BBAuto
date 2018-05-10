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
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.DiagCard;
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
    private readonly IDiagCardService _diagCardService;

    private readonly IMileageForm _formMileage;
    private readonly IDiagCardForm _formDiagCard;
    
    public CarForm(
      IDealerService dealerService,
      IMileageService mileageService,
      IMileageForm formMileage,
      IDiagCardService diagCardService,
      IDiagCardForm formDiagCard)
    {
      _dealerService = dealerService;
      _mileageService = mileageService;
      _formMileage = formMileage;
      _diagCardService = diagCardService;
      _formDiagCard = formDiagCard;
    }

    public DialogResult ShowDialog(Car car)
    {
      _car = car;
      InitializeComponent();

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
      LoadModel();
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

      FillFields();

      LoadCarDoc();
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
      if (_load)
      {
        _load = false;

        int idMark = 0;
        if (cbMark.SelectedValue != null)
          int.TryParse(cbMark.SelectedValue.ToString(), out idMark);

        var models = ModelList.getInstance();

        cbModel.DataSource = models.ToDataTable(idMark);
        cbModel.DisplayMember = "Название";
        cbModel.ValueMember = "id";
        _load = true;
        LoadGrade();
      }
    }

    private void LoadGrade()
    {
      if (_load)
      {
        var idModel = 0;
        if (cbModel.SelectedValue != null)
          int.TryParse(cbModel.SelectedValue.ToString(), out idModel);
        var grades = GradeList.getInstance();

        cbGrade.DataSource = grades.ToDataTable(idModel);
        cbGrade.DisplayMember = "Название";
        cbGrade.ValueMember = "id";
      }
    }

    private void FillFields()
    {
      cbMark.SelectedValue = _car.Mark?.Id.ToString() ?? "0";
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

      var driver = _driverCarList.GetDriver(_car) ?? new Driver();
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
      var tbFilePts = ucFilePTS.Controls["tbFile"] as TextBox;
      tbFilePts.Text = _pts.File;

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
        MessageBox.Show(Messages.NeedSelectGrade, Captions.FieldIsRequired, MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        return false;
      }

      int.TryParse(cbMark.SelectedValue.ToString(), out int idMark);
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
        LoadPolicy();
      else if (tabControl1.SelectedTab == tabDTP)
        LoadDtp();
      else if (tabControl1.SelectedTab == tabViolation)
        LoadViolation();
      else if (tabControl1.SelectedTab == tabDiagCard)
        LoadDiagCard();
      else if (tabControl1.SelectedTab == tabMileage)
        LoadMileage();
      else if (tabControl1.SelectedTab == tabRepair)
        LoadRepair();
      else if (tabControl1.SelectedTab == tabShipParts)
        loadShipPart();
    }

    private void LoadPolicy()
    {
      _dgvPolicy.DataSource = _policyList.ToDataTable(_car);

      FormatDgvPolicy();
    }

    private void FormatDgvPolicy()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvPolicy);
      dgvFormated.Format(Status.Policy);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.HideColumn(2);
      dgvFormated.HideColumn(3);
    }

    private void LoadDtp()
    {
      _dgvDTP.DataSource = _dtpList.ToDataTable(_car);
      FormatDgvDtp();
    }

    private void FormatDgvDtp()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvDTP);
      dgvFormated.HideTwoFirstColumns();
      dgvFormated.Format(Status.DTP);
    }

    private void LoadDiagCard()
    {
      _dgvDiagCard.DataSource = _car.getDataTableDiagCard();
      FormatDgvDiagCard();
    }

    private void FormatDgvDiagCard()
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
      FormatDgvMileage();
    }

    private void FormatDgvMileage()
    {
      DGVFormat dgvFormated = new DGVFormat(_dgvMileage);
      dgvFormated.HideColumn(0);
      dgvFormated.SetFormatMileage();
    }

    private void LoadRepair()
    {
      dgvRepair.DataSource = _repairList.ToDataTableByCar(_car);
      FormatDgvRepair();
    }

    private void FormatDgvRepair()
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

        LoadInvoice();
      }
    }

    private void _dgvInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);

      var invoice = _invoiceList.getItem(idInvoice);

      if (e.ColumnIndex == 4 && (invoice.File != string.Empty))
        WorkWithFiles.OpenFile(invoice.File);
      else if (openAddEditDialog(invoice))
        LoadInvoice();
    }

    private bool openAddEditDialog(Invoice invoice)
    {
      var invoiceAe = new Invoice_AddEdit(invoice);
      return invoiceAe.ShowDialog() == DialogResult.OK;
    }

    private void btnDelInvoice_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить накладную?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        int idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);
        _invoiceList.Delete(idInvoice);

        _driverCarList.ReLoad();

        LoadInvoice();
      }
    }

    private void btnAddInsurance_Click(object sender, EventArgs e)
    {
      Policy_AddEdit pAE = new Policy_AddEdit(_car.CreatePolicy());
      if (pAE.ShowDialog() == DialogResult.OK)
        LoadPolicy();
    }

    private void btnDeletePolicy_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить полис?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        int idPolicy = Convert.ToInt32(_dgvPolicy.Rows[_dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value);

        _policyList.Delete(idPolicy);

        LoadPolicy();
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

        LoadPolicy();
      }
    }

    private void btnAddDTP_Click(object sender, EventArgs e)
    {
      DTP dtp = _car.createDTP();

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);

      if (dtpAE.ShowDialog() == DialogResult.OK)
      {
        _dtpList.Add(dtp);

        LoadDtp();
      }
    }

    private void _dgvDTP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idDtp = Convert.ToInt32(_dgvDTP.Rows[e.RowIndex].Cells[0].Value);

      var dtp = _dtpList.getItem(idDtp);

      var dtpAe = new DTP_AddEdit(dtp);

      if (dtpAe.ShowDialog() == DialogResult.OK)
        LoadDtp();
    }

    private void btnAddViolation_Click(object sender, EventArgs e)
    {
      var violation = _car.createViolation();

      var formViolation = new ViolationForm(this);

      if (formViolation.ShowDialog(violation) == DialogResult.OK)
      {
        _violationList.Add(violation);
        LoadViolation();
      }
    }

    private void _dgvViolation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idViolation = Convert.ToInt32(_dgvViolation.Rows[e.RowIndex].Cells[0].Value);

      var violation = _violationList.getItem(idViolation);

      if (e.ColumnIndex == 6 && violation.File != string.Empty)
        WorkWithFiles.OpenFile(violation.File);
      else if (e.ColumnIndex == 7 && violation.FilePay != string.Empty)
        WorkWithFiles.OpenFile(violation.FilePay);
      else
      {
        var formViolation = new ViolationForm(this);

        if (formViolation.ShowDialog(violation) == DialogResult.OK)
          LoadViolation();
      }
    }

    private void btnViolation_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.DeleteViolation, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        int idViolation = Convert.ToInt32(_dgvViolation.Rows[_dgvViolation.SelectedCells[0].RowIndex].Cells[0].Value);

        _violationList.Delete(idViolation);

        LoadViolation();
      }
    }

    private void LoadViolation()
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
      var diagCard = new DiagCardModel { CarId = _car.Id };
      
      if (_formDiagCard.ShowDialog(diagCard) == DialogResult.OK)
      {
        _diagCardService.Save(diagCard);

        LoadDiagCard();
      }
    }

    private void _dgvDiagCard_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idDiagCard = Convert.ToInt32(_dgvDiagCard.Rows[e.RowIndex].Cells[0].Value);

      var diagCard = _diagCardService.Get(idDiagCard);
      
      if (e.ColumnIndex == 4 && diagCard.File != string.Empty)
        WorkWithFiles.OpenFile(diagCard.File);
      else
      {
        if (_formDiagCard.ShowDialog(diagCard) == DialogResult.OK)
          LoadDiagCard();
      }
    }

    private void btnDeleteDiagCard_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.DeleteDiagCard, Captions.Delete, MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var idDiagCard = Convert.ToInt32(_dgvDiagCard.Rows[_dgvDiagCard.SelectedCells[0].RowIndex].Cells[0].Value);

        _diagCardService.Delete(idDiagCard);

        LoadDiagCard();
      }
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

      LoadCarDoc();
    }

    private void LoadCarDoc()
    {
      CarDocList carDocList = CarDocList.getInstance();
      dgvCarDoc.DataSource = carDocList.ToDataTableByCar(_car);

      if (dgvCarDoc.DataSource != null)
        FormatDgvCardDoc();
    }

    private void FormatDgvCardDoc()
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

        LoadCarDoc();
      }
    }

    private void btnAddRepair_Click(object sender, EventArgs e)
    {
      Repair repair = _car.createRepair();
      Repair_AddEdit repairAE = new Repair_AddEdit(repair);

      if (repairAE.ShowDialog() == DialogResult.OK)
      {
        _repairList.Add(repair);
        LoadRepair();
      }
    }

    private void _dgvRepair_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idRepair = Convert.ToInt32(dgvRepair.Rows[dgvRepair.SelectedCells[0].RowIndex].Cells[0].Value);

      var repair = _repairList.getItem(idRepair);

      var repairAe = new Repair_AddEdit(repair);

      if (repairAe.ShowDialog() == DialogResult.OK)
        LoadRepair();
    }

    private void btnDelRepair_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить запись о ремонте?", Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        int idRepair = Convert.ToInt32(dgvRepair.Rows[dgvRepair.SelectedCells[0].RowIndex].Cells[0].Value);

        _repairList.Delete(idRepair);

        LoadRepair();
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
        var policy = _policyList.getItem(_car, PolicyType.КАСКО);

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
      var dtp = _dtpList.getItem(Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value));

      var driver = _driverCarList.GetDriver(dtp.Car, dtp.Date);

      var licencesList = LicenseList.getInstance();
      var driverLicense = licencesList.getItem(driver);

      WorkWithFiles.OpenFile(driverLicense.File);
    }

    private void messageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int.TryParse(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int idDtp);

      var dtp = _dtpList.getItem(idDtp);

      var doc = new CreateDocument(_car);

      doc.ShowNotice(dtp);
    }

    private void btnDelDTP_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.DeleteDTP, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          DialogResult.Yes)
      {
        var idDtp = Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value);

        _dtpList.Delete(idDtp);

        LoadDtp();
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
            LoadCarDoc();
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
      int.TryParse(dgvShipPart.Rows[e.RowIndex].Cells[0].Value.ToString(), out int idShipPart);

      var shipPart = _shipPartList.getItem(idShipPart);
      var shipPartAe = new ShipPart_AddEdit(shipPart);

      if (shipPartAe.ShowDialog() == DialogResult.OK)
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
      DataGridView dgv = FindDgv();
      if (dgv != null)
        tryCopy(dgv);
    }

    private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      DataGridView dgv = FindDgv();
      if (dgv != null)
        tryCopy(dgv);
    }

    private DataGridView FindDgv()
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
      FormatDgvInvoice();
    }

    private void _dgvPolicy_Sorted(object sender, EventArgs e)
    {
      FormatDgvPolicy();
    }

    private void _dgvDTP_Sorted(object sender, EventArgs e)
    {
      FormatDgvDtp();
    }

    private void _dgvViolation_Sorted(object sender, EventArgs e)
    {
      FormatDGVViolation();
    }

    private void _dgvDiagCard_Sorted(object sender, EventArgs e)
    {
      FormatDgvDiagCard();
    }

    private void _dgvMileage_Sorted(object sender, EventArgs e)
    {
      FormatDgvMileage();
    }

    private void dgvCarDoc_Sorted(object sender, EventArgs e)
    {
      FormatDgvCardDoc();
    }

    private void dgvRepair_Sorted(object sender, EventArgs e)
    {
      FormatDgvRepair();
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
