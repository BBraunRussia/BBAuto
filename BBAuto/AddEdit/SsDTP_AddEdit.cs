using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForCar;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class SsDTP_AddEdit : Form
  {
    private readonly SsDTP _ssDtp;

    private WorkWithForm _workWithForm;

    public SsDTP_AddEdit(SsDTP ssDtp)
    {
      InitializeComponent();

      _ssDtp = ssDtp;
    }

    private void aeSsDTP_Load(object sender, EventArgs e)
    {
      loadDictionary();

      cbMark.SelectedValue = _ssDtp.MarkId;
      cbServiceStantion.SelectedValue = _ssDtp.ServiceStantionId;

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_ssDtp.ID == 0);
    }

    private void loadDictionary()
    {
      Marks marks = Marks.getInstance();

      cbMark.DataSource = marks.ToDataTable();
      cbMark.DisplayMember = "Название";
      cbMark.ValueMember = "id";

      ServiceStantions serviceStantions = ServiceStantions.getInstance();

      cbServiceStantion.DataSource = serviceStantions.ToDataTable();
      cbServiceStantion.DisplayMember = "Название";
      cbServiceStantion.ValueMember = "id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (int.TryParse(cbMark.SelectedValue.ToString(), out int markId))
        {
          _ssDtp.MarkId = markId;

          if (int.TryParse(cbServiceStantion.SelectedValue.ToString(), out int serviceStantionId))
            _ssDtp.ServiceStantionId = serviceStantionId;

          _ssDtp.Save();

          DialogResult = DialogResult.OK;
        }
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
