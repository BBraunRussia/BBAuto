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
    public partial class ShipPart_AddEdit : Form
    {
        ShipPart shipPart;

        private WorkWithForm _workWithForm;

        public ShipPart_AddEdit(ShipPart shipPart)
        {
            InitializeComponent();

            this.shipPart = shipPart;
        }
        
        private void ShipPart_AddEdit_Load(object sender, EventArgs e)
        {
            loadDictionary();

            loadData();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(shipPart.ID == 0);
        }

        private void loadDictionary()
        {
            CarList carList = CarList.GetInstance();
            cbCar.DataSource = carList.ToDataTable(Status.All);
            cbCar.ValueMember = "idCar";
            cbCar.DisplayMember = "Регистрационный знак";

            DriverList driverList = DriverList.getInstance();
            cbDriver.DataSource = driverList.ToDataTable();
            cbDriver.ValueMember = "id";
            cbDriver.DisplayMember = "ФИО";
        }

        private void loadData()
        {
            cbCar.SelectedValue = shipPart.Car.ID;
            cbDriver.SelectedValue = shipPart.Driver.ID;
            tbNumber.Text = shipPart.Number;
            mtbDateRequest.Text = shipPart.DateRequest;
            mtbDateSent.Text = shipPart.DateSent;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = shipPart.File;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                shipPart.Car = CarList.GetInstance().getItem(Convert.ToInt32(cbCar.SelectedValue));
                shipPart.Driver = DriverList.getInstance().getItem(Convert.ToInt32(cbDriver.SelectedValue));
                shipPart.Number = tbNumber.Text;
                shipPart.DateRequest = mtbDateRequest.Text;
                shipPart.DateSent = mtbDateSent.Text;

                TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
                shipPart.File = tbFile.Text;

                shipPart.Save();
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
