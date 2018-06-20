using System.Windows.Forms;
using BBAuto.Domain.Services.Comp;

namespace BBAuto.AddEdit
{
  public partial class CompForm : Form
  {
    private readonly ICompService _compService;
    private readonly Comp _comp;

    private WorkWithForm _workWithForm;

    public CompForm(ICompService compService, int id)
    {
      InitializeComponent();

      _compService = compService;

      _comp = id == 0
        ? new Comp()
        : _compService.GetCompById(id);
    }

    private void CompForm_Load(object sender, System.EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(Controls, btnSave, btnClose);

      _workWithForm.SetEditMode(_comp.Id == 0);
    }

    private void FillFields()
    {
      tbName.Text = _comp.Name;

      rb1.Checked = _comp.KaskoPaymentCount == 1;
      rb2.Checked = _comp.KaskoPaymentCount == 2;
    }

    private void btnSave_Click(object sender, System.EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        CopyFields();
        _compService.SaveComp(_comp);
        DialogResult = DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void CopyFields()
    {
      _comp.Name = tbName.Text;
      _comp.KaskoPaymentCount = rb1.Checked ? 1 : 2;
    }
  }
}
