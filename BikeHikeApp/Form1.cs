using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows;


//
// Windows Form App to allow bike rnetals via the BikeHike database
//
// Brandon Haak
// U. of Illinois, Chicago
// CS480, Summer 2018
// Project #2
//

namespace BikeHikeApp
{
    public partial class Form1 : Form
    {
        string ConnectionInfo;
        static string filename = "BikeHike.mdf";
        SqlConnection db;

        DataAccessTier.Data data = new DataAccessTier.Data(filename);

        DataSet CustomerSet = null;
        DataSet BikeSet = null;
        List<Customer> CustomerList = new List<Customer>();
        List<Bike> AllBikeList = new List<Bike>();
        List<Bike> AvailableBikeList = new List<Bike>();
        List<Rental> CustomerRentalList = new List<Rental>();

        public Form1()
        {
            InitializeComponent();
        }

        private void ClearCustomerUI()
        {
            CustomerList.Clear();
            CustomerSet = null;

            this.listBox1.Items.Clear();
            this.listBox1.Refresh();

            this.textBox2.Clear();
            this.textBox1.Clear();

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
        }

        public void ClearBikeUI()
        {
            BikeSet = null;

            this.listBox2.Items.Clear();
            this.listBox2.Refresh();

            this.textBox20.Clear();
            this.textBox19.Clear();
            this.textBox18.Clear();

            radioButton3.Checked = false;
            radioButton4.Checked = false;

            textBox20.ReadOnly = true;
            textBox19.ReadOnly = true;
            textBox18.ReadOnly = true;

            radioButton4.Enabled = false;
            radioButton3.Enabled = false;
        }

        public void pullFromCustomerDB()
        {
            CustomerSet = null;
            CustomerList.Clear();

            ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
            filename);

            db = new SqlConnection(ConnectionInfo);
            db.Open();

            string sql = string.Format(@"SELECT *  
FROM Customers
ORDER BY LastName, Firstname;");

            //MessageBox.Show(sql);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            CustomerSet = new DataSet();

            cmd.CommandText = sql;
            adapter.Fill(CustomerSet);

            db.Close();

            foreach (DataRow row in CustomerSet.Tables["TABLE"].Rows)
            {
                int id = Convert.ToInt32(row["CID"]);
                string last = Convert.ToString(row["LastName"]);
                string first = Convert.ToString(row["FirstName"]);
                string email = Convert.ToString(row["Email"]);

                Customer c = new Customer(id, last, first, email);

                CustomerList.Add(c);
            }
        }

        public void pullAllBikes()
        {
            AllBikeList.Clear();

            ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
           filename);

            db = new SqlConnection(ConnectionInfo);
            db.Open();

            string sql = string.Format(@"SELECT Bikes.BID, Bikes.TID, Bikes.Year, Bikes.Rented, BikeTypes.TypeDescription, BikeTypes.PricePerHour
FROM Bikes
INNER JOIN BikeTypes ON BikeTypes.TID = Bikes.TID
Order By Bikes.BID;");

            //MessageBox.Show(sql);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            BikeSet = new DataSet();

            cmd.CommandText = sql;
            adapter.Fill(BikeSet);

            db.Close();

            foreach (DataRow row in BikeSet.Tables["TABLE"].Rows)
            {
                int id = Convert.ToInt32(row["BID"]);
                int tid = Convert.ToInt32(row["TID"]);
                int year = Convert.ToInt32(row["Year"]);
                string description = Convert.ToString(row["TypeDescription"]);
                decimal hrprice = Convert.ToDecimal(row["PricePerHour"]);
                bool rented = Convert.ToBoolean(row["Rented"]);

                Bike b = new Bike(id, tid, year, description, hrprice, rented);

                AllBikeList.Add(b);
            }
        }

        public void pullAvailableBikes()
        {
            AvailableBikeList.Clear();
            BikeSet = null;

            ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
           filename);

