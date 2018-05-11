using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Common
{
  public class WayBillDay : MainDictionary, IEnumerable
  {
    private DateTime _date;
    private readonly int _count;
    private List<Route> _routes;

    public WayBillDay(DataRow row)
    {
      Id = Convert.ToInt32(row[0]);

      int.TryParse(row[1].ToString(), out int idCar);
      CarId = idCar;

      int idDriver;
      int.TryParse(row[2].ToString(), out idDriver);
      DriverList driverList = DriverList.getInstance();
      Driver = driverList.getItem(idDriver);

      DateTime.TryParse(row[3].ToString(), out _date);
    }

    public WayBillDay(int carId, Driver driver, DateTime date, int count)
    {
      CarId = carId;
      Driver = driver;
      _date = date;
      _count = count;
    }

    public string Day => _date.Day.ToString();

    public DateTime Date => _date;

    public Driver Driver { get; }

    public int CarId { get; }

    public int Distance
    {
      get
      {
        int distance = 0;
        foreach (Route route in this)
          distance += route.Distance;

        return distance;
      }
    }

    public void ReadRoute(Random random)
    {
      if (_routes == null)
        GetRouteList();

      if (_routes.Count == 0)
        Create(random);
    }

    private void GetRouteList()
    {
      WayBillRouteList wayBillRouteList = WayBillRouteList.getInstance();

      _routes = wayBillRouteList.GetList(this).ToList();

      if (_routes == null)
        _routes = new List<Route>();
    }

    private void Create(Random random)
    {
      SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
      SuppyAddress suppyAddress = suppyAddressList.getItemByRegion(Driver.Region.Id);

      if (suppyAddress == null)
        throw new NullReferenceException("Не задан адрес подачи");

      MyPoint currentPoint = suppyAddress.Point;

      RouteList routeList = RouteList.getInstance();

      int residue = _count;
      Route route;

      do
      {
        int i = 0;

        do
        {
          route = routeList.GetRandomItem(random, currentPoint);

          if (i == 10)
            break;
          i++;
        } while (residue - route.Distance < 10);

        Add(route);
        residue -= Convert.ToInt32(route.Distance);

        currentPoint = route.MyPoint2;
      } while ((residue > 10) && (_routes.Count < 16));

      if (currentPoint != suppyAddress.Point)
      {
        route = routeList.GetItem(currentPoint, suppyAddress.Point);
        Add(route);
      }
    }

    private void Add(Route route)
    {
      _routes.Add(route);

      WayBillRoute wayBillRoute = new WayBillRoute(this);
      wayBillRoute.Route = route;
      wayBillRoute.Save();
    }

    public override void Save()
    {
      Id = Convert.ToInt32(Provider.Insert("WayBillDay", Id, CarId, Driver.Id, _date));

      WayBillDayList wayBillDayList = WayBillDayList.getInstance();
      wayBillDayList.Add(this);
    }

    internal override void Delete()
    {
      Provider.Delete("WayBillDay", Id);
    }

    internal override object[] ToRow()
    {
      return new object[] {_date.ToShortDateString(), Driver.GetName(NameType.Short)};
    }

    public IEnumerator GetEnumerator()
    {
      if (_routes == null)
        GetRouteList();

      foreach (var item in _routes)
      {
        yield return item;
      }
    }
  }
}
