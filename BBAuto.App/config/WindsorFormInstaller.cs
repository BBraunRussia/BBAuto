using BBAuto.App.AddEdit;
using BBAuto.App.CommonForms;
using BBAuto.App.ContextMenu;
using BBAuto.App.Dictionary;
using BBAuto.App.FormsForCar;
using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.App.Utils.DGV;
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
      container.Register(Component.For<IDgvFormatter>()
        .ImplementedBy<DgvFormatter>()
        .LifestyleTransient());
      container.Register(Component.For<IMainDgv>()
        .ImplementedBy<MainDgv>()
        .LifestyleTransient());


      container.Register(Component.For<IForm>()
        .ImplementedBy<MainForm>()
        .LifestyleTransient());

      container.Register(Component.For<ILoadFuelForm>()
        .ImplementedBy<LoadFuelForm>()
        .LifestyleTransient());

      container.Register(Component.For<IDealerListForm>()
        .ImplementedBy<DealerListForm>()
        .LifestyleTransient());
      container.Register(Component.For<IRouteListForm>()
        .ImplementedBy<RouteListForm>()
        .LifestyleTransient());
      container.Register(Component.For<IMyPointListForm>()
        .ImplementedBy<MyPointListForm>()
        .LifestyleTransient());
      container.Register(Component.For<ITemplateListForm>()
        .ImplementedBy<TemplateListForm>()
        .LifestyleTransient());
      container.Register(Component.For<IGradeListForm>()
        .ImplementedBy<GradeListForm>()
        .LifestyleTransient());
      container.Register(Component.For<IModelListForm>()
        .ImplementedBy<ModelListForm>()
        .LifestyleTransient());
      container.Register(Component.For<ISsDtpListForm>()
        .ImplementedBy<SsDtpListForm>()
        .LifestyleTransient());
      container.Register(Component.For<IOneStringDictionaryListForm>()
        .ImplementedBy<OneStringDictionaryListForm>()
        .LifestyleTransient());
      container.Register(Component.For<IOneStringDictionaryForm>()
        .ImplementedBy<OneStringDictionaryForm>()
        .LifestyleTransient());

      container.Register(Component.For<ICarForm>()
        .ImplementedBy<CarForm>()
        .LifestyleTransient());
      container.Register(Component.For<ISaleCarForm>()
        .ImplementedBy<SaleCarForm>()
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
      container.Register(Component.For<ICarDocForm>()
        .ImplementedBy<CarDocForm>()
        .LifestyleTransient());
      container.Register(Component.For<ICarInfoForm>()
        .ImplementedBy<CarInfoForm>()
        .LifestyleTransient());
      container.Register(Component.For<IGradeForm>()
        .ImplementedBy<GradeForm>()
        .LifestyleTransient());
      container.Register(Component.For<ISsDtpForm>()
        .ImplementedBy<SsDtpForm>()
        .LifestyleTransient());
    }
  }
}
