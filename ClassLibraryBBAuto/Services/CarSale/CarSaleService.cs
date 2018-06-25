using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Services.Customer;
using BBAuto.Repository;
using BBAuto.Repository.Models;

namespace BBAuto.Domain.Services.CarSale
{
  public class CarSaleService : ICarSaleService
  {
    private readonly IDbContext _dbContext;

    public CarSaleService()
    {
      _dbContext = new DbContext();
    }

    public CarSale GetCarSaleByCarId(int carId)
    {
      var dbModel = _dbContext.CarSale.GetCarSaleByCarId(carId);

      return Mapper.Map<CarSale>(dbModel);
    }

    public void DeleteCarFromSale(int carId)
    {
      _dbContext.CarSale.DeleteCarSale(carId);
    }

    public CarSale SaveCarSale(CarSale carSale)
    {
      var dbModel = Mapper.Map<DbCarSale>(carSale);

      var result = _dbContext.CarSale.UpsertCarSale(dbModel);

      return Mapper.Map<CarSale>(result);
    }

    public IList<CarSale> GetCarSaleList()
    {
      var dbModels = _dbContext.CarSale.GetCarSaleList();

      return Mapper.Map<IList<CarSale>>(dbModels);
    }

    public DataTable ToDataTable()
    {
      var carSaleList = GetCarSaleList();

      DataTable dt = new DataTable();
      
      dt.Columns.Add("id");
      dt.Columns.Add("idCar");
      dt.Columns.Add("�������� �����");
      dt.Columns.Add("��������������� ����");
      dt.Columns.Add("�����");
      dt.Columns.Add("������");
      dt.Columns.Add("����������");
      dt.Columns.Add("������");
      dt.Columns.Add("���� �������", typeof(DateTime));
      dt.Columns.Add("�����������");
      dt.Columns.Add("� ���");
      dt.Columns.Add("� ���");
      dt.Columns.Add("������");

      ICustomerService customerService = new CustomerService();
      var customerList = customerService.GetCustomerList();

      foreach (var carSale in carSaleList)
        dt.Rows.Add(GetRow(carSale, customerList));

      return dt;
    }
    
    private object[] GetRow(CarSale carSale, IList<Customer.CustomerModel> customerList)
    {
      var car = CarList.GetInstance().getItem(carSale.CarId);

      InvoiceList invoiceList = InvoiceList.getInstance();
      Invoice invoice = invoiceList.getItem(car);

      PTSList ptsList = PTSList.getInstance();
      PTS pts = ptsList.getItem(car);

      STSList stsList = STSList.getInstance();
      STS sts = stsList.getItem(car);

      int.TryParse(car.regionUsingID.ToString(), out int idRegion);

      Regions regions = Regions.getInstance();
      string regionName = (invoice == null)
        ? regions.getItem(idRegion)
        : regions.getItem(Convert.ToInt32(invoice.RegionToID));

      var customer = customerList.FirstOrDefault(cust => cust.Id == carSale.CustomerId);

      return new object[]
      {
        carSale.CarId, carSale.CarId, car.BBNumber, car.Grz, car.Mark.Name, car.info.Model, customer?.FullName ?? "��� ������", regionName, carSale.Date, carSale.Comment, pts.Number,
        sts.Number, car.GetStatus()
      };
    }
  }
}
