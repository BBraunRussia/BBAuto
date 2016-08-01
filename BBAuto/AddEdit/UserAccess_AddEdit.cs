﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Dictionary;
using BBAuto.Domain.Entities;

namespace BBAuto
{
    public partial class UserAccess_AddEdit : Form
    {
        private UserAccess _userAccess;

        private WorkWithForm _workWithForm;

        public UserAccess_AddEdit(UserAccess userAccess)
        {
            InitializeComponent();
            _userAccess = userAccess;
        }

        private void aeUserAccess_Load(object sender, EventArgs e)
        {
            loadDictionary();

            cbDriver.SelectedValue = _userAccess.Driver.ID;
            cbRole.SelectedValue = _userAccess.RoleID;

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_userAccess.ID == 0);
        }

        private void loadDictionary()
        {
            DriverList driverList = DriverList.getInstance();
            cbDriver.DataSource = driverList.ToDataTable(true);
            cbDriver.DisplayMember = "ФИО";
            cbDriver.ValueMember = "id";

            Roles roles = Roles.getInstance();
            cbRole.DataSource = roles.ToDataTable();
            cbRole.DisplayMember = "Название";
            cbRole.ValueMember = "id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _userAccess.Driver = DriverList.getInstance().getItem(Convert.ToInt32(cbDriver.SelectedValue));
                _userAccess.RoleID = Convert.ToInt32(cbRole.SelectedValue);
                _userAccess.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDriver.SelectedValue == null)
                lbLogin.Text = string.Empty;
            else
            {
                DriverList driverList = DriverList.getInstance();

                int idDriver;
                int.TryParse(cbDriver.SelectedValue.ToString(), out idDriver);

                Driver driver = driverList.getItem(idDriver);
                lbLogin.Text = driver.Login;
            }
        }
    }
}
