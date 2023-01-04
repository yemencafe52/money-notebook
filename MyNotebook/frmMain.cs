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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Print(PeopleManager.GetALL());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmAddPerson fap = new frmAddPerson();
            fap.ShowDialog();

            Print(PeopleManager.GetALL());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (PeopleManager.GetALL().Count > 0)
            {
                frmAddTransction fat = new frmAddTransction();
                fat.ShowDialog();
                Print(PeopleManager.GetALL());
            }
            else
            {
                MessageBox.Show("لا يوجد اسماء");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword fcp = new frmChangePassword();
            fcp.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox fab = new frmAboutBox();
            fab.ShowDialog();
        }

        private void Print(List<Person> people)
        {
            listView1.Items.Clear();

            for (int i = 0; i < people.Count; i++)
            {
                ListViewItem lvi = new ListViewItem(people[i].GetNumber.ToString());
                lvi.SubItems.Add(people[i].GetName);
                lvi.SubItems.Add(people[i].Debit.ToString("#0.#0"));
                lvi.SubItems.Add(people[i].Credit.ToString("#0.#0"));

                listView1.Items.Add(lvi);

            }

            toolStripStatusLabel2.Text = people.Count.ToString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolStripTextBox1.Text))
            {
                Print(PeopleManager.Search(""));
            }
            else
            {
                Print(PeopleManager.Search(toolStripTextBox1.Text));
            }

            toolStripTextBox1.Focus();
        }
           
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            DialogResult r=  sfd.ShowDialog();
            if (r == DialogResult.OK)
            {
                if (!clsUtilities.Backup(sfd.FileName + ".bak"))
                {
                    MessageBox.Show("تعذر تنفيذ العملية");
                }
            }

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count >0)
            {
                int index = listView1.SelectedItems[0].Index;
                int num = int.Parse(listView1.Items[index].Text);
                Person p = new Person(num);
                frmDetiels fd = new frmDetiels(p);
                fd.ShowDialog();
            }
        }
    }

    
}
