using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseM5
{
    public partial class Form1 : Form
    {
        M5 d = new M5();
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Form1()
        {
            d.Connect();
            InitializeComponent();
            LoadData();
        }

        //public void LoadData()
        //{
        //    dgc.DataSource = null;
        //    cmd = new SqlCommand("RSup",d.Connection);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    SqlDependency dep = new SqlDependency(cmd);
        //    dep.OnChange += new OnChangeEventHandler(OnChange);

        //    adapter = new SqlDataAdapter(cmd);
        //    dt = new DataTable();
        //    adapter.Fill(dt);

        //    dgc.DataSource = dt;
        //}

        public void LoadData()
        {
            if (tgproc.Checked == true)
            {
                gggg.DataSource = null;
                using (cmd = new SqlCommand("RPro", d.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gggg.DataSource = dt;
                    }
                }
            }
            else
            {
                gggg.DataSource = null;
                using (cmd = new SqlCommand("RSup", d.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gggg.DataSource = dt;
                    }
                }
            }
        }


        public void OnChange(object caller, SqlNotificationEventArgs e)
        {
            if (this.InvokeRequired)
            {
                gggg.BeginInvoke(new MethodInvoker(LoadData));
            }
            else
            {
                LoadData();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(this.Width - panel1.Width - 5, 3);
            lbprocname.Visible = false;
            labelqty.Visible = false;
            lbupis.Visible = false;
            lbsup.Visible = false;
            lbqty1.Visible = false;
            txtsup.Visible = false;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void tgproc_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            if (tgproc.Checked == true)
            {
                lbprocname.Visible = true;
                labelqty.Visible = true;
                lbupis.Visible = true;
                lbsup.Visible = true;
                lbqty1.Visible = true;
                txtsup.Visible = true;
                lbcon.Visible = false;
                lbname.Visible = false;
                lbaddress.Visible = false;
                LoadData();
                lbstat.Text = "Changed to Product Table.";
            }
            else if (tgproc.Checked == false)
            {
                lbprocname.Visible = false;
                labelqty.Visible = false;
                lbupis.Visible = false;
                lbsup.Visible = false;
                lbqty1.Visible = false;
                txtsup.Visible = false;
                lbcon.Visible = true;
                lbname.Visible = true;
                lbaddress.Visible = true;
                LoadData();
                lbstat.Text = "Changed to Supplier Table.";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tgproc.Checked == false)
            {
                cmd = new SqlCommand("InsertSup", d.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@su", txtName.Text);
                cmd.Parameters.AddWithValue("@ad", txtaddress.Text);
                cmd.Parameters.AddWithValue("@con", txtCon.Text);

                cmd.ExecuteNonQuery();
                LoadData();
                lbstat.Text = "Added Supplier Data.";
            }
            if (tgproc.Checked == true)
            {
                cmd = new SqlCommand("InsertProc", d.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@pc", txtName.Text);
                cmd.Parameters.AddWithValue("@us", txtaddress.Text);
                cmd.Parameters.AddWithValue("@qy", txtCon.Text);
                cmd.Parameters.AddWithValue("@sp", txtsup.Text);

                cmd.ExecuteNonQuery();
                LoadData();
                lbstat.Text = "Added Product Data.";
            }

            //MessageBox.Show("Data Stored.");
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (tgproc.Checked == false)
            {
                if (rbID.Checked && !rbName.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        LoadData();
                    }
                    else if (int.TryParse(txtID.Text, out _))
                    {
                        (gggg.DataSource as DataTable).DefaultView.RowFilter = string.Format("ID = '{0}'",
                        txtID.Text.Replace("'", "''"));
                        lbstat.Text = "Successfully search supplier by ID.";
                    }
                    else
                    {
                        lbstat.Text = "Error";
                    }
                }
                else if (!rbID.Checked && rbName.Checked)
                {
                    (gggg.DataSource as DataTable).DefaultView.RowFilter = string.Format("Supplier LIKE '%{0}%'",
                    txtName.Text.Replace("'", "''"));
                    lbstat.Text = "Successfully search supplier by Name.";
                }
            }
            else
            {
                if (rbID.Checked && !rbName.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        LoadData();
                    }
                    else if (int.TryParse(txtID.Text, out _))
                    {
                        (gggg.DataSource as DataTable).DefaultView.RowFilter = string.Format("Code = '{0}'",
                        txtID.Text.Replace("'", "''"));
                        lbstat.Text = "Successfully search product by ID.";
                    }
                    else
                    {
                        lbstat.Text = "Error";
                    }
                }
                else if (!rbID.Checked && rbName.Checked)
                {
                    (gggg.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'",
                    txtName.Text.Replace("'", "''"));
                    lbstat.Text = "Successfully search product by Name.";
                }
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            LoadData();
            lbstat.Text = "Table Reset";
        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void bunifuGradientPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void bunifuGradientPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Click(object sender, EventArgs e)
        {
            if (tgproc.Checked == false)
            {
                if(rbID.Checked == true)
                {
                    cmd = new SqlCommand("DeleteSup", d.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                    LoadData();
                    lbstat.Text = "Deleted Supplier by ID.";
                }
                else
                {
                    cmd = new SqlCommand("DeleteSupByName", d.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@su", Convert.ToString(txtName.Text));
                    cmd.ExecuteNonQuery();
                    LoadData();
                    lbstat.Text = "Deleted Supplier by name.";
                }
            }
            else
            {
                if (rbID.Checked == true)
                {
                    cmd = new SqlCommand("DeletePro", d.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.ExecuteNonQuery();
                    LoadData();
                    lbstat.Text = "Deleted Product by ID.";
                }
                else
                {
                    cmd = new SqlCommand("DeleteProByName", d.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pc", Convert.ToString(txtName.Text));
                    cmd.ExecuteNonQuery();
                    LoadData();
                    lbstat.Text = "Deleted Product by name.";
                }
            }
        }
        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tgproc.Checked == false)
            {
                cmd = new SqlCommand("UpdateSup", d.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@su", txtName.Text);
                cmd.Parameters.AddWithValue("@ad", txtaddress.Text);
                cmd.Parameters.AddWithValue("@con", txtCon.Text);

                cmd.ExecuteNonQuery();
                LoadData();
                lbstat.Text = "Updated Supplier by ID.";
            }
            else
            {
                cmd = new SqlCommand("UpdatePro", d.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@pc", txtName.Text);
                cmd.Parameters.AddWithValue("@us", txtaddress.Text);
                cmd.Parameters.AddWithValue("@qy", txtCon.Text);
                cmd.Parameters.AddWithValue("@sp", txtsup.Text);
                cmd.ExecuteNonQuery();
                LoadData();
                lbstat.Text = "Updated Product by ID.";
            }

        }
    }
}
