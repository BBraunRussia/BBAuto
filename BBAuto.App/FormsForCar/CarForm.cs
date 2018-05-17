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
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Car.Doc;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Documents;
using BBAuto.Logic.Services.Mileage;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;
using Common.Resources;

namespace BBAuto.App.FormsForCar
{
  public partial class CarForm : Form, ICarForm
  {
    private System.Drawing.Point _curPosition;
    private CarModel _car;
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
    private readonly IDocumentsService _documentsService;
    private readonly ICarDocService _carDocService;

    private readonly IMileageForm _formMileage;
    private readonly IDiagCardForm _formDiagCard;
    private readonly ICarDocForm _carDocForm;
    private readonly ICarService _carService;

    private readonly IDgvFormatter _dgvFormatter;
    
    public CarForm(
      IDealerService dealerService,
      IMileageService mileageService,
      IMileageForm formMileage,
      IDiagCardService diagCardService,
      IDiagCardForm formDiagCard,
      IDgvFormatter dgvFormatter,
      IDocumentsService documentsService,
      ICarDocService carDocService,
      ICarDocForm carDocForm,
      ICarService carService)
    {
      _dealerService = dealerService;
      _mileageService = mileageService;
      _formMileage = formMileage;
      _diagCardService = diagCardService;
      _formDiagCard = formDiagCard;
      _dgvFormatter = dgvFormatter;
      _documentsService = documentsService;
      _carDocService = carDocService;
      _carDocForm = carDocForm;
      _carService = carService;
    }

    public DialogResult ShowDialog(CarModel car)
    {
      InitializeComponent();

      _car = car;

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
      cbMark.SelectedValue = _car.MarkId;
      cbModel.SelectedValue = _car.ModelId;
      cbGrade.SelectedValue = _car.GradeId;
      /* когда Audi не заполняется таблица с инфо о машине */
      if (dgvCarInfo.DataSource == null)
        ChangedGrade();
      cbColor.SelectedValue = _car.ColorId;

      tbBbNumber.Text = _car.BbNumber;
      tbVin.Text = _car.Vin;
      tbYear.Text = _car.Year.ToString();
      tbENumber.Text = _car.ENumber;
      tbBodyNumber.Text = _car.BodyNumber;
      mtbGRZ.Text = _car.Grz;
      cbOwner.SelectedValue = _car.OwnerId;
      cbRegionBuy.SelectedValue = _car.RegionIdBuy;
      cbRegionUsing.SelectedValue = _car.RegionIdUsing;
      cbDriver.SelectedValue = _car.DriverId;
      cbDealer.SelectedValue = _car.DealerId;
      if (_car.DateOrder.HasValue)
        dtpDateOrder.Value = _car.DateOrder.Value;
      chbIsGet.Checked = _car.IsGet;
      if (_car.DateGet.HasValue)
        dtpDateGet.Value = _car.DateGet.Value;
      tbEvents.Text = _car.Events;
      tbCost.Text = _car.Cost.ToString();
      tbDOP.Text = _car.Dop;

      var driver = _driverCarList.GetDriver(_car.Id) ?? new Driver();
      llDriver.Text = driver.GetName(NameType.Full);

      //если не назначен водитель
      if (driver.Region != null)
      {
        lbRegion.Text = driver.Region.Name;
      }

      PTSList ptsList = PTSList.getInstance();
      _pts = ptsList.getItem(_car.Id);
      mtbNumberPTS.Text = _pts.Number;
      dtpDatePTS.Value = _pts.Date;
      tbGiveOrgPTS.Text = _pts.GiveOrg;
      var tbFilePts = ucFilePTS.Controls["tbFile"] as TextBox;
      tbFilePts.Text = _pts.File;

      STSList stsList = STSList.getInstance();
      _sts = stsList.getItem(_car.Id);
      mtbNumberSTS.Text = _sts.Number;
      dtpDateSTS.Value = _sts.Date;
      tbGiveOrgSTS.Text = _sts.GiveOrg;
      TextBox tbFileSTS = ucFileSTS.Controls["tbFile"] as TextBox;
      tbFileSTS.Text = _sts.File;

      MileageModel mileage = _mileageService.GetLastMileage(_car.Id);
      if (mileage != null)
        lbMileage.Text = mileage.ToString();

      ChangeDealer(_car.DealerId);

      lbLising.Visible = _car.LisingDate.HasValue;
      mtbLising.Visible = _car.LisingDate.HasValue;
      chbLising.Checked = _car.LisingDate.HasValue;

      if (_car.LisingDate.HasValue)
        mtbLising.Text = _car.LisingDate.Value.ToShortDateString();

      tbInvertoryNumber.Text = _car.InvertoryNumber;
    }

