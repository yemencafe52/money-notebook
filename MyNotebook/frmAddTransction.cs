using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotebook
{
    public partial class frmAddTransction : Form
    {
        public frmAddTransction()
        {
            InitializeComponent();
            Preparing();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool Preparing()
        {
            comboBox1.Items.Clear();
            comboBox1.ValueMember = "GetNumber";
            comboBox1.DisplayMember = "GetName";
            comboBox1.DataSource = PTCManager.GetPTCInfo(PeopleManager.GetALL());
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int p_no = Convert.ToInt32( comboBox1.SelectedValue);
            DateTime time = DateTime.Now;
            double debit = 0;
            double credit = 0;
            string desc = textBox2.Text;

            if(!(numericUpDown1.Value>0))
            {
                numericUpDown1.Focus();
                return;
            }

            if(radioButton1.Checked)
            {
                debit = (double)numericUpDown1.Value;
            }
            else
            {
                credit = (double)numericUpDown1.Value;
            }

            if (!TransctionManager.Add(new Transction(0, p_no, time, desc, debit, credit)))
            {
                MessageBox.Show("تعذر تنفيذ العملية");
                return;
            }

            this.Close();

        }
    }

    internal class PTC
    {
        private int number;
        private string name;

        internal PTC(Person p)
        {
            this.name = p.GetName;
            this.number = p.GetNumber;
        }

        public int GetNumber
        {
            get
            {
                return this.number;
            }
        }

        public string GetName
        {
            get
            {
                return this.name;
            }
        }

    }

    internal static class PTCManager
    {

        internal static List<PTC> GetPTCInfo(List<Person> p)
        {
            List<PTC> res = new List<PTC>();

            foreach (Person p0 in p)
            {
                res.Add(new PTC(p0));
            }

            return res;
        }

    }
}
