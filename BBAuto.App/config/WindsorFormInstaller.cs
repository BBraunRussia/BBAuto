using BBAuto.App.AddEdit;
using BBAuto.App.Dictionary;
using BBAuto.App.FormsForCar;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BBAuto.App.config
{
  public class WindsorFormInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Component.For<IForm>()
        .ImplementedBy<MainForm>()
        .LifestyleTransient());

      container.Register(Component.For<ICarForm>()
        .ImplementedBy<CarForm>()
        .LifestyleTransient());
      container.Register(Component.For<ISaleCarForm>()
        .ImplementedBy<SaleCarForm>()
        .LifestyleTransient());

      container.Register(Component.For<IDealerListForm>()
        .ImplementedBy<DealerListForm>()
        .LifestyleTransient());
    }
  }
}
