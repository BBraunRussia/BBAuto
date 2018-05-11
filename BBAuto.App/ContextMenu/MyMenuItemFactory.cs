using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BBAuto.App.Actions;
using BBAuto.App.AddEdit;
using BBAuto.App.config;
using BBAuto.App.Common;
using BBAuto.App.CommonForms;
using BBAuto.App.Dictionary;
using BBAuto.App.FormsForCar;
using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.App.FormsForDriver;
using BBAuto.App.GUI;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Mileage;
using BBAuto.Logic.Static;
using Common.Resources;

namespace BBAuto.App.ContextMenu
{
  public class MyMenuItemFactory : IMyMenuItemFactory
  {
    private const string DocumentsPath = @"\\bbmru08.bbmag.bbraun.com\Depts\Fleet INT\Автохозяйство\документы на авто";

    private MainStatus _mainStatus;

    private readonly IMileageForm _formMileage;
    private readonly ICarForm _carForm;
    private readonly IViolationForm _formViolation;
    private readonly IDiagCardForm _diagCardForm;

    private readonly IMyPointListForm _myPointListForm;
    private readonly IRouteListForm _routeListForm;
    private readonly ITemplateListForm _templateListForm;

    private IMainDgv _mainDgv;

    public MyMenuItemFactory(
      IMileageForm formMileage,
      ICarForm carForm,
      IViolationForm formViolation,
      IDiagCardForm diagCardForm,
      IMyPointListForm myPointListForm,
      IRouteListForm routeListForm,
      ITemplateListForm templateListForm)
    {
      _formMileage = formMileage;
      _carForm = carForm;
      _formViolation = formViolation;
      _diagCardForm = diagCardForm;
      _myPointListForm = myPointListForm;
      _routeListForm = routeListForm;
      _templateListForm = templateListForm;
    }

    public void SetMainDgv(IMainDgv dgvMain)
    {
      _mainDgv = dgvMain;
      _mainStatus = MainStatus.getInstance();
    }

