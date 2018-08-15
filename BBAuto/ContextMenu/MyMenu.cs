using System.Windows.Forms;
using BBAuto.Domain.Static;

namespace BBAuto.ContextMenu
{
  public class MyMenu
  {
    private readonly MainStatus _mainStatus;
    private readonly MyMenuItemFactory _factory;

    public MyMenu(MainDGV dgvMain)
    {
      _mainStatus = MainStatus.getInstance();
      _factory = new MyMenuItemFactory(dgvMain);
    }

    public MenuStrip CreateMainMenu()
    {
      switch (User.GetRole())
      {
        case RolesList.Adminstrator:
        case RolesList.Boss:
        case RolesList.proxyBoss:
        case RolesList.Editor:
          return CreateMainMenuAdministrator();
        case RolesList.AccountantWayBill:
          return CreateMainMenuAccountantWayBill();
        case RolesList.Viewer:
        case RolesList.AccountantBBraun:
        case RolesList.AccountantGematek:
          return CreateMainMenuViewer();
        default:
          return null;
      }
    }

    private MenuStrip CreateMainMenuAdministrator()
    {
      MenuStrip menuStrip = new MenuStrip();

      ToolStripMenuItem itemAction = new ToolStripMenuItem("Действия");
      ToolStripMenuItem itemMainDictionary = new ToolStripMenuItem("Основные справочники");
      ToolStripMenuItem itemExtraDictionary = new ToolStripMenuItem("Дополнительные справочники");

      ToolStripMenuItem itemCreate = new ToolStripMenuItem("Создать");
      ToolStripMenuItem itemShow = new ToolStripMenuItem("Показать");
      ToolStripMenuItem itemChangeStatus = new ToolStripMenuItem("Изменить статус");
      ToolStripMenuItem itemDriverMail = new ToolStripMenuItem("Письмо водителю");
      ToolStripMenuItem itemPrint = new ToolStripMenuItem("Печать");

      itemAction.DropDownItems.Add(itemCreate);
      itemAction.DropDownItems.Add(itemShow);
      itemAction.DropDownItems.Add(itemChangeStatus);
      itemAction.DropDownItems.Add(itemDriverMail);
      itemAction.DropDownItems.Add(itemPrint);
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Copy));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Exit));

      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewCar));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.AddDriver));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewAccount));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewFuelCard));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewTransponder));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewInvoice));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowActFuelCard));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowInvoice));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowProxyOnSTO));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowAttacheToOrder));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewTempMove));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewDiagCard));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewDTP));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewMileage));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowNotice));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewViolation));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.NewPolicy));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowPolicyList));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.MileageFill));
      itemCreate.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.FuelLoad));

      ToolStripItem itemShowDriverLicense = _factory.CreateItem(ContextMenuItem.ShowDriverLicense);
      ToolStripItem itemShowWayBill = _factory.CreateItem(ContextMenuItem.ShowWayBill);
      ToolStripItem itemShowSTS = _factory.CreateItem(ContextMenuItem.ShowSTS);
      itemShowDriverLicense.Enabled = _mainStatus.Get() != Status.Account && _mainStatus.Get() != Status.FuelCard;
      itemShowWayBill.Enabled = itemShowDriverLicense.Enabled;
      itemShowSTS.Enabled = itemShowDriverLicense.Enabled;
      itemShow.DropDownItems.Add(itemShowDriverLicense);
      itemShow.DropDownItems.Add(itemShowWayBill);
      itemShow.DropDownItems.Add(itemShowSTS);

      ToolStripItem itemToSale = _factory.CreateItem(ContextMenuItem.ToSale);
      ToolStripItem itemDeleteFromSale = _factory.CreateItem(ContextMenuItem.DeleteFromSale);
      itemToSale.Enabled = (_mainStatus.Get() == Status.Actual);
      itemDeleteFromSale.Enabled = (_mainStatus.Get() == Status.Sale);
      itemChangeStatus.DropDownItems.Add(itemToSale);
      itemChangeStatus.DropDownItems.Add(itemDeleteFromSale);

      ToolStripItem itemLotusMail = _factory.CreateItem(ContextMenuItem.LotusMail);
      ToolStripItem itemSendPolicyKasko = _factory.CreateItem(ContextMenuItem.SendPolicyKasko);
      ToolStripItem itemSendPolicyOsago = _factory.CreateItem(ContextMenuItem.SendPolicyOsago);
      itemLotusMail.Enabled = _mainStatus.Get() != Status.Account;
      itemSendPolicyKasko.Enabled = itemLotusMail.Enabled;
      itemSendPolicyOsago.Enabled = itemLotusMail.Enabled;
      itemDriverMail.DropDownItems.Add(itemLotusMail);
      itemDriverMail.DropDownItems.Add(itemSendPolicyKasko);
      itemDriverMail.DropDownItems.Add(itemSendPolicyOsago);
      itemDriverMail.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.SelectRecipient));

      itemPrint.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Print));
      itemPrint.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.PrintWayBill));
      itemPrint.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.PrintAllTable));
      itemPrint.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ShowAllTable));
      itemPrint.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.PrintProxyOnSTO));

      ToolStripMenuItem itemCar = new ToolStripMenuItem("Автомобили");
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Actual));
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Buy));
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Sale));

      itemMainDictionary.DropDownItems.Add(itemCar);
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Driver));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Invoice));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.TempMove));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Policy));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Violation));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.DTP));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.DiagCard));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Repair));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.ShipPart));

      ToolStripMenuItem itemAccount = new ToolStripMenuItem("Согласования");
      itemAccount.DropDownItems.Add(_factory.CreateItem(Status.Account));
      itemAccount.DropDownItems.Add(_factory.CreateItem(Status.AccountViolation));
      itemMainDictionary.DropDownItems.Add(itemAccount);

      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.FuelCard));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Transponder));

      ToolStripMenuItem itemDriverAndEmployee = new ToolStripMenuItem("Сотрудники, регионы");
      ToolStripMenuItem itemMarkModel = new ToolStripMenuItem("Марки, модели, комплектации");
      ToolStripMenuItem itemCompanies = new ToolStripMenuItem("Компании, покупатели");
      ToolStripMenuItem itemStatusesTypes = new ToolStripMenuItem("Статусы, типы, виды");
      ToolStripMenuItem itemUsers = new ToolStripMenuItem("Пользователи программы");
      ToolStripMenuItem itemDocuments = new ToolStripMenuItem("Документы");

      itemDriverAndEmployee.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Region));
      itemDriverAndEmployee.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.SuppyAddress));
      itemDriverAndEmployee.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Employee));
      itemDriverAndEmployee.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.MyPointList));
      itemDriverAndEmployee.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.RouteList));

      itemMarkModel.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Mark));
      itemMarkModel.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Model));
      itemMarkModel.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Grade));
      itemMarkModel.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.EngineType));
      itemMarkModel.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Color));

      itemCompanies.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Dealer));
      itemCompanies.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Owner));
      itemCompanies.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Comp));
      itemCompanies.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ServiceStantion));
      itemCompanies.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ServiceStantionComp));
      itemCompanies.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.CustomerList));

      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Culprit));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.RepairType));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.StatusAfterDTP));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.CurrentStatusAfterDTP));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ViolationType));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.ProxyType));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.FuelCardType));
      itemStatusesTypes.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.MailText));
      
      itemUsers.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.UserAccess));
      itemUsers.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Profession));

      itemDocuments.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.DocumentsForSend));
      itemDocuments.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Template));

      itemExtraDictionary.DropDownItems.Add(itemDriverAndEmployee);
      itemExtraDictionary.DropDownItems.Add(itemMarkModel);
      itemExtraDictionary.DropDownItems.Add(itemCompanies);
      itemExtraDictionary.DropDownItems.Add(itemStatusesTypes);
      itemExtraDictionary.DropDownItems.Add(itemUsers);
      itemExtraDictionary.DropDownItems.Add(itemDocuments);

      menuStrip.Items.Add(itemAction);
      menuStrip.Items.Add(itemMainDictionary);
      menuStrip.Items.Add(itemExtraDictionary);
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Documents));

      return menuStrip;
    }

    private MenuStrip CreateMainMenuAccountantWayBill()
    {
      MenuStrip menuStrip = new MenuStrip();

      ToolStripMenuItem itemAction = new ToolStripMenuItem("Действия");
      ToolStripMenuItem itemMainDictionary = new ToolStripMenuItem("Основные справочники");
      ToolStripMenuItem itemExtraDictionary = new ToolStripMenuItem("Дополнительные справочники");

      ToolStripItem itemShowWayBill = _factory.CreateItem(ContextMenuItem.ShowWayBill);
      ToolStripItem itemPrintWayBill = _factory.CreateItem(ContextMenuItem.PrintWayBill);
      itemShowWayBill.Enabled = ((_mainStatus.Get() != Status.Account) && (_mainStatus.Get() != Status.FuelCard));
      itemPrintWayBill.Enabled = itemShowWayBill.Enabled;

      itemAction.DropDownItems.Add(itemShowWayBill);
      itemAction.DropDownItems.Add(itemPrintWayBill);
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Copy));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Exit));

      ToolStripMenuItem itemCar = new ToolStripMenuItem("Автомобили");
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Actual));
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Buy));
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Sale));

      itemMainDictionary.DropDownItems.Add(itemCar);
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Driver));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Invoice));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.TempMove));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Policy));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Violation));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.DTP));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.DiagCard));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Repair));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.ShipPart));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Account));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.FuelCard));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Transponder));

      itemExtraDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.SuppyAddress));
      itemExtraDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Employee));
      itemExtraDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.MyPointList));
      itemExtraDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.RouteList));

      menuStrip.Items.Add(itemAction);
      menuStrip.Items.Add(itemMainDictionary);
      menuStrip.Items.Add(itemExtraDictionary);

      return menuStrip;
    }

    private MenuStrip CreateMainMenuViewer()
    {
      MenuStrip menuStrip = new MenuStrip();

      ToolStripMenuItem itemAction = new ToolStripMenuItem("Действия");
      ToolStripMenuItem itemMainDictionary = new ToolStripMenuItem("Основные справочники");
      ToolStripMenuItem itemExtraDictionary = new ToolStripMenuItem("Дополнительные справочники");

      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Copy));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Separator));
      itemAction.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Exit));

      ToolStripMenuItem itemCar = new ToolStripMenuItem("Автомобили");
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Actual));
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Buy));
      itemCar.DropDownItems.Add(_factory.CreateItem(Status.Sale));

      itemMainDictionary.DropDownItems.Add(itemCar);
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Invoice));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.TempMove));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Policy));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Violation));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.DTP));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.DiagCard));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Repair));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.ShipPart));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Account));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.FuelCard));
      itemMainDictionary.DropDownItems.Add(_factory.CreateItem(Status.Transponder));

      itemExtraDictionary.DropDownItems.Add(_factory.CreateItem(ContextMenuItem.Driver));

      menuStrip.Items.Add(itemAction);
      menuStrip.Items.Add(itemMainDictionary);
      menuStrip.Items.Add(itemExtraDictionary);

      return menuStrip;
    }

    public ContextMenuStrip CreateContextMenu()
    {
      if (User.IsFullAccess())
      {
        switch (_mainStatus.Get())
        {
          case Status.Actual:
            return CreateContextMenuCar();
          case Status.Invoice:
            return CreateContextMenuInvoice();
          case Status.DTP:
            return CreateContextMenuDTP();
          case Status.Sale:
            return CreateContextMenuSale();
          case Status.Policy:
            return CreateContextMenuPolicy();
          case Status.TempMove:
          case Status.Violation:
          case Status.DiagCard:
          case Status.ShipPart:
          case Status.Account:
          case Status.FuelCard:
          case Status.Transponder:
            return CreateContextMenuSortAndFilter();
          case Status.Driver:
            return CreateContextMenuDriver();
          default:
            return null;
        }
      }
      if (User.GetRole() == RolesList.AccountantWayBill &&
          (_mainStatus.Get() == Status.Actual || _mainStatus.Get() == Status.Sale))
        return CreateContextMenuWayBill();

      return null;
    }

    private ContextMenuStrip CreateContextMenuCar()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewInvoice));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewDTP));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewViolation));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewPolicy));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewDiagCard));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewMileage));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.NewTempMove));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ToSale));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.LotusMail));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.SendPolicyOsago));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.SendPolicyKasko));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.SelectDocumentsForSend));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Copy));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Print));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.PrintWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBillDaily));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowActFuelCard));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuInvoice()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowInvoice));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowAttacheToOrder));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowProxyOnSTO));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowPolicyKasko));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.PrintWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowActFuelCard));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Copy));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuDTP()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowNotice));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowPolicyKasko));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowSTS));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowDriverLicense));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.PrintWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Copy));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuSale()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.DeleteFromSale));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.PrintWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBillDaily));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowContractOfSale));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowTransferCarAct));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuWayBill()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.PrintWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBill));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.ShowWayBillDaily));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuSortAndFilter()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuPolicy()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.SendPolicyKasko));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.SendPolicyOsago));

      return menuStrip;
    }

    private ContextMenuStrip CreateContextMenuDriver()
    {
      ContextMenuStrip menuStrip = new ContextMenuStrip();

      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Filter));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Sort));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.AddDriver));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.DeleteDriver));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.Separator));
      menuStrip.Items.Add(_factory.CreateItem(ContextMenuItem.SelectDocumentsForSend));

      return menuStrip;
    }
  }
}
