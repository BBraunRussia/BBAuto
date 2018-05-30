using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using Common;

namespace BBAuto.ConsoleApp.config
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
