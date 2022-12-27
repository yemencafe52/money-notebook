using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MyNotebook
{
    internal static class clsUtilities
    {
        internal static bool TestDB()
        {
            bool res = false;

            if(CheckDBFile())
            {
                AccessDB db = new AccessDB(Constants.GetConnectionString);
                string sql = "select count(*) from tblUsers";
                res = db.ExcuteQuery(sql);
                db.CloseConnecton();
            }

            return res;
        }

        internal static bool CheckDBFile()
        {
            bool res = false;

            try
            {
                if(File.Exists(Constants.GetDbPath))
                {
                    res = true;
                }
            }
            catch
            { }

            return res;
        }

        internal static bool InstallDB()
        {
            bool res = false;
            try
            {
                if(!File.Exists(Application.StartupPath + "\\" + "mnbdb.db"))
                {
                    throw new Exception();
                }

                if (!clsUtilities.ResotreDB(Application.StartupPath + "\\" + "mnbdb.db"))
                {
                    throw new Exception();
                }

                if (!TestDB())
                {
                    throw new Exception();
                }

                res = true;
            }
            catch
            {

            }
            return res;
        }

        internal static bool Backup(string path)
        {
            bool res = false;
            try
            {
                File.Copy(Constants.GetDbPath, path, false);
                res = true;
            }
            catch
            {
            }

            return res;
        }

        internal static bool ResotreDB(string path)
        {
            bool res = false;
            try
            {
                File.Copy(path,Constants.GetDbPath, false);
                res = true;
            }
            catch
            {
            }

            return res;
        }

        internal static bool CanRWDB()
        {
            bool res = false;
            try
            {
                if(TestDB())
                {
                    AccessDB db = new AccessDB(Constants.GetConnectionString);
                    string sql = "insert into tblLog (e_msg) values('"+ DateTime.Now.ToString() +"')";
                    if (db.ExcuteNonQuery(sql) == 1)
                    {
                        res = true;
                    }
                }
                
            }
            catch
            {
            }

            return res;
        }


    }
}
