using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Services.Dictionary.Mark;
using Common.Resources;

namespace BBAuto.App.AddEdit
{
  public partial class SsDtpForm : Form, ISsDtpForm
  {
    private SsDTP _ssDtp;
    private WorkWithForm _workWithForm;

    private readonly IMarkService _markService;

    public SsDtpForm(IMarkService markService)
    {
      _markService = markService;

      InitializeComponent();
    }

    public DialogResult ShowDialog(SsDTP ssDtp)
    {
      _ssDtp = ssDtp;

      return ShowDialog();
    }

    private void AeSsDTP_Load(object sender, EventArgs e)
    {
      LoadDictionary();

      cbMark.SelectedValue = _ssDtp.Id;
      cbServiceStantion.SelectedValue = _ssDtp.IDServiceStantion;

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_ssDtp.Id == 0);
    }

    private void LoadDictionary()
    {
      cbMark.DataSource = _markService.GetItems();
      cbMark.DisplayMember = Columns.Name;
      cbMark.ValueMember = Columns.Id;

      var serviceStantions = ServiceStantions.getInstance();

      cbServiceStantion.DataSource = serviceStantions.ToDataTable();
      cbServiceStantion.DisplayMember = "Название";
      cbServiceStantion.ValueMember = "id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        int.TryParse(cbMark.SelectedValue.ToString(), out int markId);
        _ssDtp.MarkId = markId;

        _ssDtp.IDServiceStantion = cbServiceStantion.SelectedValue.ToString();

        _ssDtp.Save();

        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
