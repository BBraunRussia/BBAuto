﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;
using PresentationControls;

namespace BBAuto
{
    public partial class mainForm : Form
    {
        private const string COLUMN_BBNUMBER = "Бортовой номер";
        
        Point curPosition;
        Point savedPosition;
        
        MainStatus mainStatus;

        private MainDGV _dgvMain;

        private SearchInDgv _seacher;
                
        CarList carList;

        private MyFilter _myFilter;
        private MyStatusStrip _myStatusStrip;
        
        public mainForm()
        {
            InitializeComponent();
            
            carList = CarList.getInstance();
            mainStatus = MainStatus.getInstance();
            mainStatus.StatusChanged += statusChanged;
            mainStatus.StatusChanged += SetWindowHeaderText;
            mainStatus.StatusChanged += ConfigContextMenu;

            _dgvMain = new MainDGV(_dgvCar);
            
            _seacher = new SearchInDgv(_dgvCar);

            _myStatusStrip = new MyStatusStrip(_dgvCar, statusStrip1);

            _myFilter = MyFilter.GetInstanceCars();
            _myFilter.Fill(_dgvCar, _myStatusStrip, this);
        }

        private void statusChanged(Object sender, StatusEventArgs e)
        {
            _myFilter.clearComboList();
            _myFilter.clearFilterValue();

            loadCars();
        }

        private void SetWindowHeaderText(Object sender, StatusEventArgs e)
        {
            this.Text = string.Concat("BBAuto пользователь: ", User.getDriver().GetName(NameType.Short), " Справочник: ", mainStatus.ToString());
        }

        private void ConfigContextMenu(Object sender, StatusEventArgs e)
        {
            MyMenu menu = new MyMenu(_dgvMain);
            MenuStrip menuStrip = menu.CreateMainMenu();

            this.Controls.Remove(this.MainMenuStrip);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            _dgvCar.ContextMenuStrip = menu.CreateContextMenu();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            curPosition = new Point(1, 0);
            savedPosition = new Point(1, 0);

            mainStatus.Set(Status.Actual);
        }

        private void loadCars()
        {
            loadCars(carList.ToDataTable(mainStatus.Get()));
        }

        private void loadCars(DataTable dt)
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
            ColumnSize columnSize = GetColumnSize();

            for (int i = 0; i < _dgvCar.Columns.Count; i++)
            {
                _dgvCar.Columns[i].Width = columnSize.GetSize(i);
            }
        }
        
        private void _dgvCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Point point = new Point(e.ColumnIndex, e.RowIndex);

            if (User.GetRole() == RolesList.AccountantWayBill)
            {
                if (mainStatus.Get() == Status.Driver)
                    DoubleClickDriver(point);
                if (mainStatus.Get() == Status.Actual)
                    DoubleClickDefault(point);
                return;
            }
            
