using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotebook
{
    static class clsEntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!clsUtilities.CheckDBFile())
            {
                DialogResult r = MessageBox.Show("تعذر العثور على قاعدة البيانات,, هل تريد إستعادة قاعدة بيانات احتياطية؟", "", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    DialogResult r2 = ofd.ShowDialog();
                    if (r2 == DialogResult.OK)
                    {
                        if (!clsUtilities.ResotreDB(ofd.FileName))
                        {
                            MessageBox.Show("تعذر استعادة قاعدة البيانات");
                        }
                        else
                        {
                            MessageBox.Show("تم استعادة قاعادة البيانات الاحتياطيةى بنجاح.");
                        }

                    }
                }
                else
                {
                    if (!clsUtilities.InstallDB())
                    {
                        MessageBox.Show("تعذر انشاء قاعدة بيانات جديدة.");
                    }
                    else
                    {
                        MessageBox.Show("تم انشاء قاعدة بيانات جديدة بنجاح.");
                    }
                }

                return;
            }

            if (!clsUtilities.TestDB())
            {
                MessageBox.Show("تعذر الاتصال بقاعدة البيانات.");
                return;
            }

            if (!clsUtilities.CanRWDB())
            {
                MessageBox.Show("لا توجد صلاحيات كافية لتشغيل مذكرتي , يُرجى تشغيلها كمسؤول.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmLogin flogin = new frmLogin();

            flogin.ShowDialog();

            if (flogin.Sucess)
            {
                Application.Run(new frmMain());
            }
        }
    }
}
