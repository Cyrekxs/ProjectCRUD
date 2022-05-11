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
    public partial class frm_information_list : Form
    {
        IInformationRepository repo = new InformationRepository();
        List<InformationEntity> entities = new List<InformationEntity>();
        public frm_information_list()
        {
            InitializeComponent();
        }

        private async Task FetchRepositoryRecord()
        {
            var result = await repo.GetInformations();
            entities = result.ToList();
        }

        private void DisplayData(List<InformationEntity> list)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in list)
            {
                dataGridView1.Rows.Add(item.InformationID, item.Firstname, item.Lastname, item.Gender);
            }
        }
        private async void frm_information_list_Load(object sender, EventArgs e)
        {
            await FetchRepositoryRecord();
            DisplayData(entities);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (frm_information_entry frm = new frm_information_entry())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get the selected record on the datagridview
            InformationEntity selectedEntity = new InformationEntity();
            selectedEntity.InformationID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["clmInformationID"].Value);
            selectedEntity.Firstname = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["clmFirstname"].Value);
            selectedEntity.Lastname = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["clmLastname"].Value);
            selectedEntity.Gender = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["clmGender"].Value);

            //identify button clicked in the datagridview

            //if edit clicked
            if (e.ColumnIndex == clmEdit.Index)
            {
                using(frm_information_entry frm = new frm_information_entry(selectedEntity))
                {
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                }
            }

            //if delete clicked
            else
            {
                if(MessageBox.Show("Are you sure you want to delete this record?","Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var result = await repo.Delete(selectedEntity);
                    if (result > 0)
                    {
                        MessageBox.Show("Successfully deleted!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
            }

            //refresh if either of the two has been triggered;
            await FetchRepositoryRecord();
            DisplayData(entities);
        }
    }
}
