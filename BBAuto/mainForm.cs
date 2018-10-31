using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BBAuto.AddEdit;
using BBAuto.ContextMenu;
using BBAuto.Domain.Common;
using BBAuto.Domain.Lists;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Static;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Services.CarSale;
using BBAuto.Domain.Services.Transponder;
using BBAuto.FormsForCar.AddEdit;

namespace BBAuto
{
  public partial class mainForm : Form
  {
    private const string ColumnBbnumber = "Бортовой номер";

    private Point _curPosition;
    private Point _savedPosition;

    private readonly MainStatus _mainStatus;

    private readonly MainDGV _dgvMain;

    private readonly SearchInDgv _seacher;

    private readonly CarList _carList;

    private readonly MyFilter _myFilter;
    private readonly MyStatusStrip _myStatusStrip;
    
    public mainForm()
    {
      InitializeComponent();

      _carList = CarList.GetInstance();
      _mainStatus = MainStatus.getInstance();
      _mainStatus.StatusChanged += StatusChanged;
      _mainStatus.StatusChanged += SetWindowHeaderText;
      _mainStatus.StatusChanged += ConfigContextMenu;

      _dgvMain = new MainDGV(_dgvCar);

      _seacher = new SearchInDgv(_dgvCar);

      _myStatusStrip = new MyStatusStrip(_dgvCar, statusStrip1);

      _myFilter = MyFilter.GetInstanceCars();
      _myFilter.Fill(_dgvCar, _myStatusStrip, this);
    }

    private void StatusChanged(Object sender, StatusEventArgs e)
    {
      _myFilter.clearComboList();
      _myFilter.clearFilterValue();

      _dgvCar.Columns.Clear();

      FillDataGridView();

      _dgvMain.Format(_mainStatus.Get());

      _myFilter.tryCreateComboBox();

      SetColumnsWidth();
    }

