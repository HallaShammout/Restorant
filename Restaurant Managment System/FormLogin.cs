﻿using Restaurant_Management_System;
using Restaurant_Managment_System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userType;
            if (MainClass.isValidUser(txtUser.Text, txtPassword.Text,out userType) == false)
            {
                guna2MessageDialog1.Show("invalid username or password");
                return;
            }
            if (userType == "Manager")
            {
                FormMain frm = new FormMain();
                this.Hide();
                frm.Show();
            }
            else if(userType == "Cashier")
            {
                FormCashier frm = new FormCashier();
                this.Hide(); 
                frm.Show();
            }
            else
            { guna2MessageDialog1.Show("the user not have accsses"); }
        }
    }
}
