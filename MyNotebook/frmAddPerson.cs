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
    public partial class frmAddPerson : Form
    {
        public frmAddPerson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                return;
            }

            if (!PeopleManager.Add(new Person(PeopleManager.GenerateNewPersonNumber(), textBox1.Text, 0, 0)))
            {
                MessageBox.Show("تعذر تنفيذ العملية");
                return;
            }

            this.Close();
        }
    }
}
