using BBAuto.Domain.Common;
using PresentationControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace BBAuto
{
  public class MyFilter
  {
    private DataGridView _dgv;

    private List<CheckBoxComboBox> comboList;
    private List<Label> labelList;
    private List<List<string>> valueList;
    private MyStatusStrip _statusStrip;
    private Form _mainForm;

    private bool _load;
    private static MyFilter _instanceCars;
    private static MyFilter _instanceDrivers;

    public static MyFilter GetInstanceCars()
    {
      if (_instanceCars == null)
        _instanceCars = new MyFilter();

      return _instanceCars;
    }

    public static MyFilter GetInstanceDrivers()
    {
      if (_instanceDrivers == null)
        _instanceDrivers = new MyFilter();

      return _instanceDrivers;
    }

    private MyFilter()
    {
      comboList = new List<CheckBoxComboBox>();
      labelList = new List<Label>();
      valueList = new List<List<string>>();
    }

    public void Fill(DataGridView dgv, MyStatusStrip statusStrip, Form mainForm)
    {
      _dgv = dgv;
      _statusStrip = statusStrip;
      _mainForm = mainForm;
    }

    public void ApplyFilter()
    {
      if (_load)
      {
        changeVisibleByFilter();
        tryCreateComboBox();

        setFocus();
        _statusStrip.writeStatus();
      }
    }

    private void changeVisibleByFilter()
    {
      saveFilterValue();

      SetVisibleAllRows();

      _dgv.CurrentCell = null;
      bool filtersNotSet = true;

      for (int comboIndex = 0; comboIndex < comboList.Count; comboIndex++)
      {
        if (IsFilterEmpty(comboIndex))
          continue;

        filtersNotSet = false;

        if (!IsAllSelected(comboIndex))
        {
          foreach (DataGridViewRow row in _dgv.Rows)
          {
            string dgvValue = row.Cells[comboList[comboIndex].Name].Value.ToString();
            if (MyDateTime.IsDate(dgvValue))
            {
              MyDateTime myDate = new MyDateTime(dgvValue);
              dgvValue = myDate.ToString();
            }

            row.Visible = ((IsEqualsFilterValue(comboIndex, dgvValue)) && (row.Visible == true));
          }
        }
      }

      if (filtersNotSet)
        SetVisibleAllRows();
    }

    public void tryCreateComboBox()
    {
      try
      {
        CreateAllComboBox();
      }
      catch
      {
        _load = true;
      }
    }

    private void CreateAllComboBox()
    {
      _load = false;
      clearComboList();

      foreach (DataGridViewColumn col in _dgv.Columns)
      {
        if ((col.Visible) && (DGVSpecialColumn.CanInclude(col.HeaderText)))
        {
          CreateLabelAndComboBox(col.HeaderText);
        }
      }

      FillAllComboBox();

      _load = true;
    }

    private void CreateLabelAndComboBox(string text)
    {
      ControlsFactory controlsFactory = new ControlsFactory();

      int pointY = getLocationY();

      Label label = controlsFactory.CreateLabelForFilter(pointY, text);
      CheckBoxComboBox combo = controlsFactory.CreateComboBoxForFilter(pointY, text);

      if (tryFillComboBox(text, combo))
      {
        int width = getComboBoxWith(combo, label.Text);
        combo.Width = width + 50;
        label.Width = width;

        _mainForm.Controls.Add(combo);
        comboList.Add(combo);

        _mainForm.Controls.Add(label);
        labelList.Add(label);
      }
    }

    private void FillAllComboBox()
    {
      int i = 0;

      foreach (CheckBoxComboBox combo in comboList)
      {
        if (valueList[i].Count > 0)
        {
          if (valueList[i][0] == Consts.ValueAllForCheckBox)
            combo.CheckBoxItems[1].Checked = true;
          else
          {
            for (int j = 2; j < combo.CheckBoxItems.Count; j++)
              combo.CheckBoxItems[j].Checked = true;
          }

          combo.SelectedItem = valueList[i];
        }
        i++;
      }
    }

    private int getComboBoxWith(ComboBox comboBox, string labelText)
    {
      Graphics graphics = comboBox.CreateGraphics();

      int max = Convert.ToInt32(graphics.MeasureString(labelText, comboBox.Font).Width);

      for (int i = 0; i < comboBox.Items.Count; i++)
      {
        int curWith = Convert.ToInt32(graphics.MeasureString(comboBox.Items[i].ToString(), comboBox.Font).Width);

        if (max < curWith)
          max = curWith;
      }

      return max + 15;
    }

    private int getLocationY()
    {
      int indent = 12;
      int space = 12;

      return indent + (from combo in comboList select combo.Width + space).Sum();
    }

    public void clearComboList()
    {
      foreach (ComboBox combo in comboList)
        _mainForm.Controls.RemoveByKey(combo.Name);

      foreach (Label label in labelList)
        _mainForm.Controls.RemoveByKey(label.Name);

      comboList.Clear();
      labelList.Clear();
    }

    private void saveFilterValue()
    {
      valueList.Clear();

      foreach (CheckBoxComboBox combo in comboList)
      {
        List<string> values = new List<string>();
        foreach (CheckBoxComboBoxItem item in combo.CheckBoxItems)
        {
          if (item.Checked)
            values.Add(item.Text);
        }
        valueList.Add(values);
      }
    }

    public void clearFilterValue()
    {
      valueList.Clear();
    }

    private bool tryFillComboBox(string nameCol, ComboBox combo)
    {
      try
      {
        return fillComboBox(nameCol, combo);
      }
      catch
      {
        return false;
      }
    }

    private bool fillComboBox(string nameCol, ComboBox combo)
    {
      List<string> list = Filters.GetDataSource(_dgv, nameCol);
      if (list.Count == 0)
        return false;

      foreach (string item in list)
        combo.Items.Add(item);

      combo.SelectedIndex = 0;

      return true;
    }

    private void SetVisibleAllRows()
    {
      foreach (DataGridViewRow row in _dgv.Rows)
        row.Visible = true;
    }

    private bool IsFilterEmpty(int comboIndex)
    {
      CheckBoxComboBox chbCombo = comboList[comboIndex];

      return chbCombo.Text.Equals(string.Empty);
    }

    private bool IsAllSelected(int comboIndex)
    {
      return valueList[comboIndex].Any(item => item == Consts.ValueAllForCheckBox);
    }

    private bool IsEqualsFilterValue(int comboIndex, string dgvValue)
    {
      return valueList[comboIndex].Any(item => item == dgvValue);
    }

    private void setFocus()
    {
      _dgv.Focus();
    }

    public void SetFilterValue(string columnName, Point point)
    {
      int i = -1;
      for (i = 0; i < labelList.Count; i++)
      {
        if (labelList[i].Text == columnName)
          break;
      }

      if (i >= comboList.Count)
        return;

      string value = _dgv.Rows[point.Y].Cells[point.X].Value.ToString();

      foreach (CheckBoxComboBoxItem item in comboList[i].CheckBoxItems)
      {
        if (MyDateTime.IsDate(value))
        {
          MyDateTime myDate = new MyDateTime(value);
          value = myDate.ToString();
        }

        if (item.Text == value)
        {
          item.Checked = true;
          break;
        }
      }

      ApplyFilter();
    }
  }
}
