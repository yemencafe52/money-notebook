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
    public partial class frmDetiels : Form
    {
        Person person = null;
        internal frmDetiels(Person person)
        {
            InitializeComponent();
            this.person = person;
            Preparing();
        }

        private bool Preparing()
        {
            Print(TransctionManager.Search(new DateTime(1900, 1, 1), new DateTime(2999, 12, 31), this.person));
            return true;
        }

        private void Print(List<Transction> t)
        {
            listView1.Items.Clear();

            for(int i=0;i<t.Count;i++)
            {
                ListViewItem lvi = new ListViewItem(t[i].GetTransction.ToString());
                lvi.SubItems.Add(t[i].GetTime.ToString());
                lvi.SubItems.Add(t[i].GetDescrption.ToString());
                lvi.SubItems.Add(t[i].GetDebit.ToString());
                lvi.SubItems.Add(t[i].GetCredit.ToString());
                listView1.Items.Add(lvi);
            }
        }
    }
}
