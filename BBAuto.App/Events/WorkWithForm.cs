using System;
using System.Windows.Forms;
using BBAuto.Logic.Static;

namespace BBAuto.App.Events
{
  public class WorkWithForm
  {
    private bool _editMode;
    private readonly Control.ControlCollection _controls;
    private readonly Button _btnSave;
    private readonly Button _btnClose;

    public event EventHandler<EditModeEventArgs> EditModeChanged;

    public WorkWithForm(Control.ControlCollection controls, Button btnSave, Button btnClose)
    {
      _controls = controls;
      _btnSave = btnSave;
      _btnClose = btnClose;
      EditModeChanged += DefaulEditModeChanged;
    }

    protected virtual void OnEditModeChanged(EditModeEventArgs e)
    {
      EventHandler<EditModeEventArgs> temp = EditModeChanged;

      if (temp != null)
        temp(this, e);
    }

    public void SetEnable(Control.ControlCollection controls)
    {
      foreach (Control control in controls)
      {
        if (control.Controls.Count > 0)
          SetEnable(control.Controls);
        else
          SetEnableValue(control, _editMode);
      }
    }

    public void SetEnableValue(Control control, bool value)
    {
      if ((control is DataGridView) || (control is Label))
        control.Enabled = true;
      else if (control is TextBoxBase)
        SetReadlyOnlyValue(control as TextBoxBase, !value);
      else
        control.Enabled = value;
    }

    private void SetReadlyOnlyValue(TextBoxBase textbox, bool value)
    {
      textbox.ReadOnly = value;
    }

    public bool IsEditMode()
    {
      return _editMode;
    }

    public void SetEditMode(bool enabled)
    {
      _editMode = enabled;

      EditModeEventArgs e = new EditModeEventArgs(enabled);

      OnEditModeChanged(e);
    }

    private void DefaulEditModeChanged(Object sender, EditModeEventArgs e)
    {
      SetEnable(_controls);
      SetEnableValue(_btnClose, true);
      SetEnableValue(_btnSave, User.IsFullAccess());

      _btnSave.Text = _editMode ? "Сохранить" : "Редактировать";
    }
  }
}
