using BBAuto.App.AddEdit;
using BBAuto.App.ContextMenu;
using BBAuto.App.Dictionary;
using BBAuto.App.FormsForCar;
using BBAuto.App.FormsForCar.AddEdit;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BBAuto.App.config
{
  public class WindsorFormInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Component.For<IMyMenu>()
        .ImplementedBy<MyMenu>()
        .LifestyleTransient());

      container.Register(Component.For<IMyMenuItemFactory>()
        .ImplementedBy<MyMenuItemFactory>()
        .LifestyleTransient());
      
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

      container.Register(Component.For<IMileageForm>()
        .ImplementedBy<MileageForm>()
        .LifestyleTransient());

      container.Register(Component.For<IViolationForm>()
        .ImplementedBy<ViolationForm>()
        .LifestyleTransient());

      container.Register(Component.For<IDiagCardForm>()
        .ImplementedBy<DiagCardForm>()
        .LifestyleTransient());
    }
  }
}