    private void SetWindowHeaderText(Object sender, StatusEventArgs e)
    {
      Text = string.Concat("BBAuto. Пользователь: ", User.GetDriver().Name, " Справочник: ",
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
    
    private DataTable GetDataTable()
    {
      switch (_mainStatus.Get())
      {
        case Status.Buy:
          return _carList.ToDataTableBuy();
        case Status.Actual:
          return _carList.ToDataTableActual();
        case Status.Repair:
          return RepairList.getInstance().ToDataTable();
        case Status.Sale:
        {
          ICarSaleService carSaleService = new CarSaleService();
          return carSaleService.ToDataTable();
        }
        case Status.Invoice:
          return InvoiceList.getInstance().ToDataTable();
        case Status.Policy:
          return PolicyList.getInstance().ToDataTable();
        case Status.DTP:
          return DTPList.getInstance().ToDataTable();
        case Status.Violation:
          return ViolationList.getInstance().ToDataTable();
        case Status.DiagCard:
          return DiagCardList.getInstance().ToDataTable();
        case Status.TempMove:
          return TempMoveList.getInstance().ToDataTable();
        case Status.ShipPart:
          return ShipPartList.getInstance().ToDataTable();
        case Status.Account:
          return AccountList.GetInstance().ToDataTable();
        case Status.AccountViolation:
          return ViolationList.getInstance().ToDataTableAccount();
        case Status.FuelCard:
          return FuelCardList.getInstance().ToDataTable();
        case Status.Driver:
          return DriverList.getInstance().ToDataTable();
        case Status.Transponder:
        {
          ITransponderService transponderService = new TransponderService();
          return transponderService.GetReportTransponderList();
        }
        default:
          return _carList.ToDataTable();
      }
    }

    private void FillDataGridView(DataTable dt = null)
    {
      if (dt == null)
        dt = GetDataTable();
      
      _dgvCar.DataSource = dt;

      _myStatusStrip.writeStatus();

      _myFilter.ApplyFilter();
    }

    private void SetColumnsWidth()
    {
      ColumnSize columnSize = GetColumnSize();

      for (int i = 0; i < _dgvCar.Columns.Count; i++)
      {
        _dgvCar.Columns[i].Width = columnSize.GetSize(i);
      }
    }

    private void _dgvCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      Point point = new Point(e.ColumnIndex, e.RowIndex);

      /*TO DO: для Столяровой открыть просмотр всех вкладок*/
      if (User.GetRole() == RolesList.AccountantWayBill)
      {
        if (_mainStatus.Get() == Status.Driver)
          DoubleClickDriver();
        if (_mainStatus.Get() == Status.Actual)
          DoubleClickDefault(point);
        return;
      }

      if (isCellNoHeader(e.RowIndex))
      {
        if ((_dgvCar.Columns[e.ColumnIndex].HeaderText == ColumnBbnumber) &&
            (_mainStatus.Get() != Status.AccountViolation))
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
              DoubleClickDTP();
              break;
            case Status.Violation:
              DoubleClickViolation(point);
              break;
            case Status.DiagCard:
              DoubleClickDiagCard(point);
              break;
            case Status.TempMove:
              DoubleClickTempMove();
              break;
            case Status.ShipPart:
              DoubleClickShipPart();
              break;
            case Status.Account:
              DoubleClickAccount(point);
              break;
            case Status.AccountViolation:
              DoubleClickAccountViolation(point);
              break;
            case Status.FuelCard:
              DoubleClickFuelCard();
              break;
            case Status.Driver:
              DoubleClickDriver();
              break;
            case Status.Transponder:
              DoubleClickTransponder();
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
        WorkWithFiles.openFile(pts.File);
      else if ((_dgvCar.Columns[point.X].HeaderText == "№ СТС") && (!string.IsNullOrEmpty(sts.File)))
        WorkWithFiles.openFile(sts.File);
      else
      {
        var carSaleForm = new CarSaleForm(car.ID);
        if (carSaleForm.ShowDialog() == DialogResult.OK)
        {
          FillDataGridView();
        }
      }
    }

    private void DoubleClickInvoice(Point point)
    {
      if (_dgvMain.GetID() == 0)
        return;

      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.getItem(_dgvMain.GetID());

      if ((_dgvCar.Columns[point.X].HeaderText == "№ накладной") && (!string.IsNullOrEmpty(invoice.File)))
        WorkWithFiles.openFile(invoice.File);
      else
      {
        if (InvoiceDialog.Open(invoice))
        {
          FillDataGridView();
        }
      }
    }

    private void DoubleClickPolicy(Point point)
    {
      if (_dgvMain.GetID() == 0)
        return;

      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(_dgvMain.GetID());

      string columnName = _dgvCar.Columns[point.X].HeaderText;

      if ((_dgvCar.Columns[point.X].HeaderText == "Номер полиса") && (!string.IsNullOrEmpty(policy.File)))
        WorkWithFiles.openFile(policy.File);
      else if (DGVSpecialColumn.CanFiltredPolicy(columnName)
      ) // (labelList.Where(item => item.Text == columnName).Count() == 1)
        _myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
      else
      {
        PolicyForm policyAE = new PolicyForm(policy);
        if (policyAE.ShowDialog() == DialogResult.OK)
        {
          FillDataGridView();
        }
      }
    }

    private void DoubleClickDTP()
    {
      if (_dgvMain.GetID() == 0)
        return;

      DTPList dtpList = DTPList.getInstance();
      DTP dtp = dtpList.getItem(_dgvMain.GetID());

      DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);
      if (dtpAE.ShowDialog() == DialogResult.OK)
      {
        FillDataGridView();
      }
    }

    private void DoubleClickViolation(Point point)
    {
      if (_dgvMain.GetID() == 0)
        return;

      ViolationList violationList = ViolationList.getInstance();
      Violation violation = violationList.getItem(_dgvMain.GetID());

      if ((_dgvCar.Columns[point.X].HeaderText == "№ постановления") && (!string.IsNullOrEmpty(violation.File)))
        WorkWithFiles.openFile(violation.File);
      else if ((_dgvCar.Columns[point.X].HeaderText == "Дата оплаты") && (!string.IsNullOrEmpty(violation.FilePay)))
        WorkWithFiles.openFile(violation.FilePay);
      else
      {
        ViolationForm vAE = new ViolationForm(violation);
        if (vAE.ShowDialog() == DialogResult.OK)
        {
          FillDataGridView();
        }
      }
    }

    private void DoubleClickDiagCard(Point point)
    {
      if (_dgvMain.GetID() == 0)
        return;

      DiagCardList diagCardList = DiagCardList.getInstance();
      DiagCard diagCard = diagCardList.getItem(_dgvMain.GetID());

      if ((_dgvCar.Columns[point.X].HeaderText == "№ ДК") && (!string.IsNullOrEmpty(diagCard.File)))
        WorkWithFiles.openFile(diagCard.File);
      else
      {
        DiagCard_AddEdit diagCardAE = new DiagCard_AddEdit(diagCard);
        if (diagCardAE.ShowDialog() == DialogResult.OK)
        {
          FillDataGridView();
        }
      }
    }

    private void DoubleClickTempMove()
    {
      if (_dgvMain.GetID() == 0)
        return;

      TempMoveList tempMoveList = TempMoveList.getInstance();
      TempMove tempMove = tempMoveList.getItem(_dgvMain.GetID());

      TempMove_AddEdit tempMoveAE = new TempMove_AddEdit(tempMove);
      if (tempMoveAE.ShowDialog() == DialogResult.OK)
      {
        FillDataGridView();
      }
    }

    private void DoubleClickShipPart()
    {
      if (_dgvMain.GetID() == 0)
        return;

      ShipPartList shipPartList = ShipPartList.getInstance();
      ShipPart shipPart = shipPartList.getItem(_dgvMain.GetID());

      ShipPart_AddEdit shipPartAE = new ShipPart_AddEdit(shipPart);
      if (shipPartAE.ShowDialog() == DialogResult.OK)
      {
        FillDataGridView();
      }
    }

    private void DoubleClickAccount(Point point)
    {
      try
      {
        if (_dgvMain.GetID() == 0)
          return;

        AccountList accountListList = AccountList.GetInstance();
        Account account = accountListList.getItem(_dgvMain.GetID());

        if ((_dgvCar.Columns[point.X].HeaderText == "Файл") && (!string.IsNullOrEmpty(account.File)))
          WorkWithFiles.openFile(account.File);
        else if (_dgvCar.Columns[point.X].HeaderText == "Номер счёта")
          GotoPagePolicy(account);
        else if ((_dgvCar.Columns[point.X].HeaderText == "Согласование") && (account.CanAgree()))
        {
          if (account.File == string.Empty)
            throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");
          else if ((User.GetRole() == RolesList.Boss) || (User.GetRole() == RolesList.Adminstrator))
          {
            account.Agree();
            FillDataGridView();
          }
          else
            throw new AccessViolationException("Вы не имеете прав на выполнение этой операции");
        }
        else
        {
          Account_AddEdit accountAE = new Account_AddEdit(account);
          if (accountAE.ShowDialog() == DialogResult.OK)
          {
            FillDataGridView();
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
        int id = _dgvMain.GetID();
        if (id == 0)
          return;

        Violation violation = ViolationList.getInstance().getItem(id);

        string columnName = _dgvCar.Columns[point.X].HeaderText;

        if (((_dgvCar.Columns[point.X].HeaderText == "№ постановления") ||
             (_dgvCar.Columns[point.X].HeaderText == "Сумма штрафа"))
            && (!string.IsNullOrEmpty(violation.File)))
          WorkWithFiles.openFile(violation.File);
        else if ((_dgvCar.Columns[point.X].HeaderText == "Согласование") && (!violation.Agreed))
        {
          if (violation.File == string.Empty)
            throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");
          else if ((User.GetRole() == RolesList.Boss) || (User.GetRole() == RolesList.Adminstrator))
          {
            violation.Agree();
            FillDataGridView();
          }
          else
            throw new AccessViolationException("Вы не имеете прав на выполнение этой операции");
        }
        else if (DGVSpecialColumn.CanInclude(columnName))
          _myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
        else
        {
          ViolationForm violationAE = new ViolationForm(violation);
          if (violationAE.ShowDialog() == DialogResult.OK)
          {
            FillDataGridView();
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
      FillDataGridView(dt);
    }

    private void DoubleClickFuelCard()
    {
      int id = _dgvMain.GetCarID();
      if (id == 0)
        return;

      FuelCardList fuelCardList = FuelCardList.getInstance();
      FuelCard fuelCard = fuelCardList.getItem(id);

      FuelCard_AddEdit fuelCardAddEdit = new FuelCard_AddEdit(fuelCard);
      if (fuelCardAddEdit.ShowDialog() == DialogResult.OK)
        FillDataGridView();
    }

    private void DoubleClickTransponder()
    {
      var id = _dgvMain.GetID();
      if (id == 0)
        return;

      ITransponderService transponderService = new TransponderService();
      var transponder = transponderService.GetTransponder(id);

      var transponderForm = new TransponderForm(transponder);
      if (transponderForm.ShowDialog() == DialogResult.OK)
        FillDataGridView();
    }

    private void DoubleClickDriver()
    {
      if (_dgvMain.GetID() == 0)
        return;

      DriverList driverList = DriverList.getInstance();
      Driver_AddEdit driverAddEdit = new Driver_AddEdit(driverList.getItem(_dgvMain.GetID()));

      if (driverAddEdit.ShowDialog() == DialogResult.OK)
        FillDataGridView();
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
          DriverCarList driverCarList = DriverCarList.GetInstance();
          Driver driver = driverCarList.GetDriver(car);

          if (driver == null)
          {
            return;
          }

          DriverList driverList = DriverList.getInstance();
          Driver_AddEdit dAE = new Driver_AddEdit(driver);
          if (dAE.ShowDialog() == DialogResult.OK)
          {
            FillDataGridView();
          }
        }
      }
      else if ((_dgvCar.Columns[point.X].HeaderText == "№ ПТС") && (!string.IsNullOrEmpty(pts.File)))
      {
        WorkWithFiles.openFile(pts.File);
      }
      else if ((_dgvCar.Columns[point.X].HeaderText == "№ СТС") && (!string.IsNullOrEmpty(sts.File)))
      {
        WorkWithFiles.openFile(sts.File);
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
      var carForm = new CarForm(car);
      if (carForm.ShowDialog() == DialogResult.OK)
      {
        FillDataGridView();
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
      _dgvMain.Format(_mainStatus.Get());
    }
    
    private void btnBack_Click(object sender, EventArgs e)
    {
      btnBack.Visible = false;
      _mainStatus.Set(Status.Account);
      FillDataGridView();
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

      ColumnSizeList columnSizeList = ColumnSizeList.GetInstance();
      return columnSizeList.getItem(driver, _mainStatus.Get());
    }

    private void btnClearFilter_Click(object sender, EventArgs e)
    {
      _myFilter.clearFilterValue();
      FillDataGridView();
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

    private void btnUpdateData_Click(object sender, EventArgs e)
    {
      ReLoadData();

      FillDataGridView();
    }

    private void ReLoadData()
    {
      switch (_mainStatus.Get())
      {
        case Status.Buy:
        case Status.Actual:
          _carList.ReLoad();
          break;
        case Status.Repair:
          RepairList.getInstance().ReLoad();
          break;
        case Status.Sale:
          break;
        case Status.Invoice:
          InvoiceList.getInstance().ReLoad();
          break;
        case Status.Policy:
          PolicyList.getInstance().ReLoad();
          break;
        case Status.DTP:
          DTPList.getInstance().ReLoad();
          break;
        case Status.Violation:
          ViolationList.getInstance().ReLoad();
          break;
        case Status.DiagCard:
          DiagCardList.getInstance().ReLoad();
          break;
        case Status.TempMove:
          TempMoveList.getInstance().ReLoad();
          break;
        case Status.ShipPart:
          ShipPartList.getInstance().ReLoad();
          break;
        case Status.Account:
          AccountList.GetInstance().ReLoad();
          break;
        case Status.AccountViolation:
          ViolationList.getInstance().ReLoad();
          break;
        case Status.FuelCard:
          FuelCardList.getInstance().ReLoad();
          break;
        case Status.Driver:
          DriverList.getInstance().ReLoad();
          break;
        case Status.Transponder:
          break;
      }
    }
  }
}
