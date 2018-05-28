using System.Configuration;
using BBAuto.Logic.Loaders;
using BBAuto.Logic.Services.Account;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Car.Doc;
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Dictionary;
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
using BBAuto.Logic.Services.Driver.DriverCar;
using BBAuto.Logic.Services.Grade;
using BBAuto.Logic.Services.Mileage;
using BBAuto.Logic.Services.Violation;
using BBAuto.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Common;

namespace BBAuto.Logic
{
  public class WindsorInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Component.For<IFuelLoader>()
        .ImplementedBy<FuelLoader>()
        .LifestyleTransient());

      container.Register(Component.For(typeof(IRepository<>))
        .ImplementedBy(typeof(Repository<>))
        .DependsOn(new
        {
          connectionStringSettings =
            ConfigurationManager.ConnectionStrings[Consts.Config.ConnectionName]
        })
        .LifeStyle.Transient);

      container.Register(Component.For<IDbContext>()
        .ImplementedBy<DbContext>()
        .DependsOn(new
        {
          ConnectionStringSettings = ConfigurationManager.ConnectionStrings[Consts.Config.ConnectionName]
        })
        .LifestyleTransient());

      container.Register(Component.For<IAccountService>()
        .ImplementedBy<AccountService>()
        .LifestyleTransient());

      container.Register(Component.For<IDealerService>()
        .ImplementedBy<DealerService>()
        .LifestyleTransient());

      container.Register(Component.For<ICarService>()
        .ImplementedBy<CarService>()
        .LifestyleTransient());
      container.Register(Component.For<ISaleCarService>()
        .ImplementedBy<SaleCarService>()
        .LifestyleTransient());
      container.Register(Component.For<ICarDocService>()
        .ImplementedBy<CarDocService>()
        .LifestyleTransient());

      container.Register(Component.For<IMileageService>()
        .ImplementedBy<MileageService>()
        .LifestyleTransient());

      container.Register(Component.For<IDiagCardService>()
        .ImplementedBy<DiagCardService>()
        .LifestyleTransient());

      container.Register(Component.For<IDocumentsService>()
        .ImplementedBy<DocumentsService>()
        .LifestyleTransient());

      container.Register(Component.For<IViolationService>()
        .ImplementedBy<ViolationService>()
        .LifestyleTransient());

      container.Register(Component.For<IGradeService>()
        .ImplementedBy<GradeService>()
        .LifestyleTransient());

      container.Register(Component.For<IDictionaryService<ColorModel>>()
        .Forward<IColorService>()
        .ImplementedBy<ColorService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<CompModel>>()
        .Forward<ICompService>()
        .ImplementedBy<CompService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<CulpritModel>>()
        .Forward<ICulpritService>()
        .ImplementedBy<CulpritService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<CurrentStatusAfterDtpModel>>()
        .Forward<ICurrentStatusAfterDtpService>()
        .ImplementedBy<CurrentStatusAfterDtpService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<EmployeesNameModel>>()
        .Forward<IEmployeesNameService>()
        .ImplementedBy<EmployeesNameService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<EngineTypeModel>>()
        .Forward<IEngineTypeService>()
        .ImplementedBy<EngineTypeService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<FuelCardTypeModel>>()
        .Forward<IFuelCardTypeService>()
        .ImplementedBy<FuelCardTypeService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<MarkModel>>()
        .Forward<IMarkService>()
        .ImplementedBy<MarkService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<OwnerModel>>()
        .Forward<IOwnerService>()
        .ImplementedBy<OwnerService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<ProxyTypeModel>>()
        .Forward<IProxyTypeService>()
        .ImplementedBy<ProxyTypeService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<RegionModel>>()
        .Forward<IRegionService>()
        .ImplementedBy<RegionService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<RepairTypeModel>>()
        .Forward<IRepairTypeService>()
        .ImplementedBy<RepairTypeService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<ServiceStantionModel>>()
        .Forward<IServiceStantionService>()
        .ImplementedBy<ServiceStantionService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<StatusAfterDtpModel>>()
        .Forward<IStatusAfterDtpService>()
        .ImplementedBy<StatusAfterDtpService>()
        .LifestyleTransient());
      container.Register(Component.For<IDictionaryService<ViolationTypeModel>>()
        .Forward<IViolationTypeService>()
        .ImplementedBy<ViolationTypeService>()
        .LifestyleTransient());

      container.Register(Component.For<IDriverCarService>()
        .ImplementedBy<DriverCarService>()
        .LifestyleTransient());
    }
  }
}
