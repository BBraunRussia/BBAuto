using System.Configuration;
using BBAuto.Logic.Services.Account;
using BBAuto.Logic.Services.Car;
using BBAuto.Logic.Services.Car.Doc;
using BBAuto.Logic.Services.Car.Sale;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Logic.Services.DiagCard;
using BBAuto.Logic.Services.Documents;
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
    }
  }
}
