using System;
using System.Data;
using System.Drawing;
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
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Static;

namespace BBAuto.App
{
  public partial class MainForm : Form, IForm
  {
    private const string ColumnBbnumber = "Бортовой номер";

    private Point _curPosition;
    private Point _savedPosition;

    private readonly MainStatus _mainStatus;

    private readonly MainDgv _dgvMain;

    private readonly SearchInDgv _seacher;
    
    private readonly MyFilter _myFilter;
    private readonly MyStatusStrip _myStatusStrip;

    private readonly ICarForm _carForm;
    private readonly ICarService _carService;

    public MainForm(
      ICarForm carForm,
      ICarService carService)
    {
      _carForm = carForm;
      _carService = carService;

      InitializeComponent();

      _mainStatus = MainStatus.getInstance();
      _mainStatus.StatusChanged += statusChanged;
      _mainStatus.StatusChanged += SetWindowHeaderText;
      _mainStatus.StatusChanged += ConfigContextMenu;

      _dgvMain = new MainDgv(_dgvCar);

      _seacher = new SearchInDgv(_dgvCar);

      _myStatusStrip = new MyStatusStrip(_dgvCar, statusStrip1);

      _myFilter = MyFilter.GetInstanceCars();
      _myFilter.Fill(_dgvCar, _myStatusStrip, this);
    }

    private void statusChanged(Object sender, StatusEventArgs e)
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
      var menu = new MyMenu(_dgvMain);
      var menuStrip = menu.CreateMainMenu();

      Controls.Remove(MainMenuStrip);
      MainMenuStrip = menuStrip;
      Controls.Add(menuStrip);

      _dgvCar.ContextMenuStrip = menu.CreateContextMenu();
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

      formatDGV();

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
        if (_dgvCar.Columns[e.ColumnIndex].HeaderText == ColumnBbnumber &&
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
      Car car = _dgvMain.GetCar();
      if (car == null)
        return;

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car);

      STSList stsList = STSList.getInstance();
      STS sts = stsList.getItem(car);

