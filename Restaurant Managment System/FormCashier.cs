using Restaurant_Managment_System.Model;
using RM;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Restaurant_Managment_System
{
    public partial class FormCashier : Form
    {
        public FormCashier()
        {
            InitializeComponent();
        }
        public int MainId = 0;
        public string OrderTyper;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormCashier_Load(object sender, EventArgs e)
        {
           
        }
        private void FormCashier_Load_1(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            AddCategory();
            ProductPanel.Controls.Clear();
            LoadProducts();
        }
        private void AddCategory()
        {
            string qry = "select * from category";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CategoryPanel.Controls.Clear();
            if (dt.Rows.Count > 0)
            {
                int buttonHeight = 50;
                int yPosition = 0;

                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(134, buttonHeight);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["catName"].ToString();

                    b.Location = new Point(10, yPosition);
                    yPosition += buttonHeight + 5;
                    //event for click
                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }
            }
        }
        private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;

            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }

        }
        private void AddItems(string id, string proID, string name, string cat, string price, System.Drawing.Image Pimage)
        {
            var W = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = Pimage,
                id = Convert.ToInt32(proID)
            };
            ProductPanel.Controls.Add(W);
            W.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;
                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
                    {
                        //this will chek it product already there then a one to quantity and update price
                        item.Cells["dvgQty"].Value = int.Parse(item.Cells["dvgQty"].Value.ToString()) + 1;
                        item.Cells["dvgAmount"].Value = int.Parse(item.Cells["dvgQty"].Value.ToString()) *
                                                             double.Parse(item.Cells["dvgPrice"].Value.ToString());
                        return;
                    }
                }
                //this line add new product for sr# and 0 from id
                guna2DataGridView1.Rows.Add(new object[] { 0, 0,wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                GetTotal();
            };
        }

        //Getting product from db
        private void LoadProducts()
        {
            string qry = "select * from products inner join category  on catID=CategoryID";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["pImage"];
                byte[] imagebytearray = imagearray;
                AddItems("0", item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(), item["pPrice"].ToString(), System.Drawing.Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
        private void GetTotal()
        {

            double tot = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dvgAmount"].Value.ToString());
            }
            lblTotal.Text = tot.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainId = 0;
            lblTotal.Text = "00";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderTyper = "Delivery";
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderTyper = "Take Away";
        }

        private void btnDin_Click(object sender, EventArgs e)
        {
            OrderTyper = "Din In";
            // need to creat table for table selection and waiter selection
            frmTableSelect frm = new frmTableSelect();
            frm.Show();

            if (frm.TableName != "")
            {
                lblTable.Text = frm.TableName;
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;
            }




            frmWaiterSelect frm2 = new frmWaiterSelect();

            if (frm2.WaiterName != "")
            {
                lblWaiter.Text = frm2.WaiterName;
                lblWaiter.Visible = true;
            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;
            }
        }

        private void btnKot_Click(object sender, EventArgs e)
        {

            //save the data in database
            //creat tables
            string qry1 = ""; //Main table
            string qry2 = ""; //Main Details

            int detailID = 0;
            if (MainId == 0)  //insert
            {
                qry1 = @" insert into tblMain values(@aDate,  @aTime,  @TableName ,  @WaiterName , @status,   @orderType,@total,   @recieved ,  @change );
                  select SCOPE_IDENTIFITY()";


            }
            else
            {

                qry1 = @" update tblMain set statues= @status,total= @total  ,  recieved =@recieved ,  change=@change where MainID=@ID";

            }
            Hashtable ht = new Hashtable();


            SqlCommand cmd = new SqlCommand(qry1, MainClass.con);
            cmd.Parameters.AddWithValue("@MainID", MainId);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue(" @aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue(" @TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@WaiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "pending");
            cmd.Parameters.AddWithValue(" @orderType", OrderTyper);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@recieved", Convert.ToDouble(0));

            if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
            if (MainId == 0) { MainId = Convert.ToInt32(cmd.ExecuteNonQuery()); } else { cmd.ExecuteNonQuery(); }
            if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dvgid"].Value);
                if (detailID == 0)
                {
                    qry2 = @"insert into tblDetails values ( @MainID,@proID,@qty,@price,@amount)";
                }
                else
                {
                    qry2 = @"update set tblDetails  proID= @proID,qty= @qty,price= @price,amount= @amount where DetailID=@ID";


                }
                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainId);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dvgProID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dvgQty"].Value));

                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dvgPrice"].Value));

                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dvgAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }



                guna2MessageDialog1.Show("saved successfuly");
                MainId = 0;
                detailID = -0;
                guna2DataGridView1.Rows.Clear();
                lblTable.Text = "";
                lblWaiter.Text = "";
                lblTable.Visible = false;
                lblWaiter.Visible = false;
                MainId = 0;
                lblTotal.Text = "00";
            }
        }

        private void CategoryPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
    }
}