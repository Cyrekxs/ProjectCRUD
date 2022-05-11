using ProjectCRUD.Library.Entities;
using ProjectCRUD.Library.Interface;
using ProjectCRUD.Library.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCRUD
{    
    public partial class frm_information_entry : Form
    {
        private readonly SavingOptions savingOptions;
        private readonly InformationEntity entity;
        IInformationRepository repo = new InformationRepository();

        //constructor identifier if the form is using for adding
        public frm_information_entry()
        {
            InitializeComponent();
            this.savingOptions = SavingOptions.New;
        }

        //constructor identifier if the form is using for editing
        public frm_information_entry(InformationEntity entity)
        {
            InitializeComponent();
            this.entity = entity;
            this.savingOptions = SavingOptions.Update;
            txtFirstname.Text = entity.Firstname;
            txtLastname.Text = entity.Lastname;
            cmbGender.Text = entity.Gender;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            InformationEntity entity = new InformationEntity()
            {
                Firstname = txtFirstname.Text,
                Lastname = txtLastname.Text,
                Gender = cmbGender.Text
            };

            int savingResult = 0;
            if (savingOptions == SavingOptions.New)
            {
                savingResult = await repo.Save(entity);
            }
            else
            {
                entity.InformationID = this.entity.InformationID;
                savingResult = await repo.Update(entity);
            }
            
            if (savingResult > 0)
            {
                MessageBox.Show("Success","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Error encounter while trying to save/update!", "Error Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public enum SavingOptions
        {
            New,
            Update
        }

    }
}