            db = new SqlConnection(ConnectionInfo);
            db.Open();

            string sql = string.Format(@"SELECT Bikes.BID, Bikes.TID, Bikes.Year, Bikes.Rented, BikeTypes.TypeDescription, BikeTypes.PricePerHour
FROM Bikes
INNER JOIN BikeTypes ON BikeTypes.TID = Bikes.TID
Where Bikes.Rented = 0
Order By Bikes.BID;");

            //MessageBox.Show(sql);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            BikeSet = new DataSet();

            cmd.CommandText = sql;
            adapter.Fill(BikeSet);

            db.Close();

            foreach (DataRow row in BikeSet.Tables["TABLE"].Rows)
            {
                int id = Convert.ToInt32(row["BID"]);
                int tid = Convert.ToInt32(row["TID"]);
                int year = Convert.ToInt32(row["Year"]);
                string description = Convert.ToString(row["TypeDescription"]);
                decimal hrprice = Convert.ToDecimal(row["PricePerHour"]);
                bool rented = Convert.ToBoolean(row["Rented"]);

                Bike b = new Bike(id, tid, year, description, hrprice, rented);

                AvailableBikeList.Add(b);
            }
        }

        public bool containsLetter(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (!char.IsNumber(s[i]))
                {
                    return true;
                }
            return false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ClearCustomerUI();

            pullFromCustomerDB();

            foreach (Customer c in CustomerList)
            {
                this.listBox1.Items.Add(c.printName());
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {

            ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
            filename);

            db = new SqlConnection(ConnectionInfo);
            db.Open();

            string msg = db.State.ToString();
            MessageBox.Show(msg);

            db.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            Customer c = CustomerList[i];
            this.textBox1.Text = Convert.ToString(c.CID);
            this.textBox2.Text = c.Email;

            radioButton2.Checked = false;
            radioButton1.Checked = false;

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            textBox5.Clear();
            textBox3.Clear();

            listBox4.Items.Clear();
            listBox4.Refresh();
            CustomerRentalList.Clear();

            ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
           filename);

            db = new SqlConnection(ConnectionInfo);
            db.Open();

            string sql = string.Format(@"select Rentals.BID, Rentals.RID, Rentals.ExpDuration, BikeTypes.TypeDescription, Rentals.StartTime, BikeTypes.PricePerHour
from Rentals
Inner Join Bikes on Bikes.BID = Rentals.BID
Inner Join BikeTypes on Bikes.TID = BikeTypes.TID
Where Rentals.CID = '{0}' AND Rentals.ActDuration IS NULL
ORDER BY StartTime asc;", c.CID);

            //MessageBox.Show(sql);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            cmd.CommandText = sql;
            adapter.Fill(ds);

            db.Close();

            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                this.listBox4.Items.Add(row["BID"]);

                int bid = Convert.ToInt32(row["BID"]);
                int rid = Convert.ToInt32(row["RID"]);
                double dur = Convert.ToDouble(row["ExpDuration"]);
                string desc = Convert.ToString(row["TypeDescription"]);
                string start = Convert.ToString(row["StartTime"]);
                decimal price = Convert.ToDecimal(row["PricePerHour"]);

                Rental r = new Rental(bid, rid, dur, desc, start, price);

                CustomerRentalList.Add(r);
            }
            if (CustomerRentalList.Count > 0)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearBikeUI();

            pullAllBikes();

            foreach (Bike b in AllBikeList)
            {
                this.listBox2.Items.Add(b.printName());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            ClearBikeUI();

            pullAllBikes();

            //pullAvailableBikes();

            foreach (Bike b in AllBikeList)
            {
                if (!b.Rented)
                    this.listBox2.Items.Add(b.printName());
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();

            string test = listBox2.GetItemText(listBox2.SelectedItem);
            int i = Convert.ToInt32(test.Substring(0, 4));
            Bike b = AllBikeList[i-1001];
            this.textBox20.Text = Convert.ToString(b.Year);
            this.textBox19.Text = Convert.ToString(b.Description);
            this.textBox18.Text = string.Format("${0:#.00}", Convert.ToDecimal(b.HourlyPrice));

            if (b.Rented == true)
            {
                radioButton4.Checked = true;

                ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
           filename);

                db = new SqlConnection(ConnectionInfo);
                db.Open();

                string sql = string.Format(@"select Rentals.BID, Rentals.ExpDuration, Rentals.StartTime
from Rentals
Inner Join Bikes on Bikes.BID = Rentals.BID
Inner Join BikeTypes on Bikes.TID = BikeTypes.TID
Where Rentals.BID = '{0}';", b.BID);

                //MessageBox.Show(sql);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = db;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                cmd.CommandText = sql;
                adapter.Fill(ds);

                db.Close();

                int bid;
                double duration = 0.0;
                string startTime = null;
                foreach (DataRow row in ds.Tables["TABLE"].Rows)
                {
                    this.listBox5.Items.Add(row["BID"]);

                    bid = Convert.ToInt32(row["BID"]);
                    duration = Convert.ToDouble(row["ExpDuration"]);
                    startTime = Convert.ToString(row["StartTime"]);
                }

                TimeSpan dur = TimeSpan.FromHours(duration);
                DateTime start = DateTime.Parse(startTime);
                DateTime timeLeft = start.Add(dur);
                this.textBox21.Text = Convert.ToString(timeLeft);
            }

            else
                radioButton3.Checked = true;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
            listBox5.Refresh();
            CustomerRentalList.Clear();

            int i = listBox3.SelectedIndex;
            Customer c = CustomerList[i];

            ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
           filename);

            db = new SqlConnection(ConnectionInfo);
            db.Open();

            string sql = string.Format(@"select Rentals.BID, Rentals.RID, Rentals.ExpDuration, BikeTypes.TypeDescription, Rentals.StartTime, BikeTypes.PricePerHour
from Rentals
Inner Join Bikes on Bikes.BID = Rentals.BID
Inner Join BikeTypes on Bikes.TID = BikeTypes.TID
Where Rentals.CID = '{0}' AND Rentals.ActDuration IS NULL
ORDER BY StartTime asc;", c.CID);

            //MessageBox.Show(sql);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            cmd.CommandText = sql;
            adapter.Fill(ds);

            db.Close();

            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                this.listBox5.Items.Add(row["BID"]);

                int bid = Convert.ToInt32(row["BID"]);
                int rid = Convert.ToInt32(row["RID"]);
                double dur = Convert.ToDouble(row["ExpDuration"]);
                string desc = Convert.ToString(row["TypeDescription"]);
                string start = Convert.ToString(row["StartTime"]);
                decimal price = Convert.ToDecimal(row["PricePerHour"]);

                Rental r = new Rental(bid, rid, dur, desc, start, price);

                CustomerRentalList.Add(r);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CustomerList.Clear();
            CustomerSet = null;

            listBox3.Items.Clear();
            listBox3.Refresh();

            pullFromCustomerDB();

            foreach (Customer c in CustomerList)
            {
                this.listBox3.Items.Add(c.printName());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView1.Refresh();

            pullAllBikes();

            treeView1.CheckBoxes = true;

            treeView1.BeginUpdate();

            treeView1.Nodes.Add("Tandem For Two");
            TreeNode tandem = treeView1.Nodes[0];
            treeView1.Nodes.Add("Single Cruiser");
            TreeNode cruiser = treeView1.Nodes[1];
            treeView1.Nodes.Add("Single Electric");
            TreeNode electric = treeView1.Nodes[2];
            treeView1.Nodes.Add("Recumbent");
            TreeNode recumbent = treeView1.Nodes[3];
            treeView1.Nodes.Add("Single 21-Speed");
            TreeNode speed = treeView1.Nodes[4];
            treeView1.Nodes.Add("Tricycle");
            TreeNode tri = treeView1.Nodes[5];
            treeView1.Nodes.Add("Unicycle");
            TreeNode uni = treeView1.Nodes[6];

            foreach (Bike b in AllBikeList)
            {
                if (!b.Rented && b.TID == 1)
                {
                    tandem.Nodes.Add(Convert.ToString(b.BID));
                }

                if (!b.Rented && b.TID == 2)
                {
                    cruiser.Nodes.Add(Convert.ToString(b.BID));
                }

                if (!b.Rented && b.TID == 3)
                {
                    electric.Nodes.Add(Convert.ToString(b.BID));
                }

                if (!b.Rented && b.TID == 4)
                {
                    recumbent.Nodes.Add(Convert.ToString(b.BID));
                }

                if (!b.Rented && b.TID == 5)
                {
                    speed.Nodes.Add(Convert.ToString(b.BID));
                }

                if (!b.Rented && b.TID == 6)
                {
                    tri.Nodes.Add(Convert.ToString(b.BID));
                }

                if (!b.Rented && b.TID == 7)
                {
                    uni.Nodes.Add(Convert.ToString(b.BID));
                }
            }
            treeView1.ExpandAll();
            treeView1.EndUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedBikes = null;
            List<Bike> selectedBikeList = new List<Bike>();
            decimal totalPrice = 0;
            foreach (TreeNode node in this.treeView1.Nodes)
            {
                foreach (TreeNode sub in node.Nodes)
                {
                    if (sub.Checked)
                    {
                        int id = Convert.ToInt32(sub.Text);
                        Bike b = AllBikeList[id - 1001];
                        totalPrice += b.HourlyPrice;
                        selectedBikes += b.BID + ", ";
                        selectedBikeList.Add(b);
                    }
                }
            }

            if (selectedBikes == null || string.IsNullOrWhiteSpace(textBox8.Text) || containsLetter(textBox8.Text))
            {
                MessageBox.Show("Please make sure that you enter a numerical value in the expecteed time field!");
            }
            else
            {
                selectedBikes = selectedBikes.Remove(selectedBikes.Length - 2);
                DialogResult result = MessageBox.Show(string.Format("Are you sure you would like to rent bikes " + selectedBikes + " at an estimated price of ${0:#.00}?", Convert.ToDecimal(totalPrice)*Convert.ToDecimal(textBox8.Text)), "ATTENTION!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    Customer c = CustomerList[this.listBox3.SelectedIndex];

                    ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
                    filename);

                    db = new SqlConnection(ConnectionInfo);
                    db.Open();

                    foreach (Bike b in selectedBikeList)
                    {
                        string sql = string.Format(@"INSERT INTO 
Rentals(CID,BID,StartTime,ExpDuration)
Values((SELECT CID from Customers WHERE CID = '{0}'),
(SELECT BID from Bikes WHERE BID = '{1}'),
'{2}',
'{3}');

UPDATE Bikes
Set Rented = 1
Where BID = '{1}';
                    ", c.CID, b.BID, DateTime.Now, Convert.ToDecimal(textBox8.Text)); ;

                        //MessageBox.Show(sql);

                        data.ExecuteActionQuery(sql);
                    }
                    

                    db.Close();

                    MessageBox.Show("success");

                    treeView1.Nodes.Clear();
                    treeView1.Refresh();

                    textBox8.Clear();
                    textBox8.Refresh();
                }
                else { }
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox7.Clear();
            textBox6.Clear();

            textBox7.ReadOnly = true;
            textBox6.ReadOnly = true;

            Rental r = CustomerRentalList[this.listBox5.SelectedIndex];

            this.textBox7.Text = r.Description;
            TimeSpan dur = TimeSpan.FromHours(r.Duration);
            DateTime start = DateTime.Parse(r.StartTime);
            DateTime timeLeft = start.Add(dur);
            this.textBox6.Text = Convert.ToString(timeLeft);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (CustomerRentalList.Count == 0)
            {
                return;
            }

            Rental r = CustomerRentalList[this.listBox5.SelectedIndex];

            DateTime start = DateTime.Parse(r.StartTime);
            TimeSpan timeHad = DateTime.Now.Subtract(start);

            decimal actPrice = Convert.ToDecimal((timeHad.TotalHours).ToString("#.00")) * r.Price;

            DialogResult results = MessageBox.Show(string.Format("Are you sure you would like to return this {0}? \n" + "(BID: {1}) \nCurrent Expense ${2:#.00}", r.Description, r.BID, actPrice), "ATTENTION!", MessageBoxButtons.YesNo);
            if (results == DialogResult.Yes)
            {
                ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
                    filename);

                db = new SqlConnection(ConnectionInfo);
                db.Open();

                string sql = string.Format(@"UPDATE Rentals
Set ActDuration = '{0}', TotalPrice = '{1}'
Where Rentals.BID = '{2}' AND Rentals.ActDuration IS NULL;

UPDATE Bikes
Set Rented = 0
Where BID = '{2}';", Convert.ToDouble(timeHad.Hours), actPrice, r.BID);

                //MessageBox.Show(sql);

                data.ExecuteActionQuery(sql);

                db.Close();

                MessageBox.Show("success");

                listBox5.Items.Clear();
                listBox5.Refresh();
                CustomerRentalList.Clear();

                textBox7.Clear();
                textBox6.Clear();
            }
            else if (results == DialogResult.No) { }

            else { }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox5.Clear();

            textBox3.ReadOnly = true;
            textBox5.ReadOnly = true;

            Rental r = CustomerRentalList[this.listBox4.SelectedIndex];

            this.textBox3.Text = r.Description;
            TimeSpan dur = TimeSpan.FromHours(r.Duration);
            DateTime start = DateTime.Parse(r.StartTime);
            DateTime timeLeft = start.Add(dur);
            this.textBox5.Text = Convert.ToString(timeLeft);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (CustomerRentalList.Count == 0)
            {
                return;
            }

            decimal actPrice = 0;
            foreach (Rental r in CustomerRentalList)
            {

                DateTime start = DateTime.Parse(r.StartTime);
                TimeSpan timeHad = DateTime.Now.Subtract(start);

                actPrice += Convert.ToDecimal((timeHad.TotalHours).ToString("#.00")) * r.Price;
                
            }

            DialogResult results = MessageBox.Show(string.Format("Are you sure you want to return all {0} of your rented bikes? \nCurrent Expense ${1:#.00}", CustomerRentalList.Count, actPrice), "ATTENTION!", MessageBoxButtons.YesNo);
            if (results == DialogResult.Yes)
            {
                foreach (Rental r in CustomerRentalList)
                {
                    DateTime start = DateTime.Parse(r.StartTime);
                    TimeSpan timeHad = DateTime.Now.Subtract(start);

                    ConnectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;
                    AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;",
                        filename);

                    db = new SqlConnection(ConnectionInfo);
                    db.Open();

                    string sql = string.Format(@"UPDATE Rentals
Set ActDuration = '{0}', TotalPrice = '{1}'
Where Rentals.BID = '{2}' AND Rentals.ActDuration IS NULL;

UPDATE Bikes
Set Rented = 0
Where BID = '{2}';", Convert.ToDouble(timeHad.Hours), actPrice, r.BID);

                    //MessageBox.Show(sql);

                    data.ExecuteActionQuery(sql);

                    db.Close();

                }

                MessageBox.Show("success");

                listBox5.Items.Clear();
                listBox5.Refresh();
                CustomerRentalList.Clear();

                textBox7.Clear();
                textBox6.Clear();
            }
            else if (results == DialogResult.No) { }

            else { }
        }
    }
}