    public ToolStripItem CreateItem(ContextMenuItem item)
    {
      switch (item)
      {
        case ContextMenuItem.Separator:
          return CreateSeparator();
        case ContextMenuItem.NewInvoice:
          return CreateNewInvoice();
        case ContextMenuItem.NewDTP:
          return CreateNewDtp();
        case ContextMenuItem.NewViolation:
          return CreateNewViolation();
        case ContextMenuItem.NewPolicy:
          return CreateNewPolicy();
        case ContextMenuItem.NewDiagCard:
          return CreateNewDiagCard();
        case ContextMenuItem.NewMileage:
          return CreateNewMileage();
        case ContextMenuItem.NewTempMove:
          return CreateNewTempMove();
        case ContextMenuItem.ToSale:
          return CreateToSale();
        case ContextMenuItem.DeleteFromSale:
          return CreateDeleteFromSale();
        case ContextMenuItem.LotusMail:
          return CreateLotusMail();
        case ContextMenuItem.SendPolicyOsago:
          return CreateSendPolicyOsago();
        case ContextMenuItem.SendPolicyKasko:
          return CreateSendPolicyKasko();
        case ContextMenuItem.Copy:
          return CreateCopy();
        case ContextMenuItem.Print:
          return CreatePrint();
        case ContextMenuItem.PrintWayBill:
          return CreatePrintWayBill();
        case ContextMenuItem.ShowWayBill:
          return CreateShowWayBill();
        case ContextMenuItem.ShowWayBillDaily:
          return CreateShowWayBillDaily();
        //------------------------------
        case ContextMenuItem.ShowInvoice:
          return CreateShowInvoice();
        case ContextMenuItem.ShowAttacheToOrder:
          return CreateShowAttacheToOrder();
        case ContextMenuItem.ShowProxyOnSTO:
          return CreateShowProxyOnSto();
        case ContextMenuItem.PrintProxyOnSTO:
          return CreatePrintProxyOnSto();
        case ContextMenuItem.ShowPolicyKasko:
          return CreateShowPolicyKasko();
        case ContextMenuItem.ShowActFuelCard:
          return CreateShowActFuelCard();
        case ContextMenuItem.ShowNotice:
          return CreateShowNotice();
        case ContextMenuItem.ShowSTS:
          return CreateShowSTS();
        case ContextMenuItem.ShowDriverLicense:
          return CreateShowDriverLicense();
        //----------------------------------
        case ContextMenuItem.Exit:
          return CreateExit();
        case ContextMenuItem.Documents:
          return CreateDocuments();
        case ContextMenuItem.NewCar:
          return CreateNewCar();
        case ContextMenuItem.NewAccount:
          return CreateNewAccount();
        case ContextMenuItem.NewFuelCard:
          return CreateNewFuelCard();
        case ContextMenuItem.ShowPolicyList:
          return CreateShowPolicyList();
        case ContextMenuItem.PrintAllTable:
          return CreatePrintAllTable();
        case ContextMenuItem.ShowAllTable:
          return CreateShowAllTable();
        //---------------------------------
        case ContextMenuItem.Driver:
          return CreateDriver();
        case ContextMenuItem.Region:
          return CreateRegion();
        case ContextMenuItem.SuppyAddress:
          return CreateSuppyAddress();
        case ContextMenuItem.Employee:
          return CreateEmployee();
        case ContextMenuItem.Mark:
          return CreateMark();
        case ContextMenuItem.Model:
          return CreateModel();
        case ContextMenuItem.Grade:
          return CreateGrade();
        case ContextMenuItem.EngineType:
          return CreateEngineType();
        case ContextMenuItem.Color:
          return CreateColor();
        case ContextMenuItem.Dealer:
          return CreateDiler();
        case ContextMenuItem.Owner:
          return CreateOwner();
        case ContextMenuItem.Comp:
          return CreateComp();
        case ContextMenuItem.ServiceStantion:
          return CreateServiceStantion();
        case ContextMenuItem.ServiceStantionComp:
          return CreateServiceStantionComp();
        case ContextMenuItem.Culprit:
          return CreateCulprit();
        case ContextMenuItem.RepairType:
          return CreateRepairType();
        case ContextMenuItem.StatusAfterDTP:
          return CreateStatusAfterDTP();
        case ContextMenuItem.CurrentStatusAfterDTP:
          return CreateCurrentStatusAfterDTP();
        case ContextMenuItem.ViolationType:
          return CreateViolationType();
        case ContextMenuItem.ProxyType:
          return CreateProxyType();
        case ContextMenuItem.FuelCardType:
          return CreateFuelCardType();
        case ContextMenuItem.MailText:
          return CreateMailText();
        case ContextMenuItem.Template:
          return CreateTemplate();
        case ContextMenuItem.UserAccess:
          return CreateUserAccess();
        case ContextMenuItem.Profession:
          return CreateProfession();
        case ContextMenuItem.Sort:
          return CreateSort();
        case ContextMenuItem.Filter:
          return CreateFilter();
        case ContextMenuItem.AddDriver:
          return CreateAddDriver();
        case ContextMenuItem.DeleteDriver:
          return CreateDeleteDriver();
        case ContextMenuItem.MyPointList:
          return CreateMyPointList();
        case ContextMenuItem.RouteList:
          return CreateRouteList();
        case ContextMenuItem.MileageFill:
          return CreateMileageFill();
        case ContextMenuItem.FuelLoad:
          return CreateFuelLoad();
        default:
          throw new NotImplementedException();
      }
    }

    public ToolStripItem CreateItem(Status status)
    {
      switch (status)
      {
        case Status.Account:
          return CreateAccount();
        case Status.AccountViolation:
          return CreateAccountViolation();
        case Status.Actual:
          return CreateActual();
        case Status.Buy:
          return CreateBuy();
        case Status.DiagCard:
          return CreateDiagCard();
        case Status.DTP:
          return CreateDTP();
        case Status.FuelCard:
          return CreateFuelCard();
        case Status.Invoice:
          return CreateInvoice();
        case Status.Policy:
          return CreatePolicy();
        case Status.Repair:
          return CreateRepair();
        case Status.Sale:
          return CreateSale();
        case Status.ShipPart:
          return CreateShipPart();
        case Status.TempMove:
          return CreateTempMove();
        case Status.Violation:
          return CreateViolation();
        default:
          throw new NotImplementedException();
      }
    }

    private ToolStripSeparator CreateSeparator()
    {
      return new ToolStripSeparator();
    }

    private ToolStripMenuItem CreateNewInvoice()
    {
      var item = CreateItem("Новое перемещение");
      item.Click += delegate { InvoiceDialog.CreateNewInvoiceAndOpen(_mainDgv.GetCar()); };
      return item;
    }

