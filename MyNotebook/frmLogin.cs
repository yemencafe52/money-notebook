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

    
    public partial class frmLogin : Form
    {

        private bool sucess = false;

        internal bool Sucess
        {
            get
            {
                return sucess;
            }
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserManager.Login(new User(1, textBox1.Text, textBox2.Text)))
            {
                this.sucess = true;
            }

            this.Close();
        }
    }
}