            if (isCellNoHeader(e.RowIndex))
            {
                if (_dgvCar.Columns[e.ColumnIndex].HeaderText == COLUMN_BBNUMBER)
                    DoubleClickDefault(point);
                else
                {
                    switch (mainStatus.Get())
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
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = carList.getItem(_dgvMain.GetCarID());
            
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
                CarSaleList carSaleList = CarSaleList.getInstance();
                CarSale carSale = carSaleList.getItem(car);

                Car_Sale carSaleForm = new Car_Sale(carSale);
                if (carSaleForm.ShowDialog() == DialogResult.OK)
                {
                    loadCars();
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
                    loadCars();
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
            else if (DGVSpecialColumn.CanFiltredPolicy(columnName)) // (labelList.Where(item => item.Text == columnName).Count() == 1)
                _myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
            else
            {
                Policy_AddEdit policyAE = new Policy_AddEdit(policy);
                if (policyAE.ShowDialog() == DialogResult.OK)
                {
                    loadCars();
                }
            }
        }

        private void DoubleClickDTP(Point point)
        {
            if (_dgvMain.GetID() == 0)
                return;

            DTPList dtpList = DTPList.getInstance();
            DTP dtp = dtpList.getItem(_dgvMain.GetID());

            DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);
            if (dtpAE.ShowDialog() == DialogResult.OK)
            {
                loadCars();
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
                Violation_AddEdit vAE = new Violation_AddEdit(violation);
                if (vAE.ShowDialog() == DialogResult.OK)
                {
                    loadCars();
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
                    loadCars();
                }
            }
        }
        
        private void DoubleClickTempMove(Point point)
        {
            if (_dgvMain.GetID() == 0)
                return;

            TempMoveList tempMoveList = TempMoveList.getInstance();
            TempMove tempMove = tempMoveList.getItem(_dgvMain.GetID());

            TempMove_AddEdit tempMoveAE = new TempMove_AddEdit(tempMove);
            if (tempMoveAE.ShowDialog() == DialogResult.OK)
            {
                loadCars();
            }
        }

        private void DoubleClickShipPart(Point point)
        {
            if (_dgvMain.GetID() == 0)
                return;

            ShipPartList shipPartList = ShipPartList.getInstance();
            ShipPart shipPart = shipPartList.getItem(_dgvMain.GetID());

            ShipPart_AddEdit shipPartAE = new ShipPart_AddEdit(shipPart);
            if (shipPartAE.ShowDialog() == DialogResult.OK)
            {
                loadCars();
            }
        }

        private void DoubleClickAccount(Point point)
        {
            try
            {
                if (_dgvMain.GetID() == 0)
                    return;

                AccountList accountListList = AccountList.getInstance();
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
                        loadCars();
                    }
                    else
                        throw new AccessViolationException("Вы не имеете прав на выполнение этой операции");
                }
                else
                {
                    Account_AddEdit accountAE = new Account_AddEdit(account);
                    if (accountAE.ShowDialog() == DialogResult.OK)
                    {
                        loadCars();
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
                if (_dgvMain.GetID() == 0)
                    return;

                Violation violation = ViolationList.getInstance().getItem(_dgvMain.GetID());

                if ((_dgvCar.Columns[point.X].HeaderText == "Файл") && (!string.IsNullOrEmpty(violation.File)))
                    WorkWithFiles.openFile(violation.File);
                else if ((_dgvCar.Columns[point.X].HeaderText == "Согласование") && (!violation.Agreed))
                {
                    if (violation.File == string.Empty)
                        throw new NotImplementedException("Для согласования необходимо прикрепить скан копию счёта");
                    else if ((User.GetRole() == RolesList.Boss) || (User.GetRole() == RolesList.Adminstrator))
                    {
                        violation.Agree();
                        loadCars();
                    }
                    else
                        throw new AccessViolationException("Вы не имеете прав на выполнение этой операции");
                }
                else
                {
                    Violation_AddEdit violationAE = new Violation_AddEdit(violation);
                    if (violationAE.ShowDialog() == DialogResult.OK)
                    {
                        loadCars();
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

        private void GotoPagePolicy(Account account)
        {
            savedPosition = new Point(_dgvCar.SelectedCells[0].RowIndex, _dgvCar.SelectedCells[0].ColumnIndex);
            mainStatus.Set(Status.Policy);
            PolicyList policyList = PolicyList.getInstance();
            DataTable dt = policyList.ToDataTable(account);
            btnBack.Visible = true;
            loadCars(dt);
        }

        private void DoubleClickFuelCard(Point point)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            FuelCardList fuelCardList = FuelCardList.getInstance();
            FuelCard fuelCard = fuelCardList.getItem(_dgvMain.GetCarID());

            FuelCard_AddEdit fuelCardAddEdit = new FuelCard_AddEdit(fuelCard);
            if (fuelCardAddEdit.ShowDialog() == DialogResult.OK)
                loadCars();
        }

        private void DoubleClickDriver(Point point)
        {
            if (_dgvMain.GetID() == 0)
                return;

            DriverList driverList = DriverList.getInstance();
            Driver_AddEdit driverAddEdit = new Driver_AddEdit(driverList.getItem(_dgvMain.GetID()));

            if (driverAddEdit.ShowDialog() == DialogResult.OK)
                loadCars();
        }
        
        private void DoubleClickDefault(Point point)
        {
            if (_dgvMain.GetCarID() == 0)
                return;
            
            Car car = carList.getItem(_dgvMain.GetCarID());

            if (User.getDriver().UserRole == RolesList.AccountantWayBill)
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
                    Driver_AddEdit dAE = new Driver_AddEdit(driver);
                    if (dAE.ShowDialog() == DialogResult.OK)
                    {
                        loadCars();
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
            Car_AddEdit carAE = new Car_AddEdit(car);
            if (carAE.ShowDialog() == DialogResult.OK)
            {
                loadCars();
            }
        }
        
        private bool isCellNoHeader(int rowIndex)
        {
            return rowIndex >= 0;
        }
        
        private void _dgvCar_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            curPosition.X = e.ColumnIndex;
            curPosition.Y = e.RowIndex;
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
            _dgvMain.Format(mainStatus.Get());
        }
                
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.Visible = false;
            mainStatus.Set(Status.Account);
            loadCars();
            _dgvCar.CurrentCell = _dgvCar.Rows[savedPosition.X].Cells[savedPosition.Y];
        }

        private void _dgvCar_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ColumnSize columnSize = GetColumnSize();
            columnSize.SetSize(e.Column.Index, e.Column.Width);
        }

        private ColumnSize GetColumnSize()
        {
            Driver driver = User.getDriver();

            ColumnSizeList columnSizeList = ColumnSizeList.getInstance();
            return columnSizeList.getItem(driver, mainStatus.Get());
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            _myFilter.clearFilterValue();
            loadCars();
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