    private ToolStripMenuItem CreateNewDtp()
    {
      var item = CreateItem("Новое ДТП");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        var dtp = car.createDTP();

        var dtpAe = new DTP_AddEdit(dtp);
        dtpAe.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateNewViolation()
    {
      ToolStripMenuItem item = CreateItem("Новое нарушение ПДД");
      item.Click += delegate
      {
        Car car = _mainDgv.GetCar();
        if (car == null)
          return;

        var violation = new Violation(car);

        _formViolation.ShowDialog(violation);
      };
      return item;
    }

    private ToolStripMenuItem CreateNewPolicy()
    {
      var item = CreateItem("Новый полис");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        var policyAE = new Policy_AddEdit(car.CreatePolicy());
        policyAE.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateNewDiagCard()
    {
      var item = CreateItem("Новая диагностическая карта");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        var diagCard = new DiagCardModel {CarId = car.Id};

        _diagCardForm.ShowDialog(diagCard);
      };
      return item;
    }

    private ToolStripMenuItem CreateNewMileage()
    {
      var item = CreateItem("Новая запись о пробеге");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        var mileage = new MileageModel(car.Id);

        if (_formMileage.ShowDialog(mileage) == DialogResult.OK)
          _mainStatus.Set(_mainStatus.Get());
      };
      return item;
    }

