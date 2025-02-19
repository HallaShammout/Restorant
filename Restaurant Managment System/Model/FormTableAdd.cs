﻿using RM;
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
    public partial class FormTableAdd : SampleAdd
    {
        public FormTableAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public override void guna2Button1_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0)
            { qry = "insert into tables values (@Name)"; }
            else
            { qry = "update tables set tName=@Name where tID=@id"; }
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            if (MainClass.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved Successfully..");
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }
        }
    }
}