      if ((_dgvCar.Columns[point.X].HeaderText == "№ ПТС") && (!string.IsNullOrEmpty(pts.File)))
        WorkWithFiles.OpenFile(pts.File);
      else if ((_dgvCar.Columns[point.X].HeaderText == "№ СТС") && (!string.IsNullOrEmpty(sts.File)))
        WorkWithFiles.OpenFile(sts.File);
      else
      {
        CarSaleList carSaleList = CarSaleList.getInstance();
        CarSale carSale = carSaleList.getItem(car.Id);

        Car_Sale carSaleForm = new Car_Sale(carSale);
        if (carSaleForm.ShowDialog() == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickInvoice(Point point)
    {
      if (_dgvMain.GetId() == 0)
        return;

      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.getItem(_dgvMain.GetId());

      if ((_dgvCar.Columns[point.X].HeaderText == "№ накладной") && (!string.IsNullOrEmpty(invoice.File)))
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
      if (_dgvMain.GetId() == 0)
        return;

      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(_dgvMain.GetId());

      string columnName = _dgvCar.Columns[point.X].HeaderText;

      if ((_dgvCar.Columns[point.X].HeaderText == "Номер полиса") && (!string.IsNullOrEmpty(policy.File)))
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
      if (_dgvMain.GetId() == 0)
        return;

      DTPList dtpList = DTPList.getInstance();
      DTP dtp = dtpList.getItem(_dgvMain.GetId());

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);
      if (dtpAE.ShowDialog() == DialogResult.OK)
      {
        LoadCars();
      }
    }

    private void DoubleClickViolation(Point point)
    {
      if (_dgvMain.GetId() == 0)
        return;

      ViolationList violationList = ViolationList.getInstance();
      Violation violation = violationList.getItem(_dgvMain.GetId());

      if ((_dgvCar.Columns[point.X].HeaderText == "№ постановления") && (!string.IsNullOrEmpty(violation.File)))
        WorkWithFiles.OpenFile(violation.File);
      else if ((_dgvCar.Columns[point.X].HeaderText == "Дата оплаты") && (!string.IsNullOrEmpty(violation.FilePay)))
        WorkWithFiles.OpenFile(violation.FilePay);
      else
      {
        Violation_AddEdit vAE = new Violation_AddEdit(violation);
        if (vAE.ShowDialog() == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickDiagCard(Point point)
    {
      if (_dgvMain.GetId() == 0)
        return;

      DiagCardList diagCardList = DiagCardList.getInstance();
      DiagCard diagCard = diagCardList.getItem(_dgvMain.GetId());

      if ((_dgvCar.Columns[point.X].HeaderText == "№ ДК") && (!string.IsNullOrEmpty(diagCard.File)))
        WorkWithFiles.OpenFile(diagCard.File);
      else
      {
        DiagCard_AddEdit diagCardAE = new DiagCard_AddEdit(diagCard);
        if (diagCardAE.ShowDialog() == DialogResult.OK)
        {
          LoadCars();
        }
      }
    }

    private void DoubleClickTempMove(Point point)
    {
      if (_dgvMain.GetId() == 0)
        return;

      TempMoveList tempMoveList = TempMoveList.getInstance();
      TempMove tempMove = tempMoveList.getItem(_dgvMain.GetId());

      TempMove_AddEdit tempMoveAE = new TempMove_AddEdit(tempMove);
      if (tempMoveAE.ShowDialog() == DialogResult.OK)
      {
        LoadCars();
      }
    }

    private void DoubleClickShipPart(Point point)
    {
      if (_dgvMain.GetId() == 0)
        return;

      ShipPartList shipPartList = ShipPartList.getInstance();
      ShipPart shipPart = shipPartList.getItem(_dgvMain.GetId());

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
        if (_dgvMain.GetId() == 0)
          return;

        AccountList accountListList = AccountList.GetInstance();
        Account account = accountListList.getItem(_dgvMain.GetId());

        if ((_dgvCar.Columns[point.X].HeaderText == "Файл") && (!string.IsNullOrEmpty(account.File)))
          WorkWithFiles.OpenFile(account.File);
        else if (_dgvCar.Columns[point.X].HeaderText == "Номер счёта")
          GotoPagePolicy(account);
        else if ((_dgvCar.Columns[point.X].HeaderText == "Согласование") && (account.CanAgree()))
        {
          if (account.File == string.Empty)
            throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");
          else if ((User.GetRole() == RolesList.Boss) || (User.GetRole() == RolesList.Adminstrator))
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
        MessageBox.Show(ex.Message, "Ошибка отправки", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка отправки", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (AccessViolationException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DoubleClickAccountViolation(Point point)
    {
      try
      {
        int id = _dgvMain.GetId();
        if (id == 0)
          return;

        Violation violation = ViolationList.getInstance().getItem(id);

        string columnName = _dgvCar.Columns[point.X].HeaderText;

        if (((_dgvCar.Columns[point.X].HeaderText == "№ постановления") ||
             (_dgvCar.Columns[point.X].HeaderText == "Сумма штрафа"))
            && (!string.IsNullOrEmpty(violation.File)))
          WorkWithFiles.OpenFile(violation.File);
        else if ((_dgvCar.Columns[point.X].HeaderText == "Согласование") && (!violation.Agreed))
        {
          if (violation.File == string.Empty)
            throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");
          else if ((User.GetRole() == RolesList.Boss) || (User.GetRole() == RolesList.Adminstrator))
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
          Violation_AddEdit violationAE = new Violation_AddEdit(violation);
          if (violationAE.ShowDialog() == DialogResult.OK)
          {
            LoadCars();
          }
        }
      }
      catch (NotImplementedException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка отправки", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка отправки", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (AccessViolationException ex)
      {
        MessageBox.Show(ex.Message, "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
      int id = _dgvMain.GetCarId();
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
      if (_dgvMain.GetId() == 0)
        return;

      DriverList driverList = DriverList.getInstance();
      DriverForm driverAddEdit = new DriverForm(driverList.getItem(_dgvMain.GetId()));

      if (driverAddEdit.ShowDialog() == DialogResult.OK)
        LoadCars();
    }

    private void DoubleClickDefault(Point point)
    {
      Car car = _dgvMain.GetCar();
      if (car == null)
        return;

      /*TODO: Столяровой доступ к информации про водителя и основную о машине */
      if (User.GetDriver().UserRole == RolesList.AccountantWayBill && _dgvCar.Columns[point.X].HeaderText != "Водитель")
      {
        OpenCarAddEdit(car);
        return;
      }

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car);

      STSList stsList = STSList.getInstance();
      STS sts = stsList.getItem(car);

      string columnName = _dgvCar.Columns[point.X].HeaderText;

      if (_dgvCar.Columns[point.X].HeaderText == "VIN")
      {
        formCarInfo formcarInfo = new formCarInfo(car);
        formcarInfo.ShowDialog();
      }
      else if (_dgvCar.Columns[point.X].HeaderText == "Водитель")
      {
        if (isCellNoHeader(point.X))
        {
          DriverCarList driverCarList = DriverCarList.getInstance();
          Driver driver = driverCarList.GetDriver(car);

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
      else if ((_dgvCar.Columns[point.X].HeaderText == "№ ПТС") && (!string.IsNullOrEmpty(pts.File)))
      {
        WorkWithFiles.OpenFile(pts.File);
      }
      else if ((_dgvCar.Columns[point.X].HeaderText == "№ СТС") && (!string.IsNullOrEmpty(sts.File)))
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

    private void OpenCarAddEdit(Car car)
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
      formatDGV();
    }

    private void formatDGV()
    {
      _dgvMain.Format(_mainStatus.Get());
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
      ColumnSize columnSize = GetColumnSize();
      columnSize.SetSize(e.Column.Index, e.Column.Width);
    }

    private ColumnSize GetColumnSize()
    {
      Driver driver = User.GetDriver();

      ColumnSizeList columnSizeList = ColumnSizeList.getInstance();
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
