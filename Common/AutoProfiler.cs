using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Common
{
  public class AutoProfiler
  {
    public static void RegisterProfiles(IEnumerable<Assembly> assemblies)
    {
      Mapper.Initialize(cfg => GetConfiguration(cfg, assemblies));
    }

    private static void GetConfiguration(IMapperConfigurationExpression cfg, IEnumerable<Assembly> assemblies)
    {
      foreach (var current in assemblies)
      {
        foreach (var type in from t in
          current.GetTypes()
          where t != typeof(Profile) && typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract
          select t)
        {
          var instance = (Profile)Activator.CreateInstance(type);

          cfg.AddProfile(instance);
        }
      }
    }
  }
}
