using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BBAuto.Domain.Static;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.CarSale;
using BBAuto.Domain.Services.Comp;

namespace BBAuto.Domain.ForCar
{
  public sealed class Policy : MainDictionary, IActual
  {
    private string _number;
    private int _idOwner;
    private int _idAccount;
    private int _idAccount2;
    private double _limitCost;
    private int _idPolicyType;
    private DateTime _dateBegin;
    private DateTime _dateEnd;
    private int _notifacationSent;

    public string File { get; set; }
    public DateTime DateCreate { get; private set; }
    
    public double Pay { get; set; }
    public double Pay2 { get; set; }

    public string PayString => Pay.ToString().Replace(',', '.');
    public string Pay2String => Pay2.Equals(0) ? string.Empty : Pay2.ToString().Replace(',', '.');
    
    public string IdOwner
    {
      get => _idOwner.ToString();
      set => int.TryParse(value, out _idOwner);
    }

    public int CompId { get; set; }

    public string Number
    {
      get => _number == string.Empty ? "нет данных" : _number;
      set => _number = value;
    }

    public bool IsCarSale => Car.IsSale;

    public bool IsCarSaleWithDate
    {
      get
      {
        if (!Car.IsSale)
          return false;

        ICarSaleService carSaleService = new CarSaleService();
        return carSaleService.GetCarSaleByCarId(Car.ID)?.Date != null;
      }
    }

    public DateTime DateBegin
    {
      get => _dateBegin == new DateTime() ? DateTime.Today : _dateBegin;
      set => _dateBegin = value;
    }

    public DateTime DateEnd
    {
      get => _dateEnd == new DateTime() ? DateTime.Today : _dateEnd;
      set => _dateEnd = value;
    }

    public string LimitCost
    {
      get => _limitCost.Equals(0) ? string.Empty : _limitCost.ToString().Replace(',', '.');
      set => double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _limitCost);
    }
    
    public PolicyType Type
    {
      get => (PolicyType) _idPolicyType;
      set => _idPolicyType = (int) value;
    }

    public DateTime DatePay2 { get; set; }

    public string DatePay2ToString => IsEmptyDate(DatePay2) ? string.Empty : DatePay2.ToShortDateString();

    public string DatePay2ForSql => IsEmptyDate(DatePay2)
      ? string.Empty
      : DatePay2.Year + "-" + DatePay2.Month + "-" + DatePay2.Day;

    internal bool IsNotificationSent
    {
      get => Convert.ToBoolean(_notifacationSent);
      private set => _notifacationSent = Convert.ToInt32(value);
    }

    public string Comment { get; set; }
    public Car Car { get; private set; }

    public Policy(Car car)
    {
      Car = car;
      ID = 0;
    }

    public Policy(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      if (int.TryParse(row.ItemArray[0].ToString(), out int id))
        ID = id;

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      Car = CarList.GetInstance().getItem(idCar);

      int.TryParse(row.ItemArray[2].ToString(), out _idPolicyType);
      IdOwner = row.ItemArray[3].ToString();

      if (int.TryParse(row.ItemArray[4].ToString(), out int compId))
        CompId = compId;

      _number = row.ItemArray[5].ToString();
      DateTime.TryParse(row.ItemArray[6].ToString(), out _dateBegin);
      DateTime.TryParse(row.ItemArray[7].ToString(), out _dateEnd);

      if (double.TryParse(row.ItemArray[8].ToString(), out double pay))
        Pay = pay;

      File = row.ItemArray[9].ToString();
      _fileBegin = File;

      LimitCost = row.ItemArray[10].ToString();

      if (double.TryParse(row.ItemArray[11].ToString(), out double pay2))
        Pay2 = pay2;

      if (DateTime.TryParse(row.ItemArray[12].ToString(), out DateTime datePay2))
        DatePay2 = datePay2;

      int.TryParse(row.ItemArray[13].ToString(), out _idAccount);
      int.TryParse(row.ItemArray[14].ToString(), out _idAccount2);

      int.TryParse(row.ItemArray[15].ToString(), out _notifacationSent);

      Comment = row.ItemArray[16].ToString();

      if (DateTime.TryParse(row.ItemArray[17].ToString(), out DateTime dateCreate))
        DateCreate = new DateTime(dateCreate.Year, dateCreate.Month, dateCreate.Day);
    }

    public override void Save()
    {
      if (ID == 0)
      {
        PolicyList.getInstance().Add(this);

        execSave();
      }

      DeleteFile(File);

      File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Policy", $"{_number}_{DateBegin.Year}");
      _fileBegin = File;

      execSave();
    }

    private void execSave()
    {
      int id;
      int.TryParse(
        _provider.Insert("Policy", ID, _idPolicyType, Car.ID, IdOwner, CompId, _number, _dateBegin, _dateEnd, PayString,
          LimitCost, Pay2String, DatePay2ForSql, File, _notifacationSent, Comment), out id);
      ID = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("Policy", ID);
    }

    public void SetAccountID(int idAccount, int paymentNumber)
    {
      _provider.DoOther("exec Policy_Insert_AccountID @p1, @p2, @p3", ID, idAccount, paymentNumber);
    }

    public bool IsInList(Account account)
    {
      return account.ID != 0 && (_idAccount == account.ID || _idAccount2 == account.ID);
    }

    public void ClearAccountID(Account account)
    {
      var sqlPaymentNumber = 1;

      if (account.IsPolicyKaskoAndPayment2())
      {
        _idAccount2 = 0;
        sqlPaymentNumber = 2;
      }
      else
        _idAccount = 0;

      _provider.DoOther("exec Policy_Delete_AccountID @p1, @p2", ID, sqlPaymentNumber);
    }

    internal bool IsHaveAccountID(int paymentNumber)
    {
      return (paymentNumber == 1) ? _idAccount != 0 : _idAccount2 != 0;
    }

    public bool IsAgreed(int paymentNumber)
    {
      if (!IsHaveAccountID(paymentNumber))
        return false;

      var idAccount = paymentNumber == 1 ? _idAccount : _idAccount2;

      var account = AccountList.getInstance().getItem(idAccount);
      return account.Agreed;
    }

    public object[] CreateRow(IList<Comp> compList)
    {
      return new object[]
      {
        ID, Car.ID, Car.BBNumber, Car.Grz, Type, Owners.getInstance().getItem(Convert.ToInt32(IdOwner)),
        compList.FirstOrDefault(comp => comp.Id == CompId)?.Name,
        Number, Pay, DateBegin, DateEnd,
        _limitCost, Pay2
      };
    }

    internal override object[] getRow()
    {
      throw new NotImplementedException();
    }

    private static bool IsEmptyDate(DateTime date)
    {
      return date == new DateTime();
    }

    public string ToMail()
    {
      IsNotificationSent = true;
      execSave();

      StringBuilder sb = new StringBuilder();
      sb.Append(Car.Grz);
      sb.Append(" ");
      sb.Append(Type);
      sb.Append(" ");
      sb.Append(Number);
      sb.Append(" ");
      sb.Append(DateEnd.ToShortDateString());
      return sb.ToString();
    }

    public bool IsDateActual()
    {
      throw new NotImplementedException();
    }

    public bool IsHaveFile()
    {
      throw new NotImplementedException();
    }

    public bool IsActual()
    {
      return _dateEnd >= DateTime.Today;
    }
  }
}
