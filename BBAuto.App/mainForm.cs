using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BBAuto.App.Actions;
using BBAuto.App.AddEdit;
using BBAuto.App.Common;
using BBAuto.App.ContextMenu;
using BBAuto.App.Events;
using BBAuto.App.FormsForCar;
using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.App.FormsForDriver.AddEdit;
using BBAuto.App.GUI;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App
{
  public partial class MainForm : Form, IForm
  {
    private Point _curPosition;
    private Point _savedPosition;

    private readonly MainStatus _mainStatus;

    private readonly SearchInDgv _seacher;
    
    private readonly MyFilter _myFilter;
    private readonly MyStatusStrip _myStatusStrip;

    private readonly ICarForm _carForm;
    private readonly ISaleCarForm _saleCarForm;
    private readonly IViolationForm _formViolation;
    private readonly IDiagCardForm _diagCardForm;
    
    private readonly ICarService _carService;
    private readonly ISaleCarService _saleCarService;
    private readonly IDiagCardService _diagCardService;

    private readonly IMyMenu _myMenu;
    private readonly IMainDgv _mainDgv;

    private readonly IDgvFormatter _dgvFormatter;

    public MainForm(
      ICarForm carForm,
      ISaleCarForm saleCarForm,
      ICarService carService,
      ISaleCarService saleCarService,
      IMyMenu myMenu,
      IViolationForm formViolation,
      IDiagCardForm diagCardForm,
      IDiagCardService diagCardService,
      IMainDgv mainDgv,
      IDgvFormatter dgvFormatter)
    {
      _carForm = carForm;
      _saleCarForm = saleCarForm;
      _carService = carService;
      _saleCarService = saleCarService;
      _myMenu = myMenu;
      _formViolation = formViolation;
      _diagCardForm = diagCardForm;
      _diagCardService = diagCardService;
      _mainDgv = mainDgv;
      _dgvFormatter = dgvFormatter;

      InitializeComponent();

      _mainStatus = MainStatus.getInstance();
      _mainStatus.StatusChanged += StatusChanged;
      _mainStatus.StatusChanged += SetWindowHeaderText;
      _mainStatus.StatusChanged += ConfigContextMenu;

      _mainDgv.SetDgv(_dgvCar);

      _seacher = new SearchInDgv(_dgvCar);

      _myStatusStrip = new MyStatusStrip(_dgvCar, statusStrip1);

      _myFilter = MyFilter.GetInstanceCars();
      _myFilter.Fill(_dgvCar, _myStatusStrip, this);
    }

    private void StatusChanged(Object sender, StatusEventArgs e)
    {
      _myFilter.clearComboList();
      _myFilter.clearFilterValue();

      LoadCars();
    }

    private void SetWindowHeaderText(Object sender, StatusEventArgs e)
    {
      Text = string.Concat("BBAuto пользователь: ", User.GetDriver().GetName(NameType.Short), " Справочник: ",
        _mainStatus.ToString());
    }

    private void ConfigContextMenu(Object sender, StatusEventArgs e)
    {
      _myMenu.SetMainDgv(_mainDgv);
      
      var menuStrip = _myMenu.CreateMainMenu();

      Controls.Remove(MainMenuStrip);
      MainMenuStrip = menuStrip;
      Controls.Add(menuStrip);

      _dgvCar.ContextMenuStrip = _myMenu.CreateContextMenu();
    }

    private void mainForm_Load(object sender, EventArgs e)
    {
      _curPosition = new Point(1, 0);
      _savedPosition = new Point(1, 0);

      _mainStatus.Set(Status.Actual);
    }

    private void LoadCars()
    {
      LoadCars(_carService.ToDataTable(_mainStatus.Get()));
    }

    private void LoadCars(DataTable dt)
    {
      _dgvCar.Columns.Clear();
      _dgvCar.DataSource = dt;

      FormatDgv();

      _myFilter.tryCreateComboBox();

      SetColumnsWidth();

      _myStatusStrip.writeStatus();
    }

    private void SetColumnsWidth()
    {
      var columnSize = GetColumnSize();

      for (var i = 0; i < _dgvCar.Columns.Count; i++)
      {
        _dgvCar.Columns[i].Width = columnSize.GetSize(i);
      }
    }

    private void _dgvCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      var point = new Point(e.ColumnIndex, e.RowIndex);

      /*TO DO: для Столяровой открыть просмотр всех вкладок*/
      if (User.GetRole() == RolesList.AccountantWayBill)
      {
        if (_mainStatus.Get() == Status.Driver)
          DoubleClickDriver(point);
        if (_mainStatus.Get() == Status.Actual)
          DoubleClickDefault(point);
        return;
      }

      if (isCellNoHeader(e.RowIndex))
      {
        if (_dgvCar.Columns[e.ColumnIndex].HeaderText == Columns.BBNumber &&
            _mainStatus.Get() != Status.AccountViolation)
          DoubleClickDefault(point);
        else
        {
          switch (_mainStatus.Get())
          {
            case Status.Sale:
              DoubleClickSale(point);
              break;
            case Status.Invoice:
              DoubleClickInvoice(point);
              break;
            case Status.Policy:
              DoubleClickPolicy(point);
              break;
            case Status.DTP:
              DoubleClickDTP(point);
              break;
            case Status.Violation:
              DoubleClickViolation(point);
              break;
            case Status.DiagCard:
              DoubleClickDiagCard(point);
              break;
            case Status.TempMove:
              DoubleClickTempMove(point);
              break;
            case Status.ShipPart:
              DoubleClickShipPart(point);
              break;
            case Status.Account:
              DoubleClickAccount(point);
              break;
            case Status.AccountViolation:
              DoubleClickAccountViolation(point);
              break;
            case Status.FuelCard:
              DoubleClickFuelCard(point);
              break;
            case Status.Driver:
              DoubleClickDriver(point);
              break;
            default:
              DoubleClickDefault(point);
              break;
          }
        }
      }
    }

    private void DoubleClickSale(Point point)
    {
      var carId = _mainDgv.GetCarId();
      var car = _carService.GetCars().FirstOrDefault(c => c.Id == carId);
      if (car == null)
        return;

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(car.Id);

      var stsList = STSList.getInstance();
      var sts = stsList.getItem(car.Id);

      if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberPTS && !string.IsNullOrEmpty(pts.File))
        WorkWithFiles.OpenFile(pts.File);
      else if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberSTS && !string.IsNullOrEmpty(sts.File))
        WorkWithFiles.OpenFile(sts.File);
      else
      {
        var saleCars = _saleCarService.GetSaleCars();
        var carSale = saleCars.FirstOrDefault(c => c.Id == car.Id);

        if (_saleCarForm.ShowDialog(carSale) == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickInvoice(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.GetItem(_mainDgv.GetId());

      if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberInvoice && !string.IsNullOrEmpty(invoice.File))
        WorkWithFiles.OpenFile(invoice.File);
      else
      {
        if (InvoiceDialog.Open(invoice))
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickPolicy(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(_mainDgv.GetId());

      string columnName = _dgvCar.Columns[point.X].HeaderText;

      if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberPolicy && !string.IsNullOrEmpty(policy.File))
        WorkWithFiles.OpenFile(policy.File);
      else if (DGVSpecialColumn.CanFiltredPolicy(columnName)
      ) // (labelList.Where(item => item.Text == columnName).Count() == 1)
        _myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
      else
      {
        Policy_AddEdit policyAE = new Policy_AddEdit(policy);
        if (policyAE.ShowDialog() == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickDTP(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      DTPList dtpList = DTPList.getInstance();
      DTP dtp = dtpList.getItem(_mainDgv.GetId());

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);
      if (dtpAE.ShowDialog() == DialogResult.OK)
      {
        LoadCars();
      }
    }

    private void DoubleClickViolation(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      ViolationList violationList = ViolationList.getInstance();
      Violation violation = violationList.getItem(_mainDgv.GetId());

      if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberViolation && !string.IsNullOrEmpty(violation.File))
        WorkWithFiles.OpenFile(violation.File);
      else if (_dgvCar.Columns[point.X].HeaderText == Columns.PaymentDate && !string.IsNullOrEmpty(violation.FilePay))
        WorkWithFiles.OpenFile(violation.FilePay);
      else
      {
        if (_formViolation.ShowDialog(violation) == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickDiagCard(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      var diagCard = _diagCardService.GetByCarId(_mainDgv.GetId());

      if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberDiagCard && !string.IsNullOrEmpty(diagCard.File))
        WorkWithFiles.OpenFile(diagCard.File);
      else
      {
        if (_diagCardForm.ShowDialog(diagCard) == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickTempMove(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      TempMoveList tempMoveList = TempMoveList.getInstance();
      TempMove tempMove = tempMoveList.GetItem(_mainDgv.GetId());

      TempMove_AddEdit tempMoveAE = new TempMove_AddEdit(tempMove);
      if (tempMoveAE.ShowDialog() == DialogResult.OK)
      {
        LoadCars();
      }
    }

    private void DoubleClickShipPart(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      ShipPartList shipPartList = ShipPartList.getInstance();
      ShipPart shipPart = shipPartList.getItem(_mainDgv.GetId());

      ShipPart_AddEdit shipPartAE = new ShipPart_AddEdit(shipPart);
      if (shipPartAE.ShowDialog() == DialogResult.OK)
      {
        LoadCars();
      }
    }

    private void DoubleClickAccount(Point point)
    {
      try
      {
        if (_mainDgv.GetId() == 0)
          return;

        AccountList accountListList = AccountList.GetInstance();
        Account account = accountListList.getItem(_mainDgv.GetId());

        if (_dgvCar.Columns[point.X].HeaderText == Columns.File && !string.IsNullOrEmpty(account.File))
          WorkWithFiles.OpenFile(account.File);
        else if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberAccount)
          GotoPagePolicy(account);
        else if (_dgvCar.Columns[point.X].HeaderText == Columns.Agreement && account.CanAgree())
        {
          if (account.File == string.Empty)
            throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");

          if (User.GetRole() == RolesList.Boss || User.GetRole() == RolesList.Adminstrator)
          {
            account.Agree();
            LoadCars();
          }
          else
            throw new AccessViolationException("Вы не имеете прав на выполнение этой операции");
        }
        else
        {
          Account_AddEdit accountAE = new Account_AddEdit(account);
          if (accountAE.ShowDialog() == DialogResult.OK)
          {
            LoadCars();
          }
        }
      }
      catch (NotImplementedException ex)
      {
        MessageBox.Show(ex.Message, Captions.FailedSend, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, Captions.FailedSend, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (AccessViolationException ex)
      {
        MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DoubleClickAccountViolation(Point point)
    {
      try
      {
        var id = _mainDgv.GetId();
        if (id == 0)
          return;

        var violation = ViolationList.getInstance().getItem(id);

        var columnName = _dgvCar.Columns[point.X].HeaderText;

        if ((_dgvCar.Columns[point.X].HeaderText == Columns.NumberViolation ||
             _dgvCar.Columns[point.X].HeaderText == Columns.ViolationAmount)
            && !string.IsNullOrEmpty(violation.File))
          WorkWithFiles.OpenFile(violation.File);
        else if (_dgvCar.Columns[point.X].HeaderText == Columns.Agreement && (!violation.Agreed))
        {
          if (violation.File == string.Empty)
            throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");
          if (User.GetRole() == RolesList.Boss || User.GetRole() == RolesList.Adminstrator)
          {
            violation.Agree();
            LoadCars();
          }
          else
            throw new AccessViolationException("Вы не имеете прав на выполнение этой операции");
        }
        else if (DGVSpecialColumn.CanInclude(columnName))
          _myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
        else
        {
          if (_formViolation.ShowDialog(violation) == DialogResult.OK)
          {
            LoadCars();
          }
        }
      }
      catch (NotImplementedException ex)
      {
        MessageBox.Show(ex.Message, Captions.FailedSend, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, Captions.FailedSend, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (AccessViolationException ex)
      {
        MessageBox.Show(ex.Message, Captions.AccessError, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void GotoPagePolicy(Account account)
    {
      _savedPosition = new Point(_dgvCar.SelectedCells[0].RowIndex, _dgvCar.SelectedCells[0].ColumnIndex);
      _mainStatus.Set(Status.Policy);
      PolicyList policyList = PolicyList.getInstance();
      DataTable dt = policyList.ToDataTable(account);
      btnBack.Visible = true;
      LoadCars(dt);
    }

    private void DoubleClickFuelCard(Point point)
    {
      var id = _mainDgv.GetCarId();
      if (id == 0)
        return;

      FuelCardList fuelCardList = FuelCardList.getInstance();
      FuelCard fuelCard = fuelCardList.getItem(id);

      FuelCard_AddEdit fuelCardAddEdit = new FuelCard_AddEdit(fuelCard);
      if (fuelCardAddEdit.ShowDialog() == DialogResult.OK)
        LoadCars();
    }

    private void DoubleClickDriver(Point point)
    {
      if (_mainDgv.GetId() == 0)
        return;

      DriverList driverList = DriverList.getInstance();
      DriverForm driverAddEdit = new DriverForm(driverList.getItem(_mainDgv.GetId()));

      if (driverAddEdit.ShowDialog() == DialogResult.OK)
        LoadCars();
    }

    private void DoubleClickDefault(Point point)
    {
      var carId = _mainDgv.GetCarId();
      if (carId == 0)
        return;

      var car = _carService.GetCarById(carId);

      /*TODO: Столяровой доступ к информации про водителя и основную о машине */
      if (User.GetDriver().UserRole == RolesList.AccountantWayBill && _dgvCar.Columns[point.X].HeaderText != Columns.Driver)
      {
        OpenCarAddEdit(car);
        return;
      }

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(carId);

      STSList stsList = STSList.getInstance();
      STS sts = stsList.getItem(carId);

      string columnName = _dgvCar.Columns[point.X].HeaderText;

      if (_dgvCar.Columns[point.X].HeaderText == Columns.VIN)
      {
        formCarInfo formcarInfo = new formCarInfo(car);
        formcarInfo.ShowDialog();
      }
      else if (_dgvCar.Columns[point.X].HeaderText == Columns.Driver)
      {
        if (isCellNoHeader(point.X))
        {
          DriverCarList driverCarList = DriverCarList.getInstance();
          Driver driver = driverCarList.GetDriver(car.Id);

          if (driver == null)
          {
            return;
          }

          DriverList driverList = DriverList.getInstance();
          DriverForm dAE = new DriverForm(driver);
          if (dAE.ShowDialog() == DialogResult.OK)
          {
            LoadCars();
          }
        }
      }
      else if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberPTS && !string.IsNullOrEmpty(pts.File))
      {
        WorkWithFiles.OpenFile(pts.File);
      }
      else if (_dgvCar.Columns[point.X].HeaderText == Columns.NumberSTS && !string.IsNullOrEmpty(sts.File))
      {
        WorkWithFiles.OpenFile(sts.File);
      }
      else if (DGVSpecialColumn.CanFiltredActual(columnName))
        _myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
      else
      {
        OpenCarAddEdit(car);
      }
    }

    private void OpenCarAddEdit(CarModel car)
    {
      //var carAE = new CarForm(car);
      //_carForm.Create(car);
      
      if (_carForm.ShowDialog(car) == DialogResult.OK)
      {
        LoadCars();
      }
    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }

    private void _dgvCar_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {
      _curPosition.X = e.ColumnIndex;
      _curPosition.Y = e.RowIndex;
    }

    private void _dgvCar_SelectionChanged(object sender, EventArgs e)
    {
      _myStatusStrip.WriteCountSelectCell();
    }

    private void _dgvCar_Sorted(object sender, EventArgs e)
    {
      _myFilter.ApplyFilter();
      FormatDgv();
    }

    private void FormatDgv()
    {
      _dgvFormatter.SetDgv(_mainDgv.Dgv);
      _dgvFormatter.HideTwoFirstColumns();

      if (_mainStatus.Get() != Status.FuelCard)
        _dgvFormatter.FormatByOwner();

      _dgvFormatter.FormatDgv(_mainStatus.Get());
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      btnBack.Visible = false;
      _mainStatus.Set(Status.Account);
      LoadCars();
      _dgvCar.CurrentCell = _dgvCar.Rows[_savedPosition.X].Cells[_savedPosition.Y];
    }

    private void _dgvCar_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      var columnSize = GetColumnSize();
      columnSize.SetSize(e.Column.Index, e.Column.Width);
    }

    private ColumnSize GetColumnSize()
    {
      var driver = User.GetDriver();

      var columnSizeList = ColumnSizeList.getInstance();
      return columnSizeList.getItem(driver, _mainStatus.Get());
    }

    private void btnClearFilter_Click(object sender, EventArgs e)
    {
      _myFilter.clearFilterValue();
      LoadCars();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      _myFilter.ApplyFilter();
    }

    private void tbSearch_TextChanged(object sender, EventArgs e)
    {
      Search();
    }

    private void tbSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.SuppressKeyPress = true;
        Search();
      }
    }

    private void Search()
    {
      _seacher.Find(tbSearch.Text);
    }

    private void _dgvCar_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
    {
      _dgvCar.CurrentCell = _dgvCar.Rows[e.RowIndex].Cells[e.ColumnIndex];
    }
  }
}
