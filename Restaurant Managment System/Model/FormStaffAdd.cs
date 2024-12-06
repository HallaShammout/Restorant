using RM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Managment_System.Model
{
    public partial class FormStaffAdd : SampleAdd
    {
        public FormStaffAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public override void guna2Button1_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0)
            { qry = "insert into users values (@Name,@Possword,@Username,@Phone,@Role)"; }
            else
            { qry = "update users set uName=@Name,upassword=@Possword,username=@Username,uPhone=@Phone,uRole=@Role where userID=@id"; }
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Possword", txtPossword.Text);
            ht.Add("@Username", txtUserName.Text);
            ht.Add("@Phone", txtPhone.Text);
            ht.Add("@Role", cbRole.Text);
            if (MainClass.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved Successfully..");
                id = 0;
                txtName.Text = "";
                txtPossword.Text = "";
                txtUserName.Text = "";
                txtPhone.Text = "";
                cbRole.Text = "";
                txtName.Focus();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FormStaffAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