    private void CbDiller_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_load || cbDealer.SelectedValue == null)
        return;
      
      ChangeDealer(Convert.ToInt32(cbDealer.SelectedValue));
    }

    private void ChangeDealer(int? selectedId)
    {
      if (!selectedId.HasValue)
        return;

      var dealer = (DealerModel)cbDealer.Items[selectedId.Value];
      
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
          _carService.Save(_car);
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

      if (int.TryParse(cbMark.SelectedValue.ToString(), out int idMark))
        _car.MarkId = idMark;

      if (int.TryParse(cbModel.SelectedValue.ToString(), out int modelId))
        _car.ModelId = modelId;

      if (int.TryParse(cbGrade.SelectedValue.ToString(), out int gradeId))
        _car.GradeId = gradeId;

      if (int.TryParse(cbColor.SelectedValue.ToString(), out int colorId))
        _car.ColorId = colorId;

      _car.Vin = tbVin.Text;
      _car.Grz = mtbGRZ.Text;
      _car.ENumber = tbENumber.Text.ToUpper();
      _car.BodyNumber = tbBodyNumber.Text.ToUpper();

      if (int.TryParse(tbYear.Text, out int year))
        _car.Year = year;

      if (int.TryParse(cbOwner.SelectedValue.ToString(), out int ownerId))
        _car.OwnerId = ownerId;
      
      if (int.TryParse(cbRegionBuy.SelectedValue.ToString(), out int regionIdBuy))
        _car.RegionIdBuy = regionIdBuy;
      
      if (int.TryParse(cbRegionUsing.SelectedValue.ToString(), out int regionIdUsing))
        _car.RegionIdUsing = regionIdUsing;

      if (int.TryParse(cbDriver.SelectedValue.ToString(), out int driverId))
        _car.DriverId = driverId;

      _car.DateOrder = dtpDateOrder.Value;
      _car.IsGet = chbIsGet.Checked;
      _car.DateGet = dtpDateGet.Value;

      if (decimal.TryParse(tbCost.Text, out decimal cost))
        _car.Cost = cost;

      _car.Dop = tbDOP.Text;
      _car.Events = tbEvents.Text;

      if (int.TryParse(cbDealer.SelectedValue.ToString(), out int dealerId))
        _car.DealerId = dealerId;

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

      if (chbLising.Checked && DateTime.TryParse(mtbLising.Text, out DateTime dateLising))
        _car.LisingDate = dateLising;
      else
        _car.LisingDate = null;

      _car.InvertoryNumber = tbInvertoryNumber.Text;

      return true;
    }

    private void LoadInvoice()
    {
      _dgvInvoice.DataSource = _invoiceList.ToDataTable(_car.Id);

      FormatDgvInvoice();
    }

    private void FormatDgvInvoice()
    {
      _dgvFormatter.SetDgv(_dgvInvoice);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.HideColumn(2);
      _dgvFormatter.HideColumn(3);
      _dgvFormatter.FormatDgv(Status.Invoice);
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
        LoadShipPart();
    }

    private void LoadPolicy()
    {
      _dgvPolicy.DataSource = _policyList.ToDataTable(_car.Id);

      FormatDgvPolicy();
    }

    private void FormatDgvPolicy()
    {
      _dgvFormatter.SetDgv(_dgvPolicy);
      _dgvFormatter.FormatDgv(Status.Policy);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.HideColumn(2);
      _dgvFormatter.HideColumn(3);
    }

    private void LoadDtp()
    {
      _dgvDTP.DataSource = _dtpList.ToDataTable(_car.Id);
      FormatDgvDtp();
    }

    private void FormatDgvDtp()
    {
      _dgvFormatter.SetDgv(_dgvDTP);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.FormatDgv(Status.DTP);
    }

    private void LoadDiagCard()
    {
      _dgvDiagCard.DataSource = _diagCardService.GetDataTableByCar(_car);
      FormatDgvDiagCard();
    }

    private void FormatDgvDiagCard()
    {
      _dgvFormatter.SetDgv(_dgvDiagCard);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.HideColumn(2);
      _dgvFormatter.HideColumn(3);
      _dgvFormatter.FormatDgv(Status.DiagCard);
    }

    private void LoadMileage()
    {
      _dgvMileage.DataSource = _mileageService.ToDataTable(_car.Id);
      FormatDgvMileage();
    }

    private void FormatDgvMileage()
    {
      _dgvFormatter.SetDgv(_dgvMileage);
      _dgvFormatter.HideColumn(0);
      _dgvFormatter.SetFormatMileage();
    }

    private void LoadRepair()
    {
      dgvRepair.DataSource = _repairList.ToDataTableByCar(_car.Id);
      FormatDgvRepair();
    }

    private void FormatDgvRepair()
    {
      _dgvFormatter.SetDgv(dgvRepair);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.HideColumn(2);
      _dgvFormatter.HideColumn(3);
      _dgvFormatter.SetFormatRepair();
    }

    private void btnAddInvoice_Click(object sender, EventArgs e)
    {
      Invoice invoice = new Invoice(_car.Id);

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

      var invoice = _invoiceList.GetItem(idInvoice);

      if (e.ColumnIndex == 4 && invoice.File != string.Empty)
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
      if (MessageBox.Show(Messages.DeleteInvoice, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var idInvoice = Convert.ToInt32(_dgvInvoice.Rows[_dgvInvoice.SelectedCells[0].RowIndex].Cells[0].Value);
        _invoiceList.Delete(idInvoice);

        _driverCarList.ReLoad();

        LoadInvoice();
      }
    }

    private void btnAddInsurance_Click(object sender, EventArgs e)
    {
      var pAE = new Policy_AddEdit(new Policy(_car.Id));
      if (pAE.ShowDialog() == DialogResult.OK)
        LoadPolicy();
    }

    private void btnDeletePolicy_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(Messages.DeletePolicy, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        var idPolicy = Convert.ToInt32(_dgvPolicy.Rows[_dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value);

        _policyList.Delete(idPolicy);

        LoadPolicy();
      }
    }

    private void _dgvPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var idPolicy = Convert.ToInt32(_dgvPolicy.Rows[e.RowIndex].Cells[0].Value);

      var policy = _policyList.getItem(idPolicy);

      if (e.ColumnIndex == 4 && (policy.File != string.Empty))
      {
        WorkWithFiles.OpenFile(policy.File);
      }
      else
      {
        var pAe = new Policy_AddEdit(policy);
        pAe.ShowDialog();

        LoadPolicy();
      }
    }

    private void btnAddDTP_Click(object sender, EventArgs e)
    {
      DTP dtp = new DTP(_car.Id);

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
      var violation = new Violation(_car.Id);

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
      _dgvViolation.DataSource = _violationList.ToDataTable(_car.Id);

      FormatDgvViolation();
    }

    private void FormatDgvViolation()
    {
      _dgvFormatter.SetDgv(_dgvViolation);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.HideColumn(2);
      _dgvFormatter.HideColumn(3);
      _dgvFormatter.FormatDgv(Status.Violation);
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

      var diagCard = _diagCardService.GetByCarId(idDiagCard);
      
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
      
      foreach (var file in ofd.FileNames)
      {
        _carDocService.Save(new CarDocModel
        {
          CarId = _car.Id,
          Name = System.IO.Path.GetFileNameWithoutExtension(file),
          File = file
        });
      }

      LoadCarDoc();
    }

    private void LoadCarDoc()
    {
      dgvCarDoc.DataSource = _carDocService.GetDataTableByCarId(_car.Id);

      if (dgvCarDoc.DataSource != null)
        FormatDgvCardDoc();
    }

    private void FormatDgvCardDoc()
    {
      _dgvFormatter.SetDgv(dgvCarDoc);
      _dgvFormatter.HideColumn(0);
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

        _carDocService.Delete(idCarDoc);

        LoadCarDoc();
      }
    }

    private void btnAddRepair_Click(object sender, EventArgs e)
    {
      var repair = new Repair(_car.Id);
      var repairAe = new Repair_AddEdit(repair);

      if (repairAe.ShowDialog() == DialogResult.OK)
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
      if (MessageBox.Show(Messages.DeleteRepair, Captions.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
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
        var policy = _policyList.getItem(_car.Id, PolicyType.КАСКО);

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

      var driver = _driverCarList.GetDriver(dtp.CarId, dtp.Date);

      var licencesList = LicenseList.getInstance();
      var driverLicense = licencesList.getItem(driver);

      WorkWithFiles.OpenFile(driverLicense.File);
    }

    private void messageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int.TryParse(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int idDtp);

      var dtp = _dtpList.getItem(idDtp);
      
      var document = _documentsService.CreateNotice(_car.Id, dtp);
      document.Show();
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
        var idCarDoc = Convert.ToInt32(dgvCarDoc.Rows[e.RowIndex].Cells[0].Value);

        var carDoc = _carDocService.GetCarDocById(idCarDoc);

        if (e.ColumnIndex == 2)
        {
          WorkWithFiles.OpenFile(carDoc.File);
        }
        else
        {
          if (_carDocForm.ShowDialog(carDoc) == DialogResult.OK)
            LoadCarDoc();
        }
      }
    }

    private void LoadShipPart()
    {
      dgvShipPart.DataSource = _shipPartList.ToDataTable(_car.Id);
      FormatDgvShipPart();
    }

    private void FormatDgvShipPart()
    {
      _dgvFormatter.SetDgv(dgvShipPart);
      _dgvFormatter.HideTwoFirstColumns();
      _dgvFormatter.HideColumn(2);
    }

    private void btnAddShipPart_Click(object sender, EventArgs e)
    {
      var shipPart = new ShipPart(_car.Id);
      var shipPartAe = new ShipPart_AddEdit(shipPart);

      if (shipPartAe.ShowDialog() == DialogResult.OK)
      {
        _shipPartList.Add(shipPart);
        LoadShipPart();
      }
    }

    private void dgvShipPart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int.TryParse(dgvShipPart.Rows[e.RowIndex].Cells[0].Value.ToString(), out int idShipPart);

      var shipPart = _shipPartList.getItem(idShipPart);
      var shipPartAe = new ShipPart_AddEdit(shipPart);

      if (shipPartAe.ShowDialog() == DialogResult.OK)
        LoadShipPart();
    }

    private void btnDelShipPart_Click(object sender, EventArgs e)
    {
      int.TryParse(dgvShipPart.Rows[dgvShipPart.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out int idShipPart);

      if (MessageBox.Show(Messages.DeleteShipParts, Captions.Delete, MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        _shipPartList.Delete(idShipPart);
        LoadShipPart();
      }
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var dgv = FindDgv();
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
      var driver = _driverCarList.GetDriver(_car.Id);

      if (driver == null)
        return;

      var driverAe = new DriverForm(driver);
      driverAe.ShowDialog();
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
      FormatDgvViolation();
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
      FormatDgvShipPart();
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

        var gradeList = GradeList.getInstance();
        var grade = gradeList.getItem(id);

        var dt = _car.ToDataTableInfo();

        var dt2 = grade.ToDataTable();
        foreach (DataRow row in dt2.Rows)
          dt.Rows.Add(row.ItemArray);

        dgvCarInfo.DataSource = dt;
      }
    }
  }
}
