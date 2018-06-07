using System;
using System.Data;
using System.Text;
using BBAuto.Domain.Static;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Services.CarSale;

namespace BBAuto.Domain.ForCar
{
  public sealed class Policy : MainDictionary, IActual
  {
    private string _number;
    private double _pay;
    private int _idOwner;
    private int _idComp;
    private int _idAccount;
    private int _idAccount2;
    private double _limitCost;
    private double _pay2;
    private int _idPolicyType;
    private DateTime _dateBegin;
    private DateTime _dateEnd;
    private int _notifacationSent;

    public string File { get; set; }
    public DateTime DateCreate { get; private set; }

    public string Pay
    {
      get { return _pay.ToString().Replace(',', '.'); }
      set { double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _pay); }
    }

    public double PayToDouble
    {
      get { return _pay; }
    }

    public string IdOwner
    {
      get { return _idOwner.ToString(); }
      set { int.TryParse(value, out _idOwner); }
    }

    public string IdComp
    {
      get { return _idComp.ToString(); }
      set { int.TryParse(value, out _idComp); }
    }

    public string Number
    {
      get { return _number == string.Empty ? "нет данных" : _number; }
      set { _number = value; }
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
      get { return _dateBegin == new DateTime() ? DateTime.Today : _dateBegin; }
      set { _dateBegin = value; }
    }

    public DateTime DateEnd
    {
      get { return _dateEnd == new DateTime() ? DateTime.Today : _dateEnd; }
      set { _dateEnd = value; }
    }

    public string LimitCost
    {
      get { return _limitCost.Equals(0) ? string.Empty : _limitCost.ToString().Replace(',', '.'); }
      set { double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _limitCost); }
    }

    public string Pay2
    {
      get { return _pay2.Equals(0) ? string.Empty : _pay2.ToString().Replace(',', '.'); }
      set { double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _pay2); }
    }

    public double Pay2ToDouble
    {
      get { return _pay2; }
    }

    public PolicyType Type
    {
      get { return (PolicyType) _idPolicyType; }
      set { _idPolicyType = (int) value; }
    }

    public DateTime DatePay2 { get; set; }

    public string DatePay2ToString
    {
      get { return IsEmptyDate(DatePay2) ? string.Empty : DatePay2.ToShortDateString(); }
    }

    public string DatePay2ForSQL
    {
      get
      {
        return IsEmptyDate(DatePay2)
          ? string.Empty
          : DatePay2.Year.ToString() + "-" + DatePay2.Month.ToString() + "-" + DatePay2.Day.ToString();
      }
    }

    internal bool IsNotificationSent
    {
      get { return Convert.ToBoolean(_notifacationSent); }
      private set { _notifacationSent = Convert.ToInt32(value); }
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
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      ID = id;

      int idCar;
      int.TryParse(row.ItemArray[1].ToString(), out idCar);
      Car = CarList.GetInstance().getItem(idCar);

      int.TryParse(row.ItemArray[2].ToString(), out _idPolicyType);
      IdOwner = row.ItemArray[3].ToString();
      IdComp = row.ItemArray[4].ToString();
      _number = row.ItemArray[5].ToString();
      DateTime.TryParse(row.ItemArray[6].ToString(), out _dateBegin);
      DateTime.TryParse(row.ItemArray[7].ToString(), out _dateEnd);
      Pay = row.ItemArray[8].ToString();
      File = row.ItemArray[9].ToString();
      _fileBegin = File;

      LimitCost = row.ItemArray[10].ToString();
      Pay2 = row.ItemArray[11].ToString();

      DateTime datePay2;
      DateTime.TryParse(row.ItemArray[12].ToString(), out datePay2);
      DatePay2 = datePay2;

      int.TryParse(row.ItemArray[13].ToString(), out _idAccount);
      int.TryParse(row.ItemArray[14].ToString(), out _idAccount2);

      int.TryParse(row.ItemArray[15].ToString(), out _notifacationSent);

      Comment = row.ItemArray[16].ToString();

      DateTime dateCreate;
      DateTime.TryParse(row.ItemArray[17].ToString(), out dateCreate);
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

      File = WorkWithFiles.fileCopyByID(File, "cars", Car.ID, "Policy", _number);
      _fileBegin = File;

      execSave();
    }

    private void execSave()
    {
      int id;
      int.TryParse(
        _provider.Insert("Policy", ID, _idPolicyType, Car.ID, IdOwner, IdComp, _number, _dateBegin, _dateEnd, Pay,
          LimitCost, Pay2, DatePay2ForSQL, File, _notifacationSent, Comment), out id);
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
      return (account.ID != 0) && ((_idAccount == account.ID) || (_idAccount2 == account.ID));
    }

    public void ClearAccountID(Account account)
    {
      int sqlPaymentNumber = 1;

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
      if (IsHaveAccountID(paymentNumber))
      {
        AccountList accountList = AccountList.getInstance();

        int idAccount = (paymentNumber == 1) ? _idAccount : _idAccount2;

        Account account = accountList.getItem(idAccount);
        return account.Agreed;
      }
      else
        return false;
    }

    internal override object[] getRow()
    {
      Owners owners = Owners.getInstance();
      Comps comps = Comps.getInstance();

      return new object[]
      {
        ID, Car.ID, Car.BBNumber, Car.Grz, Type, owners.getItem(Convert.ToInt32(IdOwner)),
        comps.getItem(Convert.ToInt32(IdComp)), Number, _pay, DateBegin, DateEnd,
        _limitCost, _pay2
      };
    }

    private bool IsEmptyDate(DateTime date)
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
