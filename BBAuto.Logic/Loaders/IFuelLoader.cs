using System.Collections.Generic;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Loaders
{
  public interface IFuelLoader
  {
    IList<string> Load(string path, FuelReport fuelReport);
  }
}