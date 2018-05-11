using System;
using System.Windows.Forms;
using BBAuto.App.Utils.DGV;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;

namespace BBAuto.App.CommonForms
{
  public partial class TemplateListForm : Form, ITemplateListForm
  {
    private readonly TemplateList _templateList;

    private readonly IMainDgv _mainDgv;

    public TemplateListForm(IMainDgv mainDgv)
    {
      _mainDgv = mainDgv;
      InitializeComponent();

      _templateList = TemplateList.getInstance();
    }

    private void TemplateList_Load(object sender, EventArgs e)
    {
      loadData();

      _mainDgv.SetDgv(_dgvTemplate);

      ResizeDgv();
    }

    private void loadData()
    {
      _dgvTemplate.DataSource = _templateList.ToDataTable();

      if (_dgvTemplate.Columns.Count > 0)
        _dgvTemplate.Columns[0].Visible = false;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      OpenAddEdit(new Template());
    }

    private void _dgvTemplate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      OpenAddEdit(_templateList.getItem(_mainDgv.GetId()));
    }

    private void OpenAddEdit(Template template)
    {
      TemplateAddEdit templateAE = new TemplateAddEdit(template);
      if (templateAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      _templateList.Delete(_mainDgv.GetId());

      loadData();
    }

    private void _dgvTemplate_Resize(object sender, EventArgs e)
    {
      ResizeDgv();
    }

    private void ResizeDgv()
    {
      _dgvTemplate.Columns[1].Width = _dgvTemplate.Width / 2;
      _dgvTemplate.Columns[2].Width = _dgvTemplate.Width / 2;
    }
  }
}
