namespace BBAuto.Logic.Static
{
  public enum Status
  {
    All = 0,
    Buy = 1,
    Actual = 2,
    Repair = 3,
    Sale = 4,
    Invoice = 5,
    Policy = 6,
    DTP = 7,
    Violation = 8,
    DiagCard = 9,
    TempMove = 10,
    ShipPart = 11,
    Account = 12,
    FuelCard = 13,
    Driver = 14,
    AccountViolation = 15
  };

  public enum RolesList
  {
    Employee = 0,
    Adminstrator = 1,
    Editor = 2,
    Boss = 3,
    Viewer = 4,
    AccountantWayBill = 5,
    AccountantBBraun = 6,
    AccountantGematek = 7,
    proxyBoss = 8
  }

  public enum NameType
  {
    Full,
    Short,
    Genetive
  }

  public enum PolicyType
  {
    ОСАГО = 1,
    КАСКО = 2,
    ДСАГО = 3,
    GAP = 4,
    расш_КАСКО = 5
  }

  public enum MailTextType
  {
    License = 1,
    MedicalCert = 2,
    Policy = 3,
    DiagCard = 4
  }

  public enum Actions
  {
    Show,
    Print
  };

  public enum WayBillType
  {
    Day,
    Month
  };

  public enum ContextMenuItem
  {
    Separator,
    NewInvoice,
    NewDTP,
    NewViolation,
    NewPolicy,
    NewDiagCard,
    NewMileage,
    NewTempMove,
    ToSale,
    DeleteFromSale,
    LotusMail,
    SendPolicyOsago,
    SendPolicyKasko,
    Copy,
    Print,
    PrintWayBill,
    ShowWayBill,
    ShowWayBillDaily,

    ShowInvoice,
    ShowAttacheToOrder,
    ShowProxyOnSTO,
    PrintProxyOnSTO,
    ShowPolicyKasko,
    ShowActFuelCard,

    ShowNotice,
    ShowSTS,
    ShowDriverLicense,

    Exit,
    Documents,
    NewCar,
    NewAccount,
    NewFuelCard,
    ShowPolicyList,
    PrintAllTable,
    ShowAllTable,

    Driver,
    Region,
    SuppyAddress,
    Employee,

    Mark,
    Model,
    Grade,
    EngineType,
    Color,

    Dealer,
    Owner,
    Comp,
    ServiceStantion,
    ServiceStantionComp,

    Culprit,
    RepairType,
    StatusAfterDTP,
    CurrentStatusAfterDTP,
    ViolationType,
    ProxyType,
    FuelCardType,
    MailText,
    Template,

    UserAccess,
    Profession,

    Sort,
    Filter,

    AddDriver,
    DeleteDriver,

    MyPointList,
    RouteList,

    MileageFill,
    FuelLoad
  }

  public enum Fields
  {
    All = 1,
    Some = 2
  };

  public enum FuelReport
  {
    Петрол,
    Чеки,
    Neste
  }

  public enum Sex
  {
    Мужской = 0,
    Женский = 1
  }
}
