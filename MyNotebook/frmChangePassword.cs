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
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                return;
            }

            if (textBox1.Text != UserManager.GetActiveUser.GetPassword)
            {
                textBox1.Focus();
                return;
            }

            if (textBox2.Text != textBox3.Text)
            {
                textBox3.Focus();
                return;
            }


            if(!UserManager.ChangePassword(textBox3.Text))
            {
                MessageBox.Show("تعذر تنفيذ العملية");
                return;
            }

            UserManager.Login(new User(1,"admin",textBox3.Text));

            this.Close();

        }
    }
}
