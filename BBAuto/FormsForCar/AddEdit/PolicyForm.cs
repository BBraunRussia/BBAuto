using System;
using System.Windows.Forms;
using BBAuto.Domain.Common;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Services.Comp;
using BBAuto.Domain.Static;

namespace BBAuto.FormsForCar.AddEdit
{
  public partial class PolicyForm : Form
  {
    private readonly ICompService _compService;
    private Policy _policy;
    private bool _loadCompleted;

    private WorkWithForm _workWithForm;

    public PolicyForm(Policy policy)
    {
      InitializeComponent();
      _loadCompleted = false;

      _policy = policy;
      _compService = new CompService();
    }

    private void Policy_AddEdit_Load(object sender, EventArgs e)
    {
      LoadDictionary();

      if (_policy.ID == 0)
        ChangeDateEnd();
      else
        FillFields();

      SetVisible();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_policy.ID == 0);
    }

    private void LoadDictionary()
    {
      _loadCompleted = false;
      LoadOneStringDictionary(cbPolicyType, "PolicyType");
      LoadOneStringDictionary(cbOwner, "Owner");
      LoadOneStringDictionary(cbComp, "Comp");

      cbComp.DataSource = _compService.GetCompList();
      cbComp.ValueMember = nameof(Comp.Id);
      cbComp.DisplayMember = nameof(Comp.Name);

      _loadCompleted = true;
    }

    private static void LoadOneStringDictionary(ComboBox combo, string name)
    {
      combo.DataSource = OneStringDictionary.getDataTable(name);
      combo.DisplayMember = "Название";
      combo.ValueMember = name + "_id";
    }

    private void FillFields()
    {
      cbPolicyType.SelectedValue = (int) _policy.Type;
      cbOwner.SelectedValue = _policy.IdOwner;
      cbComp.SelectedValue = _policy.CompId;
      tbNumber.Text = _policy.Number;
      dtpDateBegin.Value = _policy.DateBegin;
      dtpDateEnd.Value = _policy.DateEnd;
      tbPay.Text = _policy.Pay;
      tbComment.Text = _policy.Comment;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _policy.File;

      if (_policy.Type == PolicyType.ДСАГО || _policy.Type == PolicyType.GAP)
        tbLimitCost.Text = _policy.LimitCost;
      else if (_policy.Type == PolicyType.КАСКО)
      {
        tbLimitCost.Text = _policy.LimitCost;
        tbPay2.Text = _policy.Pay2;
        dtpDatePay2.Value = _policy.DatePay2;
      }

      tb_Leave(tbPay);
      tb_Leave(tbPay2);
      tb_Leave(tbLimitCost);

      SetVisible();
    }

    private void SetVisible()
    {
      var policyType = GetPolicyType();

      if (policyType == PolicyType.ОСАГО || policyType == PolicyType.расш_КАСКО)
      {
        lbLimitCost.Visible = false;
        tbLimitCost.Visible = false;
        lbPay2.Visible = false;
        tbPay2.Visible = false;
        lbPay2Date.Visible = false;
        dtpDatePay2.Visible = false;
      }
      if (policyType == PolicyType.КАСКО)
      {
        var twoPaymentCount = _compService.GetCompById(_policy.CompId).KaskoPaymentCount == 2;

        lbLimitCost.Text = "Страховая стоимость, руб:";
        lbLimitCost.Visible = true;
        tbLimitCost.Visible = true;
        lbPay2.Visible = twoPaymentCount;
        tbPay2.Visible = twoPaymentCount;
        lbPay2Date.Visible = twoPaymentCount;
        dtpDatePay2.Visible = twoPaymentCount;
      }
      else if (policyType == PolicyType.ДСАГО || policyType == PolicyType.GAP)
      {
        lbLimitCost.Text = "Лимит, руб:";
        lbLimitCost.Visible = true;
        tbLimitCost.Visible = true;
        lbPay2.Visible = false;
        tbPay2.Visible = false;
        lbPay2Date.Visible = false;
        dtpDatePay2.Visible = false;
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        CopyFields();
        _policy.Save();
        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void CopyFields()
    {
      _policy.IdOwner = cbOwner.SelectedValue.ToString();
      _policy.CompId = Convert.ToInt32(cbComp.SelectedValue);
      _policy.Type = GetPolicyType();
      _policy.Number = tbNumber.Text;
      _policy.DateBegin = dtpDateBegin.Value.Date;
      _policy.DateEnd = dtpDateEnd.Value.Date;
      _policy.Pay = tbPay.Text;
      _policy.Comment = tbComment.Text;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      _policy.File = tbFile.Text;

      if (_policy.Type == PolicyType.ДСАГО || _policy.Type == PolicyType.GAP)
        _policy.LimitCost = tbLimitCost.Text;
      else if (_policy.Type == PolicyType.КАСКО && _compService.GetCompById(_policy.CompId).KaskoPaymentCount == 2)
      {
        _policy.LimitCost = tbLimitCost.Text;
        _policy.Pay2 = tbPay2.Text;
        _policy.DatePay2 = dtpDatePay2.Value.Date;
      }
    }

    private void cbPolicyType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_loadCompleted)
        SetVisible();
    }

    private void dtpDateBegin_ValueChanged(object sender, EventArgs e)
    {
      ChangeDateEnd();
    }

    private void ChangeDateEnd()
    {
      dtpDateEnd.Value = dtpDateBegin.Value.AddYears(1).AddDays(-1);
      dtpDatePay2.Value = dtpDateBegin.Value.AddMonths(6);
    }

    private PolicyType GetPolicyType()
    {
      return _loadCompleted ? (PolicyType) Convert.ToInt32(cbPolicyType.SelectedValue) : PolicyType.ОСАГО;
    }

    private void tbPay_Enter(object sender, EventArgs e)
    {
      tb_Enter(tbPay);
    }

    private void tbPay_Leave(object sender, EventArgs e)
    {
      tbPay2.Text = tbPay.Text;
      tb_Leave(tbPay);
    }

    private void tb_Leave(TextBox tb)
    {
      tb.Text = MyString.GetFormatedDigit(tb.Text);
    }

    private void tb_Enter(TextBox tb)
    {
      tb.Text = tb.Text.Replace(" ", "");
    }

    private void tbPay2_Enter(object sender, EventArgs e)
    {
      tb_Enter(tbPay2);
    }

    private void tbPay2_Leave(object sender, EventArgs e)
    {
      tb_Enter(tbPay2);
    }

    private void tbLimitCost_Leave(object sender, EventArgs e)
    {
      tb_Enter(tbLimitCost);
    }

    private void tbLimitCost_Enter(object sender, EventArgs e)
    {
      tb_Enter(tbLimitCost);
    }

    private void cbComp_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_loadCompleted)
        SetVisible();
    }
  }
}
