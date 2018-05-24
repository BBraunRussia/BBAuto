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
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Dictionary.Color;
using BBAuto.Logic.Services.Dictionary.Comp;
using BBAuto.Logic.Services.Dictionary.Culprit;
using BBAuto.Logic.Services.Dictionary.CurrentStatusAfterDtp;
using BBAuto.Logic.Services.Dictionary.EmployeesName;
using BBAuto.Logic.Services.Dictionary.EngineType;
using BBAuto.Logic.Services.Dictionary.FuelCardType;
using BBAuto.Logic.Services.Dictionary.Mark;
using BBAuto.Logic.Services.Dictionary.Owner;
using BBAuto.Logic.Services.Dictionary.ProxyType;
using BBAuto.Logic.Services.Dictionary.Region;
using BBAuto.Logic.Services.Dictionary.RepairType;
using BBAuto.Logic.Services.Dictionary.ServiceStantion;
using BBAuto.Logic.Services.Dictionary.StatusAfterDtp;
using BBAuto.Logic.Services.Dictionary.ViolationType;
using BBAuto.Logic.Services.Documents;
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
    private readonly IGradeListForm _gradeListForm;
    private readonly IModelListForm _modelListForm;
    private readonly ISsDtpListForm _ssDtpListForm;
    private readonly IOneStringDictionaryListForm _oneStringDictionaryListForm;

    private readonly IDocumentsService _documentsService;
    private readonly ISaleCarService _saleCarService;
    private readonly IEmployeesNameService _employeesNameService;
    private readonly IRegionService _regionService;
    private readonly IMarkService _markService;
    private readonly IEngineTypeService _engineTypeService;
    private readonly IColorService _colorService;
    private readonly IOwnerService _ownerService;
    private readonly ICompService _compService;
    private readonly IServiceStantionService _serviceStantionService;
    private readonly ICulpritService _culpritService;
    private readonly IRepairTypeService _repairTypeService;
    private readonly IStatusAfterDtpService _statusAfterDtpService;
    private readonly ICurrentStatusAfterDtpService _currentStatusAfterDtpService;
    private readonly IViolationTypeService _violationTypeService;
    private readonly IProxyTypeService _proxyTypeService;
    private readonly IFuelCardTypeService _fuelCardTypeService;

    private IMainDgv _mainDgv;

    public MyMenuItemFactory(
      IMileageForm formMileage,
      ICarForm carForm,
      IViolationForm formViolation,
      IDiagCardForm diagCardForm,
      IMyPointListForm myPointListForm,
      IRouteListForm routeListForm,
      ITemplateListForm templateListForm,
      IDocumentsService documentsService,
      ISaleCarService saleCarService,
      IGradeListForm gradeListForm,
      IModelListForm modelListForm,
      ISsDtpListForm ssDtpListForm,
      IOneStringDictionaryListForm oneStringDictionaryListForm,
      IEmployeesNameService employeesNameService,
      IRegionService regionService,
      IMarkService markService,
      IEngineTypeService engineTypeService,
      IColorService colorService,
      IOwnerService ownerService,
      ICompService compService,
      IServiceStantionService serviceStantionService,
      ICulpritService culpritService,
      IRepairTypeService repairTypeService,
      IStatusAfterDtpService statusAfterDtpService,
      ICurrentStatusAfterDtpService currentStatusAfterDtpService,
      IViolationTypeService violationTypeService,
      IProxyTypeService proxyTypeService,
      IFuelCardTypeService fuelCardTypeService)
    {
      _formMileage = formMileage;
      _carForm = carForm;
      _formViolation = formViolation;
      _diagCardForm = diagCardForm;
      _myPointListForm = myPointListForm;
      _routeListForm = routeListForm;
      _templateListForm = templateListForm;
      _documentsService = documentsService;
      _saleCarService = saleCarService;
      _gradeListForm = gradeListForm;
      _modelListForm = modelListForm;
      _ssDtpListForm = ssDtpListForm;
      _oneStringDictionaryListForm = oneStringDictionaryListForm;
      _employeesNameService = employeesNameService;
      _regionService = regionService;
      _markService = markService;
      _engineTypeService = engineTypeService;
      _colorService = colorService;
      _ownerService = ownerService;
      _compService = compService;
      _serviceStantionService = serviceStantionService;
      _culpritService = culpritService;
      _repairTypeService = repairTypeService;
      _statusAfterDtpService = statusAfterDtpService;
      _currentStatusAfterDtpService = currentStatusAfterDtpService;
      _violationTypeService = violationTypeService;
      _proxyTypeService = proxyTypeService;
      _fuelCardTypeService = fuelCardTypeService;
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
          return CreateShowSts();
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
          return CreateStatusAfterDtp();
        case ContextMenuItem.CurrentStatusAfterDTP:
          return CreateCurrentStatusAfterDtp();
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
      item.Click += delegate { InvoiceDialog.CreateNewInvoiceAndOpen(_mainDgv.GetCarId()); };
      return item;
    }

    private ToolStripMenuItem CreateNewDtp()
    {
      var item = CreateItem("Новое ДТП");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        var dtp = new DTP(CarList.getInstance().getItem(carId));

        var dtpAe = new DTP_AddEdit(dtp);
        dtpAe.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateNewViolation()
    {
      var item = CreateItem("Новое нарушение ПДД");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        _formViolation.ShowDialog(0, carId, _carForm);
      };
      return item;
    }

    private ToolStripMenuItem CreateNewPolicy()
    {
      var item = CreateItem("Новый полис");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        var policyAe = new Policy_AddEdit(new Policy(carId));
        policyAe.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateNewDiagCard()
    {
      var item = CreateItem("Новая диагностическая карта");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        var diagCard = new DiagCardModel {CarId = carId};

        _diagCardForm.ShowDialog(diagCard);
      };
      return item;
    }

    private ToolStripMenuItem CreateNewMileage()
    {
      var item = CreateItem("Новая запись о пробеге");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        var mileage = new MileageModel(carId);

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
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        var tempMove = new TempMove(carId);

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
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        if (MessageBox.Show(Messages.MoveCarToSale, Captions.RemoveFromSale, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          _saleCarService.Save(new SaleCarModel { Id = carId });
          
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
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        if (MessageBox.Show(Messages.RemoveCarFromSale, Captions.RemoveFromSale, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          _saleCarService.Delete(carId);
          
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
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        DriverMails driverMails = new DriverMails(_mainDgv);
        string driverList = driverMails.ToString();

        if (string.IsNullOrEmpty(driverList))
          MessageBox.Show(Messages.NotFoundEmails, Captions.CannotCreateMail, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        else
          EMail.OpenEmailProgram(driverList);
      };
      return item;
    }

    private ToolStripMenuItem CreateSendPolicyOsago()
    {
      var item = CreateItem("Отправить полис Осаго");
      item.Click += delegate { SendPolicy(PolicyType.ОСАГО); };
      return item;
    }

    private ToolStripMenuItem CreateSendPolicyKasko()
    {
      var item = CreateItem("Отправить полис Каско");
      item.Click += delegate { SendPolicy(PolicyType.КАСКО); };
      return item;
    }

    private ToolStripMenuItem CreateCopy()
    {
      var item = CreateItem("Копировать");
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
        var document = _documentsService.CreateExcelFromDgv(_mainDgv.Dgv);
        document.Print();
      };
      return item;
    }

    private ToolStripMenuItem CreatePrintWayBill()
    {
      var item = CreateItem("Печать путевого листа");
      item.Click += delegate
      {
        var inputDate = new InputDate(_mainDgv, Logic.Static.Actions.Print, WayBillType.Month, _documentsService);
        inputDate.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowWayBill()
    {
      var item = CreateItem("Просмотр путевого листа");
      item.Click += delegate
      {
        var inputDate = new InputDate(_mainDgv, Logic.Static.Actions.Show, WayBillType.Month, _documentsService);
        inputDate.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowWayBillDaily()
    {
      var item = CreateItem("Просмотр путевых листов на каждый день");
      item.Click += delegate
      {
        var formWayBillDaily = new FormWayBillDaily(_mainDgv, _documentsService);
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

        var carId = _mainDgv.GetCarId(_mainDgv.CurrentCell.RowIndex);
        var invoiceId = 0;
        if (_mainStatus.Get() == Status.Invoice)
          invoiceId = _mainDgv.GetId(_mainDgv.CurrentCell.RowIndex);

        var document = _documentsService.CreateDocumentInvoice(carId, invoiceId);
        document.Show();
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

        var carId = _mainDgv.GetCarId(_mainDgv.CurrentCell.RowIndex);
        var invoiceId = 0;
        if (_mainStatus.Get() == Status.Invoice)
          invoiceId = _mainDgv.GetId(_mainDgv.CurrentCell.RowIndex);

        var document = _documentsService.CreateAttacheToOrder(carId, invoiceId);
        document?.Show();
      };
      return item;
    }

    private ToolStripMenuItem CreateShowProxyOnSto()
    {
      ToolStripMenuItem item = CreateItem("Доверенность на предоставление интересов на СТО");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId(_mainDgv.CurrentCell.RowIndex);
        var invoiceId = 0;
        if (_mainStatus.Get() == Status.Invoice)
          invoiceId = _mainDgv.GetId(_mainDgv.CurrentCell.RowIndex);

        var document = _documentsService.CreateProxyOnSto(carId, invoiceId);
        document.Show();
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
          var carId = _mainDgv.GetCarId(cell.RowIndex);
          var invoiceId = 0;
          if (_mainStatus.Get() == Status.Invoice)
            invoiceId = _mainDgv.GetId(cell.RowIndex);

          _documentsService.PrintProxyOnSto(carId, invoiceId);
          //TODO create print with selected date
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateShowPolicyKasko()
    {
      ToolStripMenuItem item = CreateItem("Полис Каско");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        PolicyList policyList = PolicyList.getInstance();
        Policy kasko = policyList.getItem(carId, PolicyType.КАСКО);

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
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          MessageBox.Show("Для формирования акта выберите ячейку в таблице", Captions.Warning, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        else
        {
          var invoiceId = _mainDgv.GetId();
          
          var document = _documentsService.CreateActFuelCard(invoiceId);
          document.Show();
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateShowNotice()
    {
      var item = CreateItem("Извещение о страховом случае");
      item.Click += delegate
      {
        if (_mainStatus.Get() == Status.DTP)
        {
          var dtpList = DTPList.getInstance();
          var dtp = dtpList.getItem(_mainDgv.GetId());

          var carId = _mainDgv.GetCarId();

          var documnet = _documentsService.CreateNotice(carId, dtp);
          documnet.Show();
        }
        else
          MessageBox.Show(Messages.ForCreateNoticeGotoViewDtp, Captions.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      };
      return item;
    }

    private ToolStripMenuItem CreateShowSts()
    {
      ToolStripMenuItem item = CreateItem("Свидетельство о регистрации ТС");
      item.Click += delegate
      {
        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        var stsList = STSList.getInstance();
        var sts = stsList.getItem(carId);

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

        var carId = _mainDgv.GetCarId();
        if (carId == 0)
          return;

        DriverCarList driverCarList = DriverCarList.getInstance();
        Driver driver = driverCarList.GetDriver(carId, date);

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
        if (_carForm.ShowDialog(0) == DialogResult.OK)
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
        var document = _documentsService.CreatePolicyTable();
        document.Show();
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
          var document = _documentsService.CreateExcelFromAllDgv(_mainDgv.Dgv);
          document.CreateHeader("Справочник \"" + _mainStatus + "\"");
          document.Print();
        }
      };
      return item;
    }

    private ToolStripMenuItem CreateShowAllTable()
    {
      ToolStripMenuItem item = CreateItem("Экспорт текущего справочника в Excel");
      item.Click += delegate
      {
        var document = _documentsService.CreateExcelFromAllDgv(_mainDgv.Dgv);
        document.CreateHeader("Справочник \"" + _mainStatus + "\"");
        document.Show();
      };
      return item;
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
      var item = CreateItem("Штрафы");
      item.Click += delegate { _mainStatus.Set(Status.AccountViolation); };
      return item;
    }

    private ToolStripMenuItem CreateFuelCard()
    {
      var item = CreateItem("Топливные карты");
      item.Click += delegate { _mainStatus.Set(Status.FuelCard); };
      return item;
    }

    private ToolStripMenuItem CreateDriver()
    {
      var item = CreateItem("Водители");
      item.Click += delegate { _mainStatus.Set(Status.Driver); };
      return item;
    }

    private ToolStripMenuItem CreateRegion()
    {
      var item = CreateItem("Регионы");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Регионы""", _regionService); };
      return item;
    }

    private ToolStripMenuItem CreateSuppyAddress()
    {
      var item = CreateItem("Адреса подачи");
      item.Click += delegate
      {
        formSuppyAddressList formsuppyAddressList = new formSuppyAddressList();
        formsuppyAddressList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateEmployee()
    {
      var item = CreateItem("Сотрудники в регионе");
      item.Click += delegate
      {
        formEmployeesList formemployeesList = new formEmployeesList();
        formemployeesList.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateMark()
    {
      var item = CreateItem("Марки");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Марки автомобилей""", _markService); };
      return item;
    }

    private ToolStripMenuItem CreateModel()
    {
      var item = CreateItem("Модели");
      item.Click += delegate
      {
        _modelListForm.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateGrade()
    {
      var item = CreateItem("Комплектации");
      item.Click += delegate
      {
        _gradeListForm.ShowDialog();
      };
      return item;
    }

    private ToolStripMenuItem CreateEngineType()
    {
      var item = CreateItem("Типы двигателей");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Типы двигателей""", _engineTypeService); };
      return item;
    }

    private ToolStripMenuItem CreateColor()
    {
      var item = CreateItem("Цвета");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Цветов кузова""", _colorService); };
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
      var item = CreateItem("Собственники");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Собственники""", _ownerService); };
      return item;
    }

    private ToolStripMenuItem CreateComp()
    {
      var item = CreateItem("Страховые компании");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Страховые компании""", _compService); };
      return item;
    }

    private ToolStripMenuItem CreateServiceStantion()
    {
      var item = CreateItem("СТО");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Станции технического обслуживания""", _serviceStantionService); };
      return item;
    }

    private ToolStripMenuItem CreateServiceStantionComp()
    {
      var item = CreateItem("СТО страховых");
      item.Click += delegate { _ssDtpListForm.ShowDialog(); };
      return item;
    }

    private ToolStripMenuItem CreateCulprit()
    {
      var item = CreateItem("Виновники ДТП");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Виновники ДТП""", _culpritService); };
      return item;
    }

    private ToolStripMenuItem CreateRepairType()
    {
      var item = CreateItem("Виды ремонта");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Типы ремонта""", _repairTypeService); };
      return item;
    }

    private ToolStripMenuItem CreateStatusAfterDtp()
    {
      var item = CreateItem("Статусы после ДТП");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Статусы автомобиля после ДТП""", _statusAfterDtpService); };
      return item;
    }

    private ToolStripMenuItem CreateCurrentStatusAfterDtp()
    {
      var item = CreateItem("Текущее состояние после ДТП");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Текущее состояние после ДТП""", _currentStatusAfterDtpService); };
      return item;
    }

    private ToolStripMenuItem CreateViolationType()
    {
      var item = CreateItem("Типы нарушений ПДД");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Типы нарушений ПДД""", _violationTypeService); };
      return item;
    }

    private ToolStripMenuItem CreateProxyType()
    {
      var item = CreateItem("Типы доверенностей");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Типы доверенностей""", _proxyTypeService); };
      return item;
    }

    private ToolStripMenuItem CreateFuelCardType()
    {
      var item = CreateItem("Типы топливных карт");
      item.Click += delegate { _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Типы топливных карт""", _fuelCardTypeService); };
      return item;
    }

    private ToolStripMenuItem CreateMailText()
    {
      var item = CreateItem("Тексты уведомлений");
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
        _oneStringDictionaryListForm.ShowDialog(@"Справочник ""Профессий""", _employeesNameService);
        
        EmployeesNames employeesNames = EmployeesNames.getInstance();
        employeesNames.ReLoad();
      };
      return item;
    }
    
    private ToolStripMenuItem CreateSort()
    {
      var item = CreateItem("Сортировать");
      item.Click += delegate
      {
        var dgv = _mainDgv.Dgv;

        if (dgv.SelectedCells.Count == 0)
          return;

        var rowIndex = dgv.CurrentCell.RowIndex;
        var columnIndex = dgv.CurrentCell.ColumnIndex;

        var column = dgv.Columns[dgv.CurrentCell.ColumnIndex];
        System.ComponentModel.ListSortDirection sortDirection;

        if (dgv.SortedColumn == null || dgv.SortedColumn != column)
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

        var columnName = dgv.Columns[dgv.CurrentCell.ColumnIndex].HeaderText;

        Point point = new Point(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex);

        var myFilter = dgv.Name == "_dgvCar"
          ? MyFilter.GetInstanceCars()
          : MyFilter.GetInstanceDrivers();
        myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
      };
      return item;
    }

    private ToolStripMenuItem CreateAddDriver()
    {
      var item = CreateItem("Добавить водителя");
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
      var item = CreateItem("Удалить водителя");
      item.Click += delegate
      {
        if (MessageBox.Show(Messages.DeleteDriver, Captions.Delete, MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes)
        {
          DriverList driverList = DriverList.getInstance();
          Driver driver = driverList.getItem(_mainDgv.GetId());
          DriverCarList driverCarList = DriverCarList.getInstance();

          if (driverCarList.IsDriverHaveCar(driver))
            MessageBox.Show(Messages.CannotDeleteDriver, Captions.Delete, MessageBoxButtons.OK,
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
      var item = CreateItem("Список пунктов назначения");
      item.Click += delegate { _myPointListForm.ShowDialog(); };
      return item;
    }

    private ToolStripMenuItem CreateRouteList()
    {
      var item = CreateItem("Список маршрутов");
      item.Click += delegate { _routeListForm.ShowDialog(); };
      return item;
    }

    private ToolStripMenuItem CreateMileageFill()
    {
      var item = CreateItem("Загрузить пробеги");
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
      var carId = _mainDgv.GetCarId();
      if (carId == 0)
        return;

      var car = CarList.getInstance().getItem(carId);

      var result = MailPolicy.Send(car, type);

      MessageBox.Show(result, Captions.Send, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
