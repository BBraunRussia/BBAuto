using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using Common;

namespace BBAutoConsoleApplication.config
{
  public static class AutoMapperConfiguration
  {
    public static void Initialize()
    {
      IEnumerable<Assembly> assemblies = new[]
      {
        Assembly.GetAssembly(typeof(Context))
      };
      AutoProfiler.RegisterProfiles(assemblies);
    }
  }
}
