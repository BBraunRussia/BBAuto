using System;
using System.Data;
using System.Text;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForCar
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
      get => _pay.ToString().Replace(',', '.');
      set => double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _pay);
    }

    public double PayToDouble => _pay;

    public string IdOwner
    {
      get => _idOwner.ToString();
      set => int.TryParse(value, out _idOwner);
    }

    public string IdComp
    {
      get => _idComp.ToString();
      set => int.TryParse(value, out _idComp);
    }

    public string Number
    {
      get => _number == string.Empty ? "нет данных" : _number;
      set => _number = value;
    }

    public bool IsCarSale => Car.info.IsSale;

    public bool IsCarSaleWithDate
    {
      get
      {
        if (Car.info.IsSale)
        {
          var carSaleList = CarSaleList.getInstance();
          return !string.IsNullOrEmpty(carSaleList.getItem(Car.Id).Date);
        }

        return false;
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

    public string Pay2
    {
      get => _pay2.Equals(0) ? string.Empty : _pay2.ToString().Replace(',', '.');
      set => double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _pay2);
    }

    public double Pay2ToDouble => _pay2;

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
      Id = 0;
    }

    public Policy(DataRow row)
    {
      FillFields(row);
    }

    private void FillFields(DataRow row)
    {
      int.TryParse(row.ItemArray[0].ToString(), out int id);
      Id = id;

      int.TryParse(row.ItemArray[1].ToString(), out int idCar);
      Car = CarList.getInstance().getItem(idCar);

      int.TryParse(row.ItemArray[2].ToString(), out _idPolicyType);
      IdOwner = row.ItemArray[3].ToString();
      IdComp = row.ItemArray[4].ToString();
      _number = row.ItemArray[5].ToString();
      DateTime.TryParse(row.ItemArray[6].ToString(), out _dateBegin);
      DateTime.TryParse(row.ItemArray[7].ToString(), out _dateEnd);
      Pay = row.ItemArray[8].ToString();
      File = row.ItemArray[9].ToString();
      FileBegin = File;

      LimitCost = row.ItemArray[10].ToString();
      Pay2 = row.ItemArray[11].ToString();

      DateTime.TryParse(row.ItemArray[12].ToString(), out DateTime datePay2);
      DatePay2 = datePay2;

      int.TryParse(row.ItemArray[13].ToString(), out _idAccount);
      int.TryParse(row.ItemArray[14].ToString(), out _idAccount2);

      int.TryParse(row.ItemArray[15].ToString(), out _notifacationSent);

      Comment = row.ItemArray[16].ToString();

      DateTime.TryParse(row.ItemArray[17].ToString(), out DateTime dateCreate);
      DateCreate = new DateTime(dateCreate.Year, dateCreate.Month, dateCreate.Day);
    }

    public override void Save()
    {
      if (Id == 0)
      {
        PolicyList.getInstance().Add(this);

        ExecSave();
      }

      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "Policy", _number);
      FileBegin = File;

      ExecSave();
    }

    private void ExecSave()
    {
      int.TryParse(
        Provider.Insert("Policy", Id, _idPolicyType, Car.Id, IdOwner, IdComp, _number, _dateBegin, _dateEnd, Pay,
          LimitCost, Pay2, DatePay2ForSql, File, _notifacationSent, Comment), out int id);
      Id = id;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("Policy", Id);
    }

    public void SetAccountId(int idAccount, int paymentNumber)
    {
      Provider.DoOther("exec Policy_Insert_AccountID @p1, @p2, @p3", Id, idAccount, paymentNumber);
    }

    public bool IsInList(Account account)
    {
      return account.Id != 0 && (_idAccount == account.Id || _idAccount2 == account.Id);
    }

    public void ClearAccountId(Account account)
    {
      var sqlPaymentNumber = 1;

      if (account.IsPolicyKaskoAndPayment2())
      {
        _idAccount2 = 0;
        sqlPaymentNumber = 2;
      }
      else
        _idAccount = 0;

      Provider.DoOther("exec Policy_Delete_AccountID @p1, @p2", Id, sqlPaymentNumber);
    }

    internal bool IsHaveAccountId(int paymentNumber)
    {
      return (paymentNumber == 1) ? _idAccount != 0 : _idAccount2 != 0;
    }

    public bool IsAgreed(int paymentNumber)
    {
      if (IsHaveAccountId(paymentNumber))
      {
        var accountList = AccountList.GetInstance();

        var idAccount = paymentNumber == 1 ? _idAccount : _idAccount2;

        Account account = accountList.getItem(idAccount);
        return account.Agreed;
      }

      return false;
    }

    internal override object[] ToRow()
    {
      var owners = Owners.getInstance();
      var comps = Comps.GetInstance();

      return new object[]
      {
        Id, Car.Id, Car.BBNumber, Car.Grz, Type, owners.getItem(Convert.ToInt32(IdOwner)),
        comps.getItem(Convert.ToInt32(IdComp)), Number, _pay, DateBegin, DateEnd,
        _limitCost, _pay2
      };
    }

    private static bool IsEmptyDate(DateTime date)
    {
      return date == new DateTime();
    }

    public string ToMail()
    {
      IsNotificationSent = true;
      ExecSave();

      var sb = new StringBuilder();
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
