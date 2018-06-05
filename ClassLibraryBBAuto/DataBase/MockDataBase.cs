using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Entities;
using BBAuto.Repository;

namespace BBAuto.Domain.DataBase
{
  public class MockDataBase : IDataBase
  {
    private Hashtable _cars;
    private Hashtable _carSale;
    private Hashtable _accounts;

    public MockDataBase()
    {
      _cars = new Hashtable();
      _carSale = new Hashtable();
      _accounts = new Hashtable();
    }

    public DataTable Select(string tableName)
    {
      DataTable dt = new DataTable();

      switch (tableName)
      {
        case "Car":
          dt = CarToDataTable();
          break;
        case "Account":
          dt = AccountToDataTable();
          break;
      }

      return dt;
    }

    public string Insert(string tableName, params object[] Params)
    {
      string id = "0";

      switch (tableName)
      {
        case "Car":
          id = CarInsert();
          break;
        case "Account":
          id = AccountInsert(Params);
          break;
      }

      return id;
    }

    public DataTable GetRecords(string SQL, params object[] Params)
    {
      int id = 0;
      if (Params.Count() > 0)
        int.TryParse(Params.First().ToString(), out id);

      DataTable dt = new DataTable();

      switch (SQL.Split(' ')[1])
      {
        case "CarSale_Insert":
          CarSaleInsert(id);
          break;
        case "CarSale_Delete":
          CarSaleDelete(id);
          break;
        case "Car_Select":
          CarToDataTable();
          break;
        case "Car_Delete":
          CarDelete(id);
          break;
      }
      return dt;
    }

    public string GetRecordsOne(string SQL, params object[] Params)
    {
      string id = string.Empty;
      switch (SQL.Split(' ')[1])
      {
        case "Car_Insert":
          id = CarInsert();
          break;
      }

      return id;
    }

    private string CarInsert()
    {
      int idCar = _cars.Count + 1;
      _cars.Add(idCar, new Car());

      return idCar.ToString();
    }

    private string AccountInsert(params object[] Params)
    {
      int idAccount;
      int.TryParse(Params[0].ToString(), out idAccount);

      if (idAccount == 0)
      {
        idAccount = _accounts.Count + 1;

        Account account = new Account();
        account.IDPolicyType = Params[3].ToString();

        _accounts.Add(idAccount, account);
      }

      return idAccount.ToString();
    }

    private void CarSaleInsert(int idCar)
    {
      _carSale.Add(idCar, new CarSale(new Car(idCar)));
    }

    private void CarSaleDelete(int idCar)
    {
      _carSale.Remove(idCar);
    }

    private DataTable CarToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add();
      dt.Columns.Add();

      foreach (var item in _cars)
      {
        Car car = item as Car;
        object[] row = car.getRow();
        dt.Rows.Add(row[0], car.BBNumber);
      }

      return dt;
    }

    private DataTable CarSaleToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add();
      dt.Columns.Add();
      dt.Columns.Add();

      foreach (var item in _carSale)
      {
        CarSale carSaleItem = item as CarSale;
        object[] row = carSaleItem.getRow();
        dt.Rows.Add(row[0], row[5], row[6]);
      }

      return dt;
    }

    private void CarDelete(int idCar)
    {
      _cars.Remove(idCar);
    }

    private DataTable AccountToDataTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add();
      dt.Columns.Add();

      foreach (var item in _accounts)
      {
        Account account = item as Account;
        object[] row = account.getRow();
        dt.Rows.Add(row[0], account.IDPolicyType);
      }

      return dt;
    }

    public DataSet GetDataSet(string SQL, params object[] Params)
    {
      throw new NotImplementedException();
    }
  }
}
