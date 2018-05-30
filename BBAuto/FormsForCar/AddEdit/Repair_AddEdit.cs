using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto
{
    public partial class Repair_AddEdit : Form
    {
        private Repair _repair;

        private WorkWithForm _workWithForm;

        public Repair_AddEdit(Repair repair)
        {
            InitializeComponent();

            _repair = repair;
        }

        private void Repair_AddEdit_Load(object sender, EventArgs e)
        {
            FillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_repair.ID == 0);
        }

        private void FillFields()
        {
            loadDictionary();

            cbCar.SelectedValue = _repair.Car.ID;
            cbRepairType.SelectedValue = _repair.RepairTypeID;
            cbServiceStantion.SelectedValue = _repair.ServiceStantionID;

            dtpDate.Value = _repair.Date;
            tbCost.Text = _repair.Cost;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _repair.File;
        }

        private void loadDictionary()
        {
            CarList carList = CarList.GetInstance();
            cbCar.DataSource = carList.ToDataTable(Status.Actual);
            cbCar.ValueMember = "id";
            cbCar.DisplayMember = "Бортовой номер";

            RepairTypes repairTypes = RepairTypes.getInstance();
            cbRepairType.DataSource = repairTypes.ToDataTable();
            cbRepairType.ValueMember = "id";
            cbRepairType.DisplayMember = "Название";

            ServiceStantions serviceStantions = ServiceStantions.getInstance();
            cbServiceStantion.DataSource = serviceStantions.ToDataTable();
            cbServiceStantion.ValueMember = "id";
            cbServiceStantion.DisplayMember = "Название";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _repair.RepairTypeID = cbRepairType.SelectedValue.ToString();
                _repair.ServiceStantionID = cbServiceStantion.SelectedValue.ToString();

                _repair.Date = dtpDate.Value;
                _repair.Cost = tbCost.Text;

                TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
                _repair.File = tbFile.Text;

                _repair.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