    private ToolStripMenuItem CreateNewTempMove()
    {
      var item = CreateItem("Новое временное перемещение");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        var tempMove = car.createTempMove();

        var tempMoveAe = new TempMove_AddEdit(tempMove);
        tempMoveAe.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateToSale()
    {
      var item = CreateItem("На продажу");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        if (MessageBox.Show("Вы действительно хотите переместить автомобиль на продажу?", "Снятие с продажи",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            == DialogResult.Yes)
        {
          CarSale carSale = new CarSale(car);
          carSale.Save();

          _mainStatus.Set(_mainStatus.Get());
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateDeleteFromSale()
    {
      ToolStripMenuItem item = CreateItem("Снять с продажи");
      item.Click += delegate
      {
        var car = _mainDgv.GetCar();
        if (car == null)
          return;

        if (MessageBox.Show("Вы действительно хотите убрать автомобиль с продажи?", "Снятие с продажи",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            == DialogResult.Yes)
        {
          CarSaleList carSaleList = CarSaleList.getInstance();
          carSaleList.Delete(car.Id);

          _mainStatus.Set(_mainStatus.Get());
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateLotusMail()
    {
      ToolStripMenuItem item = CreateItem("Создать письмо Outlook");
      item.Click += delegate
      {
        Car car = _mainDgv.GetCar();
        if (car == null)
          return;

        DriverMails driverMails = new DriverMails(_mainDgv);
        string driverList = driverMails.ToString();

        if (string.IsNullOrEmpty(driverList))
          MessageBox.Show("Email-адреса не обнаружены", "Невозможно создать письмо", MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        else
          EMail.OpenEmailProgram(driverList);
      };
      return item;
    }

    private ToolStripMenuItem CreateSendPolicyOsago()
    {
      ToolStripMenuItem item = CreateItem("Отправить полис Осаго");
      item.Click += delegate { SendPolicy(PolicyType.ОСАГО); };
      return item;
    }

    private ToolStripMenuItem CreateSendPolicyKasko()
    {
      ToolStripMenuItem item = CreateItem("Отправить полис Каско");
      item.Click += delegate { SendPolicy(PolicyType.КАСКО); };
      return item;
    }

    private ToolStripMenuItem CreateCopy()
    {
      ToolStripMenuItem item = CreateItem("Копировать");
      item.ShortcutKeys = Keys.Control | Keys.C;
      item.Click += delegate
      {
        try
        {
          MyBuffer.Copy(_mainDgv.Dgv);
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      };
      return item;
    }

    private ToolStripMenuItem CreatePrint()
    {
      var item = CreateItem(Captions.Print);
      item.ShortcutKeys = Keys.Control | Keys.P;
      item.Click += delegate
      {
        var doc = new CreateDocument();
        doc.CreateExcelFromDGV(_mainDgv.Dgv);
        doc.Print();
      };
      return item;
    }

    private ToolStripMenuItem CreatePrintWayBill()
    {
      var item = CreateItem("Печать путевого листа");
      item.Click += delegate
      {
        var inputDate = new InputDate(_mainDgv, Logic.Static.Actions.Print, WayBillType.Month);
        inputDate.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowWayBill()
    {
      var item = CreateItem("Просмотр путевого листа");
      item.Click += delegate
      {
        var inputDate = new InputDate(_mainDgv, Logic.Static.Actions.Show, WayBillType.Month);
        inputDate.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowWayBillDaily()
    {
      var item = CreateItem("Просмотр путевых листов на каждый день");
      item.Click += delegate
      {
        var formWayBillDaily = new FormWayBillDaily(_mainDgv);
        formWayBillDaily.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowInvoice()
    {
      var item = CreateItem("Накладная на перемещение");
      item.Click += delegate
      {
        if (_mainStatus.Get() != Status.Invoice)
        {
          MessageBox.Show("Для формирования накладной необходимо перейти на страницу \"Перемещения\"", Captions.Warning,
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        var doc = CreateDocument(_mainDgv.CurrentCell);

        doc?.ShowInvoice();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowAttacheToOrder()
    {
      var item = CreateItem("Приложение к приказу");
      item.Click += delegate
      {
        if (_mainStatus.Get() != Status.Invoice)
        {
          MessageBox.Show("Для формирования накладной необходимо перейти на страницу \"Перемещения\"", Captions.Warning,
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        var doc = CreateDocument(_mainDgv.CurrentCell);

        doc?.ShowAttacheToOrder();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowProxyOnSto()
    {
      ToolStripMenuItem item = CreateItem("Доверенность на предоставление интересов на СТО");
      item.Click += delegate
      {
        CreateDocument doc = CreateDocument(_mainDgv.CurrentCell);

        doc?.ShowProxyOnSto();
      };
      return item;
    }

    private ToolStripMenuItem CreatePrintProxyOnSto()
    {
      ToolStripMenuItem item = CreateItem("Печать доверенности на предоставление интересов на СТО");
      item.Click += delegate
      {
        foreach (DataGridViewCell cell in _mainDgv.SelectedCells)
        {
          CreateDocument doc = CreateDocument(cell);

          doc?.PrintProxyOnSto();
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateShowPolicyKasko()
    {
      ToolStripMenuItem item = CreateItem("Полис Каско");
      item.Click += delegate
      {
        Car car = _mainDgv.GetCar();
        if (car == null)
          return;

        PolicyList policyList = PolicyList.getInstance();
        Policy kasko = policyList.getItem(car, PolicyType.КАСКО);

        if (!string.IsNullOrEmpty(kasko.File))
          WorkWithFiles.OpenFile(kasko.File);
      };
      return item;
    }

    private ToolStripMenuItem CreateShowActFuelCard()
    {
      ToolStripMenuItem item = CreateItem("Акт передачи топливной карты");
      item.Click += delegate
      {
        Car car = _mainDgv.GetCar();
        if (car == null)
          MessageBox.Show("Для формирования акта выберите ячейку в таблице", Captions.Warning, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        else
        {
          InvoiceList invoiceList = InvoiceList.getInstance();
          Invoice invoice = invoiceList.getItem(_mainDgv.GetId());
          if (invoice == null)
          {
            MessageBox.Show("Для формирования акта необходимо перейти на страницу \"Перемещения\"", Captions.Warning,
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }

          CreateDocument doc = new CreateDocument(car, invoice);
          doc.ShowActFuelCard();
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateShowNotice()
    {
      ToolStripMenuItem item = CreateItem("Извещение о страховом случае");
      item.Click += delegate
      {
        Car car = _mainDgv.GetCar();
        if (car == null)
          return;

        if (_mainStatus.Get() == Status.DTP)
        {
          DTPList dtpList = DTPList.getInstance();
          DTP dtp = dtpList.getItem(_mainDgv.GetId());

          CreateDocument doc = new CreateDocument(car);

          doc.ShowNotice(dtp);
        }
        else
          MessageBox.Show("Для формирования извещения необходимо перейти на вид \"ДТП\"", Captions.Warning,
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
      };
      return item;
    }

    private ToolStripMenuItem CreateShowSTS()
    {
      ToolStripMenuItem item = CreateItem("Свидетельство о регистрации ТС");
      item.Click += delegate
      {
        Car car = _mainDgv.GetCar();
        if (car == null)
          return;

        STSList stsList = STSList.getInstance();
        STS sts = stsList.getItem(car);

        if (!string.IsNullOrEmpty(sts.File))
          WorkWithFiles.OpenFile(sts.File);
      };
      return item;
    }

    private ToolStripMenuItem CreateShowDriverLicense()
    {
      ToolStripMenuItem item = CreateItem("Водительское удостоверение");
      item.Click += delegate
      {
        if (_mainDgv.GetId() == 0)
          return;

        DateTime date = DateTime.Today;

        if (_mainStatus.Get() == Status.DTP)
        {
          DTPList dtpList = DTPList.getInstance();
          DTP dtp = dtpList.getItem(_mainDgv.GetId());
          date = dtp.Date;
        }

        Car car = _mainDgv.GetCar();
        if (car == null)
          return;

        DriverCarList driverCarList = DriverCarList.getInstance();
        Driver driver = driverCarList.GetDriver(car, date);

        LicenseList licencesList = LicenseList.getInstance();
        DriverLicense driverLicense = licencesList.getItem(driver);

        if (!string.IsNullOrEmpty(driverLicense?.File))
          WorkWithFiles.OpenFile(driverLicense.File);
      };
      return item;
    }

    private ToolStripMenuItem CreateExit()
    {
      ToolStripMenuItem item = CreateItem("Выход из программы");
      item.Click += delegate { Application.Exit(); };
      return item;
    }

    private ToolStripMenuItem CreateDocuments()
    {
      ToolStripMenuItem item = CreateItem("Документы");
      item.Click += delegate { Process.Start(DocumentsPath); };
      return item;
    }
    
    private ToolStripMenuItem CreateNewCar()
    {
      var item = CreateItem("Покупка автомобиля");
      item.Click += delegate
      {
        if (_carForm.ShowDialog(new Car()) == DialogResult.OK)
          _mainStatus.Set(_mainStatus.Get());
      };
      return item;
    }
    
    private ToolStripMenuItem CreateNewAccount()
    {
      ToolStripMenuItem item = CreateItem("Добавить счёт");
      item.Click += delegate
      {
        Account_AddEdit aeaAcountForm = new Account_AddEdit(new Account());
        if (aeaAcountForm.ShowDialog() == DialogResult.OK)
          _mainStatus.Set(_mainStatus.Get());
      };
      return item;
    }

    private ToolStripMenuItem CreateNewFuelCard()
    {
      ToolStripMenuItem item = CreateItem("Добавить топливную карту");
      item.Click += delegate
      {
        FuelCard_AddEdit fuelCardAddEdit = new FuelCard_AddEdit(new FuelCard());
        if (fuelCardAddEdit.ShowDialog() == DialogResult.OK)
          _mainStatus.Set(_mainStatus.Get());
      };
      return item;
    }

    private ToolStripMenuItem CreateShowPolicyList()
    {
      ToolStripMenuItem item = CreateItem("Сформировать таблицу страхования");
      item.Click += delegate
      {
        CreateDocument doc = new CreateDocument();
        doc.CreatePolicyTable();
      };
      return item;
    }

    private ToolStripMenuItem CreatePrintAllTable()
    {
      ToolStripMenuItem item = CreateItem("Текущий справочник");
      item.Click += delegate
      {
        MyPrinter myprinter = new MyPrinter();

        string printerName = myprinter.GetDefaultPrinterName();

        if (string.IsNullOrEmpty(printerName))
        {
          MessageBox.Show(Messages.PrinterNotFound, Captions.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        string message = string.Concat("Вывести справочник \"", _mainStatus.ToString(), "\" на печать на принтер ",
          printerName, "?");

        if (MessageBox.Show(message, Captions.Print, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          CreateDocument doc = DgvToExcel();
          doc.Print();
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateShowAllTable()
    {
      ToolStripMenuItem item = CreateItem("Экспорт текущего справочника в Excel");
      item.Click += delegate
      {
        CreateDocument doc = DgvToExcel();
        doc.Show();
      };
      return item;
    }

    private CreateDocument DgvToExcel()
    {
      CreateDocument doc = new CreateDocument();
      doc.CreateExcelFromAllDgv(_mainDgv.Dgv);
      doc.CreateHeader("Справочник \"" + _mainStatus + "\"");

      return doc;
    }

    private ToolStripMenuItem CreateActual()
    {
      ToolStripMenuItem item = CreateItem("На ходу");
      item.Click += delegate { _mainStatus.Set(Status.Actual); };
      return item;
    }

    private ToolStripMenuItem CreateBuy()
    {
      ToolStripMenuItem item = CreateItem("Покупка");
      item.Click += delegate { _mainStatus.Set(Status.Buy); };
      return item;
    }

    private ToolStripMenuItem CreateSale()
    {
      ToolStripMenuItem item = CreateItem("Продажа");
      item.Click += delegate { _mainStatus.Set(Status.Sale); };
      return item;
    }

    private ToolStripMenuItem CreateInvoice()
    {
      ToolStripMenuItem item = CreateItem("Перемещения");
      item.Click += delegate { _mainStatus.Set(Status.Invoice); };
      return item;
    }

    private ToolStripMenuItem CreateTempMove()
    {
      ToolStripMenuItem item = CreateItem("Временные перемещения");
      item.Click += delegate { _mainStatus.Set(Status.TempMove); };
      return item;
    }

    private ToolStripMenuItem CreatePolicy()
    {
      ToolStripMenuItem item = CreateItem("Страховые полисы");
      item.Click += delegate { _mainStatus.Set(Status.Policy); };
      return item;
    }

    private ToolStripMenuItem CreateViolation()
    {
      ToolStripMenuItem item = CreateItem("Нарушения ПДД");
      item.Click += delegate { _mainStatus.Set(Status.Violation); };
      return item;
    }

    private ToolStripMenuItem CreateDTP()
    {
      ToolStripMenuItem item = CreateItem("ДТП");
      item.Click += delegate { _mainStatus.Set(Status.DTP); };
      return item;
    }

    private ToolStripMenuItem CreateDiagCard()
    {
      ToolStripMenuItem item = CreateItem("Диагностические карты");
      item.Click += delegate { _mainStatus.Set(Status.DiagCard); };
      return item;
    }

    private ToolStripMenuItem CreateRepair()
    {
      ToolStripMenuItem item = CreateItem("Сервисное обслуживание");
      item.Click += delegate { _mainStatus.Set(Status.Repair); };
      return item;
    }

    private ToolStripMenuItem CreateShipPart()
    {
      ToolStripMenuItem item = CreateItem("Отправка запчастей");
      item.Click += delegate { _mainStatus.Set(Status.ShipPart); };
      return item;
    }

    private ToolStripMenuItem CreateAccount()
    {
      ToolStripMenuItem item = CreateItem("Страховые полисы");
      item.Click += delegate { _mainStatus.Set(Status.Account); };
      return item;
    }

    private ToolStripMenuItem CreateAccountViolation()
    {
      ToolStripMenuItem item = CreateItem("Штрафы");
      item.Click += delegate { _mainStatus.Set(Status.AccountViolation); };
      return item;
    }

    private ToolStripMenuItem CreateFuelCard()
    {
      ToolStripMenuItem item = CreateItem("Топливные карты");
      item.Click += delegate { _mainStatus.Set(Status.FuelCard); };
      return item;
    }

    private ToolStripMenuItem CreateDriver()
    {
      ToolStripMenuItem item = CreateItem("Водители");
      item.Click += delegate { _mainStatus.Set(Status.Driver); };
      return item;
    }

    private ToolStripMenuItem CreateRegion()
    {
      ToolStripMenuItem item = CreateItem("Регионы");
      item.Click += delegate { loadDictionary("Region", "Справочник \"Регионы\""); };
      return item;
    }

    private ToolStripMenuItem CreateSuppyAddress()
    {
      ToolStripMenuItem item = CreateItem("Адреса подачи");
      item.Click += delegate
      {
        formSuppyAddressList formsuppyAddressList = new formSuppyAddressList();
        formsuppyAddressList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateEmployee()
    {
      ToolStripMenuItem item = CreateItem("Сотрудники в регионе");
      item.Click += delegate
      {
        formEmployeesList formemployeesList = new formEmployeesList();
        formemployeesList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateMark()
    {
      ToolStripMenuItem item = CreateItem("Марки");
      item.Click += delegate { loadDictionary("Mark", "Справочник \"Марки автомобилей\""); };
      return item;
    }

    private ToolStripMenuItem CreateModel()
    {
      ToolStripMenuItem item = CreateItem("Модели");
      item.Click += delegate
      {
        formModelList mList = new formModelList();
        mList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateGrade()
    {
      ToolStripMenuItem item = CreateItem("Комплектации");
      item.Click += delegate
      {
        formGradeList gList = new formGradeList();
        gList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateEngineType()
    {
      ToolStripMenuItem item = CreateItem("Типы двигателей");
      item.Click += delegate { loadDictionary("EngineType", "Справочник \"Типы двигателей\""); };
      return item;
    }

    private ToolStripMenuItem CreateColor()
    {
      ToolStripMenuItem item = CreateItem("Цвета");
      item.Click += delegate { loadDictionary("Color", "Справочник \"Цветов кузова\""); };
      return item;
    }

    private ToolStripMenuItem CreateDiler()
    {
      ToolStripMenuItem item = CreateItem("Дилеры");
      item.Click += delegate
      {
        var container = WindsorConfiguration.Container;
        var dList = new DealerListForm(container.Resolve<IDealerService>());
        dList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateOwner()
    {
      ToolStripMenuItem item = CreateItem("Собственники");
      item.Click += delegate { loadDictionary("Owner", "Справочник \"Собственники\""); };
      return item;
    }

    private ToolStripMenuItem CreateComp()
    {
      ToolStripMenuItem item = CreateItem("Страховые компании");
      item.Click += delegate { loadDictionary("Comp", "Справочник \"Страховые компании\""); };
      return item;
    }

    private ToolStripMenuItem CreateServiceStantion()
    {
      ToolStripMenuItem item = CreateItem("СТО");
      item.Click += delegate
      {
        loadDictionary("ServiceStantion", "Справочник \"Станции технического обслуживания\"");

        ServiceStantions serviceStantions = ServiceStantions.getInstance();
        serviceStantions.ReLoad();
      };
      return item;
    }

    private ToolStripMenuItem CreateServiceStantionComp()
    {
      ToolStripMenuItem item = CreateItem("СТО страховых");
      item.Click += delegate
      {
        formSsDTPList formssDTPList = new formSsDTPList();
        formssDTPList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateCulprit()
    {
      ToolStripMenuItem item = CreateItem("Виновники ДТП");
      item.Click += delegate { loadDictionary("culprit", "Справочник \"Виновники ДТП\""); };
      return item;
    }

    private ToolStripMenuItem CreateRepairType()
    {
      ToolStripMenuItem item = CreateItem("Виды ремонта");
      item.Click += delegate
      {
        loadDictionary("RepairType", "Справочник \"Типы ремонта\"");

        RepairTypes repairTypes = RepairTypes.getInstance();
        repairTypes.ReLoad();
      };
      return item;
    }

    private ToolStripMenuItem CreateStatusAfterDTP()
    {
      ToolStripMenuItem item = CreateItem("Статусы после ДТП");
      item.Click += delegate { loadDictionary("StatusAfterDTP", "Справочник \"Статусы автомобиля после ДТП\""); };
      return item;
    }

    private ToolStripMenuItem CreateCurrentStatusAfterDTP()
    {
      ToolStripMenuItem item = CreateItem("Текущее состояние после ДТП");
      item.Click += delegate
      {
        loadDictionary("CurrentStatusAfterDTP", "Справочник \"Текущее состояние после ДТП\"");

        CurrentStatusAfterDTPs currentStatusAfterDTPs = CurrentStatusAfterDTPs.getInstance();
        currentStatusAfterDTPs.ReLoad();
      };
      return item;
    }

    private ToolStripMenuItem CreateViolationType()
    {
      ToolStripMenuItem item = CreateItem("Типы нарушений ПДД");
      item.Click += delegate
      {
        loadDictionary("ViolationType", "Справочник \"Типы нарушений ПДД\"");

        ViolationTypes violationType = ViolationTypes.getInstance();
        violationType.ReLoad();
      };
      return item;
    }

    private ToolStripMenuItem CreateProxyType()
    {
      ToolStripMenuItem item = CreateItem("Типы доверенностей");
      item.Click += delegate { loadDictionary("proxyType", "Справочник \"Типы доверенностей\""); };
      return item;
    }

    private ToolStripMenuItem CreateFuelCardType()
    {
      ToolStripMenuItem item = CreateItem("Типы топливных карт");
      item.Click += delegate
      {
        loadDictionary("FuelCardType", "Справочник \"Типы топливных карт\"");

        FuelCardTypes fuelCardTypes = FuelCardTypes.getInstance();
        fuelCardTypes.ReLoad();
      };
      return item;
    }

    private ToolStripMenuItem CreateMailText()
    {
      ToolStripMenuItem item = CreateItem("Тексты уведомлений");
      item.Click += delegate
      {
        formMailText fMailText = new formMailText();
        fMailText.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateTemplate()
    {
      var item = CreateItem("Шаблоны документов");
      item.Click += delegate
      {
        _templateListForm.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateUserAccess()
    {
      var item = CreateItem("Доступ пользователей");
      item.Click += delegate
      {
        formUsersAccess fUserAccess = new formUsersAccess();
        fUserAccess.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateProfession()
    {
      var item = CreateItem("Должности пользователей");
      item.Click += delegate
      {
        loadDictionary("EmployeesName", "Справочник \"Профессий\"");

        EmployeesNames employeesNames = EmployeesNames.getInstance();
        employeesNames.ReLoad();
      };
      return item;
    }

    private void loadDictionary(string name, string title)
    {
      formOneStringDictionary oneSd = new formOneStringDictionary(name, title);
      oneSd.ShowDialog();
    }

    private ToolStripMenuItem CreateSort()
    {
      var item = CreateItem("Сортировать");
      item.Click += delegate
      {
        var dgv = _mainDgv.Dgv;

        if (dgv.SelectedCells.Count == 0)
          return;

        int rowIndex = dgv.CurrentCell.RowIndex;
        int columnIndex = dgv.CurrentCell.ColumnIndex;

        DataGridViewColumn column = dgv.Columns[dgv.CurrentCell.ColumnIndex];
        System.ComponentModel.ListSortDirection sortDirection;

        if ((dgv.SortedColumn == null) || (dgv.SortedColumn != column))
          sortDirection = System.ComponentModel.ListSortDirection.Ascending;
        else if (dgv.SortOrder == SortOrder.Ascending)
          sortDirection = System.ComponentModel.ListSortDirection.Descending;
        else
          sortDirection = System.ComponentModel.ListSortDirection.Ascending;

        dgv.Sort(column, sortDirection);

        dgv.CurrentCell = dgv.Rows[rowIndex].Cells[columnIndex];
      };
      return item;
    }

    private ToolStripMenuItem CreateFilter()
    {
      var item = CreateItem("Фильтр по значению этого поля");
      item.Click += delegate
      {
        var dgv = _mainDgv.Dgv;

        if (dgv.CurrentCell == null)
          return;

        string columnName = dgv.Columns[dgv.CurrentCell.ColumnIndex].HeaderText;

        Point point = new Point(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex);

        MyFilter myFilter = (dgv.Name == "_dgvCar") ? MyFilter.GetInstanceCars() : MyFilter.GetInstanceDrivers();
        myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
      };
      return item;
    }

    private ToolStripMenuItem CreateAddDriver()
    {
      ToolStripMenuItem item = CreateItem("Добавить водителя");
      item.Click += delegate
      {
        AddNewDriver addNewDriver = new AddNewDriver();
        if (addNewDriver.ShowDialog() == DialogResult.OK)
          _mainStatus.Set(_mainStatus.Get());
      };
      return item;
    }

    private ToolStripMenuItem CreateDeleteDriver()
    {
      ToolStripMenuItem item = CreateItem("Удалить водителя");
      item.Click += delegate
      {
        if (MessageBox.Show("Вы действительно хотите удалить водителя из списка?", Captions.Delete, MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
        {
          DriverList driverList = DriverList.getInstance();
          Driver driver = driverList.getItem(_mainDgv.GetId());
          DriverCarList driverCarList = DriverCarList.getInstance();

          if (driverCarList.IsDriverHaveCar(driver))
            MessageBox.Show("За водителем закреплён автомобиль, удаление невозможно", Captions.Delete, MessageBoxButtons.OK,
              MessageBoxIcon.Warning);
          else
          {
            driver.IsDriver = false;
            driver.Save();
            _mainStatus.Set(_mainStatus.Get());
          }
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateMyPointList()
    {
      ToolStripMenuItem item = CreateItem("Список пунктов назначения");
      item.Click += delegate
      {
        _myPointListForm.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateRouteList()
    {
      ToolStripMenuItem item = CreateItem("Список маршрутов");
      item.Click += delegate
      {
        _routeListForm.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateMileageFill()
    {
      ToolStripMenuItem item = CreateItem("Загрузить пробеги");
      item.Click += delegate
      {
        FormMileageFill formMileageFill = new FormMileageFill();
        formMileageFill.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateFuelLoad()
    {
      ToolStripMenuItem item = CreateItem("Загрузить данные по заправкам");
      item.Click += delegate
      {
        FormLoadFuel formLoadFuel = new FormLoadFuel();
        formLoadFuel.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateItem(string name)
    {
      return new ToolStripMenuItem(name);
    }

    private void SendPolicy(PolicyType type)
    {
      var car = _mainDgv.GetCar();
      if (car == null)
        return;

      var result = MailPolicy.Send(car, type);

      MessageBox.Show(result, Captions.Send, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private CreateDocument CreateDocument(DataGridViewCell cell)
    {
      var carId = _mainDgv.GetCarId(cell.RowIndex);

      if (carId == 0)
        return null;

      var carList = CarList.getInstance();
      var car = carList.getItem(carId);

      Invoice invoice = null;

      if (_mainStatus.Get() == Status.Invoice)
      {
        var invoiceId = _mainDgv.GetId(cell.RowIndex);

        var invoiceList = InvoiceList.getInstance();
        invoice = invoiceList.getItem(invoiceId);
      }

      return new CreateDocument(car, invoice);
    }
  }
}
