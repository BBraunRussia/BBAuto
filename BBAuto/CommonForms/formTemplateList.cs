using System;
using System.Windows.Forms;
using BBAuto.CommonForms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Common;

namespace BBAuto
{
  public partial class formTemplateList : Form
  {
    private MainDGV _dgvMain;
    private readonly TemplateList _templateList;

    public formTemplateList()
    {
      InitializeComponent();

      _templateList = TemplateList.getInstance();
    }

    private void TemplateList_Load(object sender, EventArgs e)
    {
      LoadData();

      _dgvMain = new MainDGV(_dgvTemplate);

      ResizeDgv();
    }

    private void LoadData()
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
      OpenAddEdit(_templateList.getItem(_dgvMain.GetID()));
    }

    private void OpenAddEdit(Template template)
    {
      var templateForm = new TemplateAddEdit(template);
      if (templateForm.ShowDialog() == DialogResult.OK)
        LoadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      _templateList.Delete(_dgvMain.GetID());

      LoadData();
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